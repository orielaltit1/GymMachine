using System.Data;
using System.Data.OleDb;

namespace GymMachineWS
{
    public interface IDbContext
    {
        //פתיחת קשר עם מסד נתונים
        void OpenConnection();
        //פתיחת קשר עם מסד נתונים
        void CloseConnection();
        //פותח חבילה של פעולות ובודק אם כל הפעולות התבצעו
        //או כולם או אף אחד
        void BeginTransaction();
        //לאשר שכל הפקודות התבצעו
        void Commit();
        //לבטל את כל הפקודות
        void RollBack();

        //למחוק שורות 
        //מחזיר 1 או 0 לפי מספר שורות שהוא מחק
        int Delete(string sql);
        //להכניס שורות
        //מחזיר 1 או 0 לפי מספר שורות שהוא מוסיף
        //בשביל לעבוד מעבירים משפטי SQL
        int Insert(string sql);
        //מעדכן שורות
        //מחזיר1 או 0 לפי שורות שהוא מעדכן
        int Update(string sql);
        //אובייקט היחידי שיכול לשמור את הRecordSet
        IDataReader Select(string sql);
        void AddParamter(string v, object value);
    }
}
