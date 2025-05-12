using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
 
using ServiceLayer.Services;
using SharedLayer.Dtos;
using UniSystemFE.Options;


namespace UniSystemFE.Areas.Account.Controllers
{
    [Area("Account")]
    public class Login : Controller
    {
        private readonly AccountService _accountService;
     
        public Login( AccountService accountService)
        {
          _accountService = accountService;
            
        }
        public IActionResult Index()
        {
            LoginDto loginDto = new LoginDto();
           //var resp= _accountService.Login(loginDto);      
            
            return View();
        }
   
 
    }
}
