using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareProject.Models;
using Microsoft.AspNetCore.Http;
using Rotativa.AspNetCore;

namespace HealthcareProject.Controllers
{
    //For Report Generation
    public class Report
    {
        public DateTime ReportDate;
 
        public string DoctorEmail;

    }


    public class DailyReportsController : Controller
    {
        private readonly healthcarev1Context _context;

        public DailyReportsController(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: DailyReports
        public async Task<IActionResult> Index()
        {
           

            return View(await _context.DailyReport.ToListAsync());
        }

        // GET: DailyReports/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!User.IsInRole("CEO"))
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }
            int.TryParse(id, out int intId);

            var doctor = _context.Doctor.Where(doc => doc.DoctorId == intId);



            var dailyReport = await _context.DailyReport
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (dailyReport == null)
            {
                return NotFound();
            }

            return View(dailyReport);
        }

        // GET: DailyReports/Create
        public IActionResult Create()
        {

            IEnumerable<SelectListItem> Doctors = _context.Doctor.Select(doc => new SelectListItem
            {
                Value = doc.DoctorEmail,
                Text = doc.DoctorEmail
            }) ;
            ViewBag.DoctorEmail = Doctors;

            return View();
        }

        // POST: DailyReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {

            var Date = DateTime.Parse(collection.ToArray()[0].Value).ToShortDateString();

            if (ModelState.IsValid)
            {

                var reportContext = _context.DailyReport.AsEnumerable().Where(rec => rec.ReportDate.ToShortDateString() == Date).ToList();

                return new ViewAsPdf("View",reportContext);
            }
            return NotFound();
        }

        // GET: DailyReports/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyReport = await _context.DailyReport.FindAsync(id);
            if (dailyReport == null)
            {
                return NotFound();
            }
            return View(dailyReport);
        }

        // POST: DailyReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ReportDate,NoPatients,ReportId,DoctorName,DailyIncome")] DailyReport dailyReport)
        {
            if (id != dailyReport.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyReportExists(dailyReport.ReportId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dailyReport);
        }

        // GET: DailyReports/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyReport = await _context.DailyReport
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (dailyReport == null)
            {
                return NotFound();
            }

            return View(dailyReport);
        }

        // POST: DailyReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dailyReport = await _context.DailyReport.FindAsync(id);
            _context.DailyReport.Remove(dailyReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyReportExists(string id)
        {
            return _context.DailyReport.Any(e => e.ReportId == id);
        }
    }
}
