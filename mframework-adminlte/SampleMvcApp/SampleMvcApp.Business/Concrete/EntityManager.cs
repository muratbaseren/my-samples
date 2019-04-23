using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Core.DataAccess;
using SampleMvcApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMvcApp.Business.Concrete
{
    public class EntityManager<T> : IEntityManager<T> where T : class, IEntity, new()
    {
        private readonly IDataAccess<T> repository;

        public EntityManager(IDataAccess<T> repository)
        {
            this.repository = repository;
        }

        public virtual List<T> List()
        {
            return repository.List();
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return repository.ListQueryable();
        }

        public virtual List<T> List(Expression<Func<T, bool>> where)
        {
            // List rules vs..

            return repository.List(where);
        }

        public virtual int Insert(T obj)
        {
            // Insert rules vs..

            return repository.Insert(obj);
        }

        public virtual int Update(T obj)
        {
            return repository.Update(obj);
        }

        public virtual int Delete(T obj)
        {
            return repository.Delete(obj);
        }

        public virtual int Save()
        {
            return repository.Save();
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            return repository.Find(where);
        }

        public List<T> List<K>(Expression<Func<T, K>> order)
        {
            return repository.List(order);
        }
    }
}
