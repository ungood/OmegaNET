using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omega.Web.Models;
using Omega.Web.Services;

namespace Omega.Web.Controllers
{
    public class TextController : Controller
    {
        private readonly ITestService testService;

        public TextController(ITestService testService)
        {
            this.testService = testService;
        }

        //
        // GET: /Text/

        public ActionResult Index()
        {
            return View(new List<TextFileModel> {
                //new TextFileModel { Label = 'A', Text = "Testing" }
            });
        }

        //
        // GET: /Text/Details/5

        public ActionResult Details(string label)
        {
            return View();
        }

        //
        // GET: /Text/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Text/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Text/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Text/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Text/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Text/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
