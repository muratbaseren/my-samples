using SampleMvcApp.Core.DataAccess;
using SampleMvcApp.Entities.Abstract;

namespace SampleMvcApp.Business.Abstract
{
    public interface IEntityManager<T> : IDataAccess<T> where T : class, IEntity, new()
    {

    }
}