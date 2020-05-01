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
    [Authorize(Roles ="Patient, Staff")]
    public class Appointments1Controller : Controller
    {
        private readonly healthcarev1Context _context;

        public Appointments1Controller(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: Appointments1
        public async Task<IActionResult> Index()
        {
           
            var healthcarev1Context = _context.Appointment.Include(a => a.Doctor).Include(a => a.Patient);
            

            return View(await healthcarev1Context.ToListAsync());
           
        }
        
        // GET: Appointments1/Details/5
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



        public IActionResult Create(int? doctor_id, DateTime? appt_date)
        {
            if (!(doctor_id == null))
            {
                var searchContext = from p in _context.DoctorUnavailability
                                    where p.DoctorId == doctor_id && p.Unavailable.Date == appt_date
                                    select p.Unavailable;
                var unavailabletimes = searchContext.ToList();

                var getdoctoravailabletimes = from p in _context.DoctorAvailability
                                              where p.DoctorId == doctor_id && p.AvailableTime.Date == appt_date
                                              select p.AvailableTime;
                var availabletimes = getdoctoravailabletimes.ToList();
                foreach (var x in unavailabletimes)
                {
                    Console.WriteLine("The available times : " + x);
                }
                foreach (var x in availabletimes)
                {
                    Console.WriteLine("The available times : " + x);
                }
                //Get the time patient can make appointment for
                var list3 = availabletimes.Except(unavailabletimes).ToList();

                foreach (var x in list3)
                {
                    Console.WriteLine("The available times : " + x);
                }
                Console.WriteLine("Listcount" + list3.Count());
                if ((list3.Count() != 0))
                {
                    Console.WriteLine("List is not empty");
                    ViewData["DoctorId"] = new SelectList(_context.Doctor.Where(c => c.DoctorId == doctor_id), "DoctorId", "DoctorName");
                    ViewData["ApptDate"] = new SelectList(list3);
                    ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail");
                    return View();
                }
                else
                {
                    Console.WriteLine("List is  empty");

                    return View("NoAppointment");
                }
            }

            return View();
        }
        // POST: Appointments1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApptId,ApptDate,AppointmentReason,PatientEmail,PatientId,VisitRecord,DoctorId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var findbyemail = _context.Appointment.Where(c => c.PatientEmail == User.Identity.Name).Count();

                //If the email is found, reject creating the appointment and redirect to the page that says unsuccessful

                if (findbyemail != 0)
                {
                    return View("AppointmentExists");
                }
                //Add the user to misused user database
                
                if (User.IsInRole("Patient"))
                {
                    if (!UserExists(User.Identity.Name))
                    {
                        
                        var newuser = new MisusedUser { UserEmail = User.Identity.Name, Misusedcount = 0 };
                        _context.MisusedUser.Add(newuser);
                        await _context.SaveChangesAsync();
                    }
                }
                _context.Add(appointment);
                await _context.SaveChangesAsync();

                //Remove from available time
                var appointment_context = _context.DoctorAvailability.Where(a => a.AvailableTime == appointment.ApptDate && a.DoctorId == appointment.DoctorId).FirstOrDefault();

                _context.DoctorAvailability.Remove(appointment_context);
                await _context.SaveChangesAsync();

                //Add to unavailable time

                var addtime = new DoctorUnavailability { DoctorId = appointment.DoctorId, Unavailable = appointment.ApptDate };
                _context.DoctorUnavailability.Add(addtime);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail", appointment.PatientId);
            return View(appointment);
        }

        /*// GET: Appointments1/Edit/5
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

        // POST: Appointments1/Edit/5
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
                    if (appointment.VisitRecord == true) {
                        var addtovisit = new VisitRecord { DoctorId = appointment.DoctorId, PatientId = (int)appointment.PatientId, Prescription = "N/A", VisitDate = appointment.ApptDate, VisitReason = appointment.AppointmentReason, Visited = false, Visitid = Guid.NewGuid().ToString() };
                        _context.Add(addtovisit);
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientEmail", appointment.PatientId);
            return View(appointment);
        }*/

        // GET: Appointments1/Delete/5
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

        // POST: Appointments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);


            var appointment1 = from p in _context.Appointment
                                where p.ApptId==id
                                select p;
            var apptid = appointment1.ToList();


            var misusecount = from p in _context.MisusedUser
                                where p.UserEmail==User.Identity.Name
                                select p.Misusedcount;
            int count = misusecount.FirstOrDefault();


            //Update misuse count or deny permission to cancel appointment based on misuse count.
            if (User.IsInRole("Patient"))
            {

                if (count > 5)
                {
                    return View("PermissionDenied");
                }
                else
                {
                    count += 1;
                    var findemail = await _context.MisusedUser.FindAsync(User.Identity.Name);
                    findemail.Misusedcount = count;
                    await _context.SaveChangesAsync();

                }
            }
           

            foreach (var x in apptid)
            {
                //Remove from unvailable time
                var appointment_context = _context.DoctorUnavailability.Where(a => a.Unavailable == x.ApptDate && a.DoctorId == x.DoctorId).FirstOrDefault();
                _context.DoctorUnavailability.Remove(appointment_context);
                await _context.SaveChangesAsync();

                //Add to available time

                var addtime = new DoctorAvailability { DoctorId = x.DoctorId, AvailableTime = x.ApptDate };
                _context.DoctorAvailability.Add(addtime);
                await _context.SaveChangesAsync();
            }
           

         
           
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
             
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.ApptId == id);
        }
        private bool UserExists(string id)
        {
            return _context.MisusedUser.Any(e => e.UserEmail == id);
        }
    }
}
