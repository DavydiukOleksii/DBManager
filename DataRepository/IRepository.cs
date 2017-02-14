using System.Collections.Generic;

namespace DataRepository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        bool Update(T newObject);
        bool Delete(int id);
        int Insert(T newObject);
    }
}
