﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sailing_Rocks.Models;
using Sailing_Rocks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;

namespace Sailing_Rocks.Controllers
{
    public class RockController : Controller
    {
        IRepository<Rock> rockRepo;
        public RockController(IRepository<Rock> rockRepo)
        {
            this.rockRepo = rockRepo;
        }

        //we are not displaying and index of rocks

        // GET: RockController
        //public ViewResult Index()
        //{
        //    return View();
        //}

        // GET: RockController/Details/5
        public ViewResult Details(int id)
        {
            var rock = rockRepo.GetById(id);
            return View(new RockLocationVM() { Rock = rock, Location = new Location() { RockId = id } });
        }

        // GET: RockController/Create
        public ViewResult Create(int userId) { 
        
            ViewBag.UserId = HttpContext.Session.GetInt32("userId");
          
            return View();
        }

        // POST: RockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rock model)
        {
            rockRepo.Create(model);
            return View(new Rock());
        }

        // GET: RockController/Edit/5
        public ActionResult Edit(int id)
        {
            var rock = rockRepo.GetById(id);
            var UserId = HttpContext.Session.GetInt32("userId");
            if(rock.UserId == UserId)
            {
                return View(rock);
            }
            else
            {
                return RedirectToAction("Details", "User", new { id = UserId });
            }

        }

        // POST: RockController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rock model)
        {
            rockRepo.Update(model);
            return View(model);
        }

        public JsonResult GetRockLocation(int id)
        {
            var rock = rockRepo.GetById(id);

            return Json(JsonConvert.SerializeObject(rock.Locations, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            


        }

        //users do not have ability to delete rocks

        // GET: RockController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: RockController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
