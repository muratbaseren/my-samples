using SampleMvcApp.Core.DataAccess;
using SampleMvcApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMvcApp.DataAccess.Concrete
{
    public class Repository<T> : IDataAccess<T> where T : class, IEntity, new()
    {
        private readonly DbSet<T> _objectSet;
        private readonly DbContext _context;
        //private readonly DbContext _context = new DatabaseContext();

        public Repository(DbContext context)
        {
            _context = context;
            _objectSet = context.Set<T>();
        }

        //public Repository(/*DbContext context*/)
        //{
        //    //_context = context;
        //    _objectSet = context.Set<T>();
        //}

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }

        public int Update(T obj)
        {
            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

        public List<T> List<K>(Expression<Func<T, K>> order)
        {
            return _objectSet.OrderBy(order).ToList();
        }
    }
}
