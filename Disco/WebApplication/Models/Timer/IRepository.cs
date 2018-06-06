using System.Linq;

namespace WebApplication.Models.Timer
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> GetAll();

        void Save(T instance);

        T Delete(int id);
    }
}