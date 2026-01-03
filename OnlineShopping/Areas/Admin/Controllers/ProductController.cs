using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Data;
using OnlineShopping.DataAccess.Repositories.IRepository;
using OnlineShopping.Models;
using OnlineShopping.Models.Models;


namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var ProductList = unitOfWork.Product.GetAll(Includeproperty:"Category");
            return View(ProductList);
        }

        public IActionResult Upsert(int? id)
        {
            
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id==0 || id==null)
            {
                // Create product
                return View(productVM);
            }
            else
            {
                productVM.Product = unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwrootPath, @"Images\Product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwrootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    productVM.Product.ImageUrl = @"\Images\Product\" + fileName;
                }
                if (productVM.Product.Id != 0)
                {
                    unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = "Product Update successfully";
                }
                else
                {
                    unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "Product created successfully";
                }
                    
                unitOfWork.Save();
                
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM.CategoryList= unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(productVM);
            }
        }
    


        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? ProductFromdB = unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
        //    if (ProductFromdB == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromdB);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Product? obj = unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    unitOfWork.Product.Remove(obj);
        //    unitOfWork.Save();
        //    TempData["success"] = "Product deleted successfully";
        //    return RedirectToAction(nameof(Index));

        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var ProductList = unitOfWork.Product.GetAll(Includeproperty: "Category");
            return Json(new { data = ProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            if (obj.ImageUrl != null)
            {
                var oldImagePath = Path.Combine(hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            unitOfWork.Product.Remove(obj);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}
