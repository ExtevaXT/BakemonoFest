using BakemonoFest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BakemonoFest.Controllers
{
    public class HomeController : Controller
    {
        
        MobileContext mobileContext;
        public HomeController(MobileContext mobileContext)
        {
            this.mobileContext = mobileContext;  
        }

        public IActionResult Index(User user = null)
        {
            ViewBag.user = user;
            return View();

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string login, string password, string name)
        {
            User reg = new User() { Login = login, Password = password, Name=name };
            mobileContext.Users.Add(reg);
            mobileContext.SaveChanges();
            return RedirectToAction("Login");

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            User user = mobileContext.Users.ToList().Where(x => x.Login == login && x.Password == password).FirstOrDefault();
            if (user == null) return View();
            else return RedirectToAction("Index", user);


        }


        public IActionResult MonsterAdd()
        {
            return View();
        }

        public IActionResult MonsterList()
        {
            ViewBag.monsterList = mobileContext.Monsters.ToList();
            ViewBag.users = mobileContext.Users.ToList();
            List<Nomination> nominations = mobileContext.Nominations.ToList();
            ViewBag.monsterTypes = mobileContext.MonsterTypes.ToList();
            SelectList test = new SelectList(nominations, "Id","Name");
            ViewBag.SelectNominations = test;

            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
