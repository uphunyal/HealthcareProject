using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareProject.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly healthcarev1Context _context;
      

        public AppointmentsController(healthcarev1Context context)
        {
            _context = context;
          
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.Appointment.Include(a => a.Doctor).Include(a => a.Patient);
            return View(await healthcarev1Context.ToListAsync());
        }


       
        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.ApptId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail");
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApptId,ApptDate,AppointmentReason,PatientEmail,PatientId,VisitRecord,DoctorId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail", appointment.PatientId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApptId,ApptDate,AppointmentReason,PatientEmail,PatientId,VisitRecord,DoctorId")] Appointment appointment)
        {
            if (id != appointment.ApptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (appointment.VisitRecord==true)
                    {
                        var record = new VisitRecord { VisitDate = DateTime.Now, VisitReason = appointment.AppointmentReason, Prescription = "N/A", PatientId = (int)appointment.PatientId, DoctorId = appointment.DoctorId, Visited = false, Visitid= Guid.NewGuid().ToString() };
                        _context.Add(record);
                        await _context.SaveChangesAsync();

                    }
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.ApptId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.ApptId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.ApptId == id);
        }
    }
}
