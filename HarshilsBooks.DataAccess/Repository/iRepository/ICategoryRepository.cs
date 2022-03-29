using HarshilsBooks.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarshilsBooks.DataAccess.Repository.IRepository
{
    interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
