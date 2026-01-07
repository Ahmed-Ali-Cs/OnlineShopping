using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Data;
using OnlineShopping.DataAccess.Repositories.IRepository;
using OnlineShopping.Models;
using OnlineShopping.Models.Models;


namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment hostEnvironment;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
          
        }
        public IActionResult Index()
        {
            var CompanyList = unitOfWork.Company.GetAll();
            return View(CompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            
          
            if (id==0 || id==null)
            {
                // Create Company
                return View(new Company());
            }
            else
            {
                Company company = unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
                return View(company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                
                if (company.Id != 0)
                {
                    unitOfWork.Company.Update(company);
                    TempData["success"] = "Company Update successfully";
                }
                else
                {
                    unitOfWork.Company.Add(company);
                    TempData["success"] = "Company created successfully";
                }
                    
                unitOfWork.Save();
                
                return RedirectToAction(nameof(Index));
            }
            else
            {
               
                return View(company);
            }
        }
    


        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromdB = unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
        //    if (CompanyFromdB == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CompanyFromdB);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Company? obj = unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    unitOfWork.Company.Remove(obj);
        //    unitOfWork.Save();
        //    TempData["success"] = "Company deleted successfully";
        //    return RedirectToAction(nameof(Index));

        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var CompanyList = unitOfWork.Company.GetAll();
            return Json(new { data = CompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);    
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            unitOfWork.Company.Remove(obj);
            unitOfWork.Save();
            return Json(new { success = true, message = "Company Delete Successful" });
        }
        #endregion

    }
}
