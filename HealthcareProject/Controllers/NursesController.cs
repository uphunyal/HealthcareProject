using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareProject.Models;
using Microsoft.AspNetCore.Identity;

namespace HealthcareProject.Controllers
{
    public class NursesController : Controller
    {
        private readonly healthcarev1Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;

        public NursesController(healthcarev1Context context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
        }

        // GET: Nurses
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.Nurse.Include(n => n.Doctor);
            return View(await healthcarev1Context.ToListAsync());
        }

        // GET: Nurses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse
                .Include(n => n.Doctor)
                .FirstOrDefaultAsync(m => m.NurseId == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // GET: Nurses/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail");
            return View();
        }

        // POST: Nurses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NurseName,Salary,NurseEmail,NurseId,DoctorId")] Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nurse);
                await _context.SaveChangesAsync();
                
                // Create nurse account using the email and default password

                var nurseaccount = new IdentityUser
                {
                    UserName = nurse.NurseEmail,
                    Email = nurse.NurseEmail,
                    EmailConfirmed = true,


                };

                var user = await _userManager.FindByEmailAsync(nurse.NurseEmail);
                if (user == null)
                {
                    var createpoweruser = await _userManager.CreateAsync(nurseaccount, "Superuser1!");
                    if (createpoweruser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(nurseaccount, "Nurse");
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", nurse.DoctorId);
            return View(nurse);
        }

        // GET: Nurses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", nurse.DoctorId);
            return View(nurse);
        }

        // POST: Nurses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NurseName,Salary,NurseEmail,NurseId,DoctorId")] Nurse nurse)
        {
            if (id != nurse.NurseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nurse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NurseExists(nurse.NurseId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorEmail", nurse.DoctorId);
            return View(nurse);
        }

        // GET: Nurses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse
                .Include(n => n.Doctor)
                .FirstOrDefaultAsync(m => m.NurseId == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // POST: Nurses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nurse = await _context.Nurse.FindAsync(id);
            _context.Nurse.Remove(nurse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NurseExists(int id)
        {
            return _context.Nurse.Any(e => e.NurseId == id);
        }
    }
}
