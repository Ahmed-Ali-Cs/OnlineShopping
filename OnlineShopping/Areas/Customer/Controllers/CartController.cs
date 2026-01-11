using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DataAccess.Repositories.IRepository;
using OnlineShopping.Models.Models;
using System.Security.Claims;

namespace OnlineShopping.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public ShoppingCartVm ShoppingCartVm { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var userId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVm = new()
            {
                ShoppingCartlist = unitOfWork.ShoppingCart.GetAll(s => s.ApplicationUserId == userId, Includeproperty: "Product"),
               OrderHeader=new()
            };

            foreach (var cart in ShoppingCartVm.ShoppingCartlist)
            {
                cart.Price = getproductprice(cart);
                ShoppingCartVm.OrderHeader.OrderTotal += (cart.Price * cart.Count);
                
            }
            
            return View(ShoppingCartVm);
        }

        public IActionResult Summary()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var userId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVm = new()
            {
                ShoppingCartlist = unitOfWork.ShoppingCart.GetAll(s => s.ApplicationUserId == userId, Includeproperty: "Product"),
                OrderHeader=new()
            };

            ShoppingCartVm.OrderHeader.ApplicationUser = unitOfWork.ApplicationUser.GetFirstOrDefault(s => s.Id == userId);

            ShoppingCartVm.OrderHeader.Name = ShoppingCartVm.OrderHeader.ApplicationUser.Name;
            ShoppingCartVm.OrderHeader.PhoneNumber = ShoppingCartVm.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVm.OrderHeader.Address = ShoppingCartVm.OrderHeader.ApplicationUser.Address;
            ShoppingCartVm.OrderHeader.City = ShoppingCartVm.OrderHeader.ApplicationUser.City;
            ShoppingCartVm.OrderHeader.State = ShoppingCartVm.OrderHeader.ApplicationUser.State;
            ShoppingCartVm.OrderHeader.PostalCode = ShoppingCartVm.OrderHeader.ApplicationUser.PostalCode;


            foreach (var cart in ShoppingCartVm.ShoppingCartlist)
            {
                cart.Price = getproductprice(cart);
                ShoppingCartVm.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVm);
        }

        public IActionResult Plus(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);
            cart.Count += 1;
            unitOfWork.ShoppingCart.Update(cart);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Miuns(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);
            if (cart.Count <= 1)
            {
                unitOfWork.ShoppingCart.Remove(cart);
            }
            else
            {
                cart.Count -= 1;
                //cart.Price = getproductprice(cart);
                unitOfWork.ShoppingCart.Update(cart);
            }
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);
            unitOfWork.ShoppingCart.Remove(cart);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

       

        private double getproductprice(ShoppingCart shopping)
        {
            if (shopping.Count <= 50)
            {
                return shopping.Product.Price;
            }
            else
            {
                if (shopping.Count <= 100)
                {
                    return shopping.Product.Price50;
                }
                else
                {
                    return shopping.Product.Price100;
                }
            }
        }
    }
}
