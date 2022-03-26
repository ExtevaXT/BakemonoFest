using BakemonoFest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BakemonoFest.Controllers
{
    public class HomeController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        MobileContext mobileContext;
        public HomeController(MobileContext mobileContext, IWebHostEnvironment _appEnvironment)
        {
            this.mobileContext = mobileContext;
            this._appEnvironment = _appEnvironment;

        }

        public IActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public IActionResult MonsterAdd()
        {
            List<Nomination> nominations = mobileContext.Nominations.ToList();
            List<MonsterType> monsterTypes = mobileContext.MonsterTypes.ToList();
            ViewBag.SelectNominations = new SelectList(nominations, "Id", "Name");
            ViewBag.SelectMonsterTypes = new SelectList(monsterTypes, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult MonsterAdd(string name, string master, DateTime birthday, IFormFile photo, string job, int nomination, int monsterType)
        {
            if (photo != null)

            {
                // путь к папке Files
                string path = "/img/" + photo.FileName;
                // сохраняем файл в папку Images в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    photo.CopyTo(fileStream); //копируем файл в папку
                }
                Monster added = new Monster();
                added.Master = master;
                added.Job = job;
                added.Name = name;
                added.NominationId = nomination;
                added.MonsterTypeId = monsterType;
                added.Photo = path;
                added.Birthday = birthday;
                mobileContext.Monsters.Add(added);
                mobileContext.SaveChanges();
                
            }
            return RedirectToAction("Index");

        }

        public IActionResult MonsterList(int filter = 0, int rate=0, int monster = 0 )
        {
            if(rate!=0 && monster != 0)
            {
                Rate rateAdd = new Rate();
                rateAdd.MonsterId = monster;
                rateAdd.Value = rate;
                mobileContext.Rates.Add(rateAdd);
            }
            
            mobileContext.SaveChanges();
            List<Monster> monsters = mobileContext.Monsters.ToList();
            if (filter != 0) ViewBag.monsterList = monsters.Where(x => x.NominationId == filter);
            else ViewBag.monsterList = monsters;
            List<Nomination> nominations = mobileContext.Nominations.ToList();
            nominations.Insert(0,new Nomination() { Id = 0, Name = "Все" });
            ViewBag.monsterTypes = mobileContext.MonsterTypes.ToList();
            var rates = mobileContext.Rates.ToList();
            
            foreach (var m in monsters)
            {
                List<Rate> monsterRates = rates.Where(x => x.MonsterId == m.Id).ToList();
                double rFull = 0;
                int rCount = 0;
                foreach (var r in monsterRates)
                {
                    rFull += r.Value;
                    rCount++;
                }
                if (rCount != 0)
                    m.Rating = Math.Round(rFull / rCount, 2);

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
