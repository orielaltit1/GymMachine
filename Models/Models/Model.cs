using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Model : INotifyDataErrorInfo
    {
        //מילון בנוי משני ערכים - תכונה ורשימת שגיאות
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        //אירוע שיש שגיאה
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        //אובייקט נעילה - סוגר את זרימת התוכנית,כדי שאל יכנס משהו אחר
        private object threadLock = new object();
        //בדיקה אם המודל תקין או לא, אם אין שגיאות אז הבדיקה עברה בסדר
        private bool isValid;

        public bool HasErrors//אפשר לבדוק אם יש טעויות
        {
            get { return errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0); }
        }

        public bool IsValid//שואל אם היה שגיאות
        {
            get { return !this.HasErrors; }
            
        }

        public void OnErrorsChanged(string propertyName)//מעלה את האירוע
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
                
            }
        }

        public IEnumerable GetErrors(string propertyName)//פעולה שמחזירה את רשימת השגיאות
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (errors.ContainsKey(propertyName) && (errors[propertyName] != null) && errors[propertyName].Count > 0)
                    return errors[propertyName].ToList();
                else
                    return null;
            }
            else
                return errors.SelectMany(err => err.Value.ToList());
        }

        public Dictionary<string, List<string>> AllErrors()//רשימה של כל השגיאות
        {
            return this.errors;
        }

        public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)//פעולה שמבקשים לבדוק את התקינות
        {
            lock (threadLock)
            {
                var validationContext = new ValidationContext(this, null, null);
                validationContext.MemberName = propertyName;
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateProperty(value, validationContext, validationResults);
               //clear previous errors from tested property
                if (errors.ContainsKey(propertyName))
                    errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
                HandleValidationResults(validationResults);
            }
        }

        public void Validate()//בודק את השגאיות ומכניס ברשימת השגיאות
        {
            this.isValid = true; //reset IsValid before validation
            lock (threadLock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                //clear all previous errors
                var propNames = errors.Keys.ToList();
                errors.Clear();
                HandleValidationResults(validationResults);
                this.isValid = validationResults.Count == 0;
            }
        }

        private void HandleValidationResults(List<ValidationResult> validationResults)
        {
            //Group validation results by property names
            var resultsByPropNames = from res in validationResults
                                     from mname in res.MemberNames
                                     group res by mname into g
                                     select g;

            //add errors to dictionary and inform  binding engine about errors
            foreach (var prop in resultsByPropNames)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();
                errors.Add(prop.Key, messages);
                OnErrorsChanged(prop.Key);
            }
        }
    }


}
