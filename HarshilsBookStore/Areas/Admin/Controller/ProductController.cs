using HarshilsBooks.DataAccess.Repository;
using HarshilsBooks.DataAccess.Repository.IRepository;
using HarshilsBooks.Models;
using HarshilsBooks.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarshilsBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
               Product product = new Product(),
               CategoryList = _unitOfWork.CoverType.GetAll().Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem)
            }
            if (id == null)
            {
                return View(product);
            }

            product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound(product);
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _unitOfWork.Product.Add(product);
                    _unitOfWork.Save();
                }
                else
                {
                    _unitOfWork.Product.Update(product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(product);
        }


        // API Calls
        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            //return NotFound
            var allObj = _unitOfWork.Product.GetAll();
            return Json(new { data = allObj });

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = true, message = "Erroe while Deleting" });
            }
            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
            #endregion
        }
    }
}
