namespace GymMachineWS.DataAccessLayer
{
    public interface IRepository<T>
    {
        //הפעולה מוציאה את כל המודלים מטיפוס T
        List<T> GetAll();//רשימה של כל המודל
        T GetById(string id);

        bool Create(T item);
        bool Update(T item);
        bool Delete(string id);

    }
}
