using HarshilsBooks.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarshilsBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        // API Calls
        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            //return NotFound
            var allObj = _unitWork.Category.GetAll();
            return Json(new {data = allObj});
              
        }
        #endregion
    }
}
