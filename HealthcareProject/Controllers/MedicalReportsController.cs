using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareProject.Controllers
{
    [Authorize]
    public class MedicalReportsController : Controller
    {
        private readonly healthcarev1Context _context;

        public MedicalReportsController(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: MedicalReports
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.MedicalReport.Include(m => m.Medicalreport).Include(m => m.Patient);
            return View(await healthcarev1Context.ToListAsync());
        }

        // GET: MedicalReports/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalReport = await _context.MedicalReport
                .Include(m => m.Medicalreport)
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicalreportName == id);
            if (medicalReport == null)
            {
                return NotFound();
            }

            return View(medicalReport);
        }

        // GET: MedicalReports/Create
        public IActionResult Create()
        {
            ViewData["MedicalreportId"] = new SelectList(_context.MedicalreportType, "MedicalreportId", "ReportType");
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy");
            return View();
        }

        // POST: MedicalReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicalreportName,ReportFile,MedicalreportId,PatientId")] MedicalReport medicalReports)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalReports);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicalreportId"] = new SelectList(_context.MedicalreportType, "MedicalreportId", "ReportType", medicalReports.MedicalreportId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy", medicalReports.PatientId);
            return View(medicalReports);
        }

        // GET: MedicalReports/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalReport = await _context.MedicalReport.FindAsync(id);
            if (medicalReport == null)
            {
                return NotFound();
            }
            ViewData["MedicalreportId"] = new SelectList(_context.MedicalreportType, "MedicalreportId", "ReportType", medicalReport.MedicalreportId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy", medicalReport.PatientId);
            return View(medicalReport);
        }

        // POST: MedicalReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MedicalreportName,ReportFile,MedicalreportId,PatientId")] MedicalReport medicalReports)
        {
            if (id != medicalReports.MedicalreportName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalReports);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalReportExists(medicalReports.MedicalreportName))
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
            ViewData["MedicalreportId"] = new SelectList(_context.MedicalreportType, "MedicalreportId", "ReportType", medicalReports.MedicalreportId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy", medicalReports.PatientId);
            return View(medicalReports);
        }

        // GET: MedicalReports/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalReport = await _context.MedicalReport
                .Include(m => m.Medicalreport)
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicalreportName == id);
            if (medicalReport == null)
            {
                return NotFound();
            }

            return View(medicalReport);
        }

        // POST: MedicalReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var medicalReport = await _context.MedicalReport.FindAsync(id);
            _context.MedicalReport.Remove(medicalReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalReportExists(string id)
        {
            return _context.MedicalReport.Any(e => e.MedicalreportName == id);
        }
    }
}
