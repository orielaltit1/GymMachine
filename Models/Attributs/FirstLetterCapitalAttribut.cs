using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class FirstLetterCapitalAttribut : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null )return false;
            string str = value as string;
            char firstLetter = str[0];
            if (firstLetter < 65 || firstLetter > 90)
                return false;
            for (int i = 1; i < str.Length; i++) 
            {
                if (str[i] < 141 && str[i] > 172)
                    return false;
            }
            return true;
        }

    }
}
