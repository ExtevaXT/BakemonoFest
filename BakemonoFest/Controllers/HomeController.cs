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

        public IActionResult MonsterList(int filter = 0)
        {
            List<Monster> monsters = mobileContext.Monsters.ToList();
            if (filter != 0) ViewBag.monsterList = monsters.Where(x => x.NominationId == filter);
            else ViewBag.monsterList = monsters;
            ViewBag.users = mobileContext.Users.ToList();
            List<Nomination> nominations = mobileContext.Nominations.ToList();
            nominations.Insert(0,new Nomination() { Id = 0, Name = "Все" });
            ViewBag.monsterTypes = mobileContext.MonsterTypes.ToList();
            var rates = mobileContext.Rates.ToList();
            foreach (var m in monsters)
            {
                List<Rate> monsterRates = rates.Where(x => x.MonsterId == m.Id) as List<Rate>;
                foreach (var r in monsterRates)
                {
                    m.Rating += r.Value;
                }
                
            }
            
            
            ViewBag.SelectNominations = new SelectList(nominations, "Id", "Name");

            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
