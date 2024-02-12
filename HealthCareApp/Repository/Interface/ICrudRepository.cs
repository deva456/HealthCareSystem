using HealthcareApp.Models.DataModels;
using System.Linq.Expressions;

namespace HealthcareApp.Repository.Interface
{
    public interface ICrudRepository<T> where T : BaseDataModel
    {
        Task<List<T>> GetAll();
        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<T?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task Add(T entity);
        Task<T?> Delete(Guid id);
        Task Update(T entity);
    }
}
