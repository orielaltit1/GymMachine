using System.Data;

namespace GymMachineWS
{
    //תפקיד ליצור מודל
    //הנתונים שאנחנו מקבלים זה רקורד סט
    public interface IModelCreator<T>
    {
        //צריך ליצור מודלים
        
        T CreateMoldel(IDataReader reader);

    }
}
