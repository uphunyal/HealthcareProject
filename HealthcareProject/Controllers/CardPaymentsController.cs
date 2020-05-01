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
    [Authorize(Roles = "Staff,CEO")]
    public class CardPaymentsController : Controller
    {
        private readonly healthcarev1Context _context;

        public CardPaymentsController(healthcarev1Context context)
        {
            _context = context;
        }

       
        // GET: CardPayments
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.CardPayment.Include(c => c.Billing);
            return View(await healthcarev1Context.ToListAsync());
        }
/*
        // GET: CardPayments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardPayment = await _context.CardPayment
                .Include(c => c.Billing)
                .FirstOrDefaultAsync(m => m.ReferenceNo == id);
            if (cardPayment == null)
            {
                return NotFound();
            }

            return View(cardPayment);
        }

        // GET: CardPayments/Create
        public IActionResult Create()
        {
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId");
            return View();
        }

        // POST: CardPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo,PaymentAmount,PaymentDate,BillingId")] CardPayment cardPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", cardPayment.BillingId);
            return View(cardPayment);
        }

        // GET: CardPayments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardPayment = await _context.CardPayment.FindAsync(id);
            if (cardPayment == null)
            {
                return NotFound();
            }
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", cardPayment.BillingId);
            return View(cardPayment);
        }

        // POST: CardPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ReferenceNo,PaymentAmount,PaymentDate,BillingId")] CardPayment cardPayment)
        {
            if (id != cardPayment.ReferenceNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardPaymentExists(cardPayment.ReferenceNo))
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
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", cardPayment.BillingId);
            return View(cardPayment);
        }

        // GET: CardPayments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardPayment = await _context.CardPayment
                .Include(c => c.Billing)
                .FirstOrDefaultAsync(m => m.ReferenceNo == id);
            if (cardPayment == null)
            {
                return NotFound();
            }

            return View(cardPayment);
        }

        // POST: CardPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cardPayment = await _context.CardPayment.FindAsync(id);
            _context.CardPayment.Remove(cardPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardPaymentExists(string id)
        {
            return _context.CardPayment.Any(e => e.ReferenceNo == id);
        }*/
    }
}
