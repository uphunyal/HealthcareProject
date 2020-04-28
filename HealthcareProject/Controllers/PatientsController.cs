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
    public class PatientsController : Controller
    {
        private readonly healthcarev1Context _context;

        public PatientsController(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Staff"))
            {
                var healthcareContext = _context.Patient;
                return View(await healthcareContext.ToListAsync());
            }

            else if (User.IsInRole("Nurse"))
            {
                //Write Query

                var patients = _context.Patient;
                return View(await patients.ToListAsync());
            }



            else
            {
                return NotFound();
            }
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            //Only Staff can create a Patient
            //If a patient tries to register himself, he is registered directly without coming to this controller.
            if (User.IsInRole("Staff"))
            { return View(); }

            else
            {
                return NotFound();
            }
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientName,PatientEmail,PatientAddress,Phone,Ssn,Weight,Height,Insurance,Bloodpressure,Pulse,AppointmentMisused,PatientId,Allergy")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientName,PatientEmail,PatientAddress,Phone,Ssn,Weight,Height,Insurance,Bloodpressure,Pulse,AppointmentMisused,PatientId,Allergy")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
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
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Staff"))
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var patient = await _context.Patient.FindAsync(id);

            //Checking to see if the patient the person is trying to delete has active appointments
            var query = _context.Patient.Where(p => p.PatientId == id).FirstOrDefault();
            var appointment = _context.Appointment.Where(pat => pat.PatientEmail == query.PatientEmail).FirstOrDefault();

            if (appointment == null)
            {
                try
                {
                    _context.Patient.Remove(patient);
                    await _context.SaveChangesAsync();
                }

                catch (Exception e)
                {
                    TempData["Error"] = "Patients with active appointments cannot be deleted.";
                    Console.WriteLine(e.Message.ToString());
                }


                return RedirectToAction(nameof(Index));
            }

            else
            {
                TempData["Error"] = "Patients with active appointments cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.PatientId == id);
        }
    }
}
