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
    public class VisitRecordsController : Controller
    {
        private readonly healthcarev1Context _context;

        public VisitRecordsController(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: VisitRecords
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.VisitRecord.Include(v => v.Doctor).Include(v => v.Patient);
            return View(await healthcarev1Context.ToListAsync());
        }
/*
        // GET: VisitRecords/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitRecord = await _context.VisitRecord
                .Include(v => v.Doctor)
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.Visitid == id);
            if (visitRecord == null)
            {
                return NotFound();
            }

            return View(visitRecord);
        }

        // GET: VisitRecords/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail");
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail");
            return View();
        }

        // POST: VisitRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitReason,Prescription,Visitid,VisitDate,Visited,PatientId,DoctorId")] VisitRecord visitRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", visitRecord.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail", visitRecord.PatientId);
            return View(visitRecord);
        }*/

        // GET: VisitRecords/Edit/5
        public async Task<IActionResult> Edit(string id, int pid)
        {
            if (id == null )
            {
                return NotFound();
            }

            var visitRecord = await _context.VisitRecord.FindAsync(id, pid);
            if (visitRecord == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", visitRecord.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail", visitRecord.PatientId);
            return View(visitRecord);
        }

        // POST: VisitRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VisitReason,Prescription,Visitid,VisitDate,Visited,PatientId,DoctorId")] VisitRecord visitRecord)
        {
            if (id != visitRecord.Visitid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitRecord);
                    await _context.SaveChangesAsync();
                    if (visitRecord.Visited == true)
                    {
                        //Clear appointment once patient visits the doctor
                        int patient_id = visitRecord.PatientId;
                        var appointment_context = _context.Appointment.Where(a => a.PatientId == patient_id).FirstOrDefault();
                        _context.Appointment.Remove(appointment_context);
                         await _context.SaveChangesAsync();

                        var record = new Billing { BillingAmount=50, BillingDate= DateTime.UtcNow, PatientId=visitRecord.PatientId, Paid= false };
                        _context.Add(record);
                        await _context.SaveChangesAsync();
                        //return RedirectToAction("Create", "Billings");
                        return RedirectToAction("Index","VisitRecords");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitRecordExists(visitRecord.Visitid))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", visitRecord.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail", visitRecord.PatientId);
            return View(visitRecord);
        }

       /* // GET: VisitRecords/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitRecord = await _context.VisitRecord
                .Include(v => v.Doctor)
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.Visitid == id);
            if (visitRecord == null)
            {
                return NotFound();
            }

            return View(visitRecord);
        }

        // POST: VisitRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var visitRecord = await _context.VisitRecord.FindAsync(id);
            _context.VisitRecord.Remove(visitRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
*/
        private bool VisitRecordExists(string id)
        {
            return _context.VisitRecord.Any(e => e.Visitid == id);
        }
    }
}
