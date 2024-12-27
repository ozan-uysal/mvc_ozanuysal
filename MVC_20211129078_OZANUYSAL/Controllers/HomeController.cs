using AspNetCoreHero.ToastNotification.Notyf.Models;
using AutoMapper;
using MVC_20211129078_OZANUYSAL.Models;
using MVC_20211129078_OZANUYSAL.Repositories;
using MVC_20211129078_OZANUYSAL.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NETCore.Encrypt.Extensions;
using System.Diagnostics;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace MVC_20211129078_OZANUYSAL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepository _productRepository;

        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly INotyfService _notyf;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ProductRepository productRepository, IMapper mapper, IConfiguration config, INotyfService notyf, IFileProvider fileProvider, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;

            _config = config;
            _notyf = notyf;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            products = products.Where(s => s.IsActive == true).ToList();
            var productModels = _mapper.Map<List<ProductModel>>(products);
            return View(productModels);
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                _notyf.Error("Girilen Kullanıcı Adı Kayıtlı Değildir!");
                return View(model);
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.KeepMe, true);

            if (signInResult.Succeeded)
            {

              
                return RedirectToAction("Index", "Admin");
            }
            if (signInResult.IsLockedOut)
            {
                _notyf.Error("Kullanıcı Girişi " + user.LockoutEnd + " kadar kısıtlanmıştır!");

                return View(model);
            }
            _notyf.Error("Geçersiz Kullanıcı Adı veya Parola Başarısız Giriş Sayısı :" + await _userManager.GetAccessFailedCountAsync(user) + "/3");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            bool isSucceded = true;

            if (model == null)
            {
                _notyf.Error("Bilinmeyen hata!");
                isSucceded = false;
            }

            if (model.Password == null)
            {
                _notyf.Error("Parola boş olamaz!");
                isSucceded = false;
            }

            if (model.UserName == null)
            {
                _notyf.Error("Kullanıcı adı boş olamaz!");
                isSucceded = false;
            }

            if (model.Email == null)
            {
                _notyf.Error("E-Posta boş olamaz!");
                isSucceded = false;
            }

            if (!isSucceded)
            {
                return View(model);
            }

            var identityResult = await _userManager.CreateAsync(new() { UserName = model.UserName, Email = model.Email, FullName = model.FullName,  }, model.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);


                    _notyf.Error(item.Description);
                }

                return View(model);
            }

            // default olarak Uye rolü ekleme
            var user = await _userManager.FindByNameAsync(model.UserName);

            string roleName = model.IsAdmin ? "Admin" : "Uye";

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var role = new AppRole { Name = roleName };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, roleName);

            _notyf.Success("Üye Kaydı Yapılmıştır. Oturum Açınız");
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}