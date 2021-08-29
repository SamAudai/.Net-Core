using Core.Interfaces;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericRepository<T> _entity;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        //public IGenericRepository<T> Entity => _entity ?? (_entity = new GenericRepository<T>(_context));

        public IGenericRepository<T> Entity //same as expression that above
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }  

            /*this mean if repository is null create new instanse of (T) same type of repository 
              but if not work with exist instanse*/
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
