using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareProject.Models;

namespace HealthcareProject.Controllers
{
    public class MedicalreportTypesController : Controller
    {
        private readonly healthcarev1Context _context;

        public MedicalreportTypesController(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: MedicalreportTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MedicalreportType.ToListAsync());
        }

        // GET: MedicalreportTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalreportType = await _context.MedicalreportType
                .FirstOrDefaultAsync(m => m.MedicalreportId == id);
            if (medicalreportType == null)
            {
                return NotFound();
            }

            return View(medicalreportType);
        }

        // GET: MedicalreportTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicalreportTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicalreportId,ReportType")] MedicalreportType medicalreportType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalreportType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicalreportType);
        }

        // GET: MedicalreportTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalreportType = await _context.MedicalreportType.FindAsync(id);
            if (medicalreportType == null)
            {
                return NotFound();
            }
            return View(medicalreportType);
        }

        // POST: MedicalreportTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicalreportId,ReportType")] MedicalreportType medicalreportType)
        {
            if (id != medicalreportType.MedicalreportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalreportType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalreportTypeExists(medicalreportType.MedicalreportId))
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
            return View(medicalreportType);
        }

        // GET: MedicalreportTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalreportType = await _context.MedicalreportType
                .FirstOrDefaultAsync(m => m.MedicalreportId == id);
            if (medicalreportType == null)
            {
                return NotFound();
            }

            return View(medicalreportType);
        }

        // POST: MedicalreportTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalreportType = await _context.MedicalreportType.FindAsync(id);
            _context.MedicalreportType.Remove(medicalreportType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalreportTypeExists(int id)
        {
            return _context.MedicalreportType.Any(e => e.MedicalreportId == id);
        }
    }
}
