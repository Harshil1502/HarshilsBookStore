using HarshilsBooks.DataAccess.Repository.IRepository;
using HarshilsBooks.Models;
using HarshilsBookStore.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarshilsBooks.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork            //Public method
    {
        private readonly ApplicationDbContext _db;
        
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public ISP_Call SP_Call { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save() => _db.SaveChanges();
    }
}
