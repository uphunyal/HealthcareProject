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
           
           /* var getdoctorid = _context.Nurse.Where(c => c.NurseEmail == User.Identity.Name).FirstOrDefault().DoctorId;
            
            var healthcarev1Context1 = _context.VisitRecord.Include(v => v.Doctor).Where(c=>c.DoctorId==getdoctorid)
                .Include(v => v.Patient);*/

            return View(await healthcarev1Context.ToListAsync());
        }

        // GET: VisitRecords/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitRecord = await _context.VisitRecord
                .Include(v => v.Doctor)
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.VisitDate == id);
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy");
            return View();
        }

        // POST: VisitRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitReason,Prescription,VisitDate,Visited,PatientId,DoctorId")] VisitRecord visitRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", visitRecord.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy", visitRecord.PatientId);
            return View(visitRecord);
        }

        // GET: VisitRecords/Edit/5
        public async Task<IActionResult> Edit( int patient_id, DateTime id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitRecord = await _context.VisitRecord.FindAsync(id, patient_id);
            if (visitRecord == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", visitRecord.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy", visitRecord.PatientId);
            return View(visitRecord);
        }

        // POST: VisitRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, int patient_id, [Bind("VisitReason,Prescription,VisitDate,Visited,PatientId,DoctorId")] VisitRecord visitRecord)
        {
            if (id != visitRecord.VisitDate && patient_id!=visitRecord.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitRecord);

                    await _context.SaveChangesAsync();


                    //Redirect to create invoice for copy
                    if (visitRecord.Visited == true)
                    {

                        await _context.SaveChangesAsync();
                        return RedirectToAction("Create", "Billings", visitRecord.PatientId);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitRecordExists(visitRecord.VisitDate))
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Allergy", visitRecord.PatientId);
            return View(visitRecord);
        }

        // GET: VisitRecords/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitRecord = await _context.VisitRecord
                .Include(v => v.Doctor)
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.VisitDate == id);
            if (visitRecord == null)
            {
                return NotFound();
            }

            return View(visitRecord);
        }

        // POST: VisitRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var visitRecord = await _context.VisitRecord.FindAsync(id);
            _context.VisitRecord.Remove(visitRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitRecordExists(DateTime id)
        {
            return _context.VisitRecord.Any(e => e.VisitDate == id);
        }
    }
}
