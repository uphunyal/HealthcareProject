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
    [Authorize(Roles="Staff")]
    public class DoctorUnavailabilitiesController : Controller
    {
        private readonly healthcarev1Context _context;

        public DoctorUnavailabilitiesController(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: DoctorUnavailabilities
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.DoctorUnavailability.Include(d => d.Doctor);
            return View(await healthcarev1Context.ToListAsync());
        }

        // GET: DoctorUnavailabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorUnavailability = await _context.DoctorUnavailability
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.UnavailabilityId == id);
            if (doctorUnavailability == null)
            {
                return NotFound();
            }

            return View(doctorUnavailability);
        }

        // GET: DoctorUnavailabilities/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail");
            return View();
        }

        // POST: DoctorUnavailabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,Unavailable,UnavailabilityId")] DoctorUnavailability doctorUnavailability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorUnavailability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", doctorUnavailability.DoctorId);
            return View(doctorUnavailability);
        }

        // GET: DoctorUnavailabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorUnavailability = await _context.DoctorUnavailability.FindAsync(id);
            if (doctorUnavailability == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", doctorUnavailability.DoctorId);
            return View(doctorUnavailability);
        }

        // POST: DoctorUnavailabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,Unavailable,UnavailabilityId")] DoctorUnavailability doctorUnavailability)
        {
            if (id != doctorUnavailability.UnavailabilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorUnavailability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorUnavailabilityExists(doctorUnavailability.UnavailabilityId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", doctorUnavailability.DoctorId);
            return View(doctorUnavailability);
        }

        // GET: DoctorUnavailabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorUnavailability = await _context.DoctorUnavailability
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.UnavailabilityId == id);
            if (doctorUnavailability == null)
            {
                return NotFound();
            }

            return View(doctorUnavailability);
        }

        // POST: DoctorUnavailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorUnavailability = await _context.DoctorUnavailability.FindAsync(id);
            _context.DoctorUnavailability.Remove(doctorUnavailability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorUnavailabilityExists(int id)
        {
            return _context.DoctorUnavailability.Any(e => e.UnavailabilityId == id);
        }
    }
}
