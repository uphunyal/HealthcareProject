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
    public class DoctorAvailabilitiesController : Controller
    {
        private readonly healthcarev1Context _context;

        public DoctorAvailabilitiesController(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: DoctorAvailabilities
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.DoctorAvailability.Include(d => d.Doctor);
            return View(await healthcarev1Context.ToListAsync());
        }

        // GET: DoctorAvailabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorAvailability = await _context.DoctorAvailability
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.AvailabilityId == id);
            if (doctorAvailability == null)
            {
                return NotFound();
            }

            return View(doctorAvailability);
        }

        // GET: DoctorAvailabilities/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail");
            return View();
        }

        // POST: DoctorAvailabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,AvailableTime,AvailabilityId")] DoctorAvailability doctorAvailability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorAvailability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", doctorAvailability.DoctorId);
            return View(doctorAvailability);
        }

        // GET: DoctorAvailabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorAvailability = await _context.DoctorAvailability.FindAsync(id);
            if (doctorAvailability == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", doctorAvailability.DoctorId);
            return View(doctorAvailability);
        }

        // POST: DoctorAvailabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,AvailableTime,AvailabilityId")] DoctorAvailability doctorAvailability)
        {
            if (id != doctorAvailability.AvailabilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorAvailability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorAvailabilityExists(doctorAvailability.AvailabilityId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", doctorAvailability.DoctorId);
            return View(doctorAvailability);
        }

        // GET: DoctorAvailabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorAvailability = await _context.DoctorAvailability
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.AvailabilityId == id);
            if (doctorAvailability == null)
            {
                return NotFound();
            }

            return View(doctorAvailability);
        }

        // POST: DoctorAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorAvailability = await _context.DoctorAvailability.FindAsync(id);
            _context.DoctorAvailability.Remove(doctorAvailability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorAvailabilityExists(int id)
        {
            return _context.DoctorAvailability.Any(e => e.AvailabilityId == id);
        }
    }
}
