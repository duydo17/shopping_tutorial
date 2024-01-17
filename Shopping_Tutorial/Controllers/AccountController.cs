using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping_Tutorial.Models;
using Shopping_Tutorial.Models.ViewModel;

namespace Shopping_Tutorial.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _usermanager;
		private SignInManager<AppUserModel> _signinmanager;
		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
		{
			_signinmanager = signInManager;
			_usermanager = userManager;
		}
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl});
		}
        
		[HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginvm)
        {
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signinmanager.PasswordSignInAsync(loginvm.UserName, loginvm.Password,false,false);
				if (result.Succeeded) { 
				return Redirect(loginvm.ReturnUrl ?? "/");
				}


            }
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
		[HttpPost]
        public async Task<IActionResult> Create(UserModel user)

        {
			if(ModelState.IsValid)
			{
				AppUserModel newUser = new AppUserModel {UserName = user.UserName, Email = user.Email };
				IdentityResult result = await _usermanager.CreateAsync(newUser,user.Password);
				if(result.Succeeded)
				{
					return Redirect("/account/login");
				}
			}
            return View();
        }
    }
}
