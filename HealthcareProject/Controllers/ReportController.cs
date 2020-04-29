using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthcareProject.Controllers
{
    public class ReportController : Controller
    {
        private readonly healthcarev1Context _context;
        // GET: Report

        public ReportController(healthcarev1Context context)
        {
            _context = context;

        }


        public ActionResult Index()
        {
            return View();
        }

        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("CEO"))
            {
                return NotFound();
            }

            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail");
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            var Date = DateTime.Parse(collection.ToArray()[0].Value).ToShortDateString();
            int docID = Int32.Parse(collection.ToArray()[1].Value);

            var apps = _context.Appointment.Where(app => app.ApptDate.ToShortDateString() == Date && app.DoctorId == docID);



            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}