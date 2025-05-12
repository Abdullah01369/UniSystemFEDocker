using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using ServiceLayer.Requests;
using SharedLayer.Dtos;

namespace UniSystemFE.Areas.Student.Controllers
{
    [Area("Student")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductList()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var val = await _productService.ProductList(userInfo.Email);
            return View(val.Data);
        }

        [HttpGet]
        public IActionResult TakingCardInfo(int Id)
        {
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TakingCardInfo(CardInfoModel model)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
          

            PaymentRequest request = new PaymentRequest
            {
                ProductId = "1",
                UserMail = userInfo.Email

            };

            var val = await _productService.Buy(request);
           
            return View("BuyGetCode",val);
        }

        [HttpGet]
        public async Task<IActionResult> BuyGetCode()
        {
             
            return View( );
        }
    }
}
