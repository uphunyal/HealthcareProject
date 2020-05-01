using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareProject.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareProject.Controllers
{
   
    public class CashPaymentsController : Controller
    {
        private readonly healthcarev1Context _context;

        public CashPaymentsController(healthcarev1Context context)
        {
            _context = context;
        }
        [Authorize(Roles = "Staff, CEO")]
        // GET: CashPayments
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.CashPayment.Include(c => c.Billing);
            return View(await healthcarev1Context.ToListAsync());
        }

     /*  //GET: CashPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayment
                .Include(c => c.Billing)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (cashPayment == null)
            {
                return NotFound();
            }

            return View(cashPayment);
        }*/
        [Authorize(Roles = "Staff")]

        // GET: CashPayments/Create
        public IActionResult Create(int billing_id)
        {
            ViewData["PaymentAmount"] = new SelectList(_context.Billing.Where(c => c.BillingId == billing_id), "BillingId", "BillingAmount");
            ViewData["BillingId"] = new SelectList(_context.Billing.Where(c => c.BillingId == billing_id), "BillingId", "BillingId");

            return View();
        }

        // POST: CashPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Staff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentDate,PaymentId,PaymentAmount,BillingId,ReceiveReceipt")] CashPayment cashPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashPayment);
                await _context.SaveChangesAsync();

                //Change status to paid
                var billing = await _context.Billing
             .FirstOrDefaultAsync(m => m.BillingId == cashPayment.BillingId);

                billing.Paid = true;
                await _context.SaveChangesAsync();
                //Send receipt 
                if (cashPayment.ReceiveReceipt == true)
                {
                    var get_patient_id = _context.Billing.Where(c => c.BillingId == cashPayment.BillingId).FirstOrDefault().PatientId;
                    var get_patient_email = _context.Patient.Where(c => c.PatientId == get_patient_id).FirstOrDefault().PatientEmail;
                    var get_patient_name = _context.Patient.Where(c => c.PatientId == get_patient_id).FirstOrDefault().PatientName;



                    /**//* SendReceipt receipt = new SendReceipt();
                         receipt.Sendemail(get_patient_email, checkPayment.PaymentAmount, checkPayment.BillingId);*//**/


                    //Send an email
                    //Sending email, make a different class file for this.
                    var fromAddress = new MailAddress("phunyal.utsav1@gmail.com", "Healthcare Service");
                    var toAddress = new MailAddress(get_patient_email, get_patient_name);
                    string messagebody = "You paid your invoice of amount " + cashPayment.PaymentAmount + " and billing Id " + cashPayment.BillingId;
                    const string subject = "Receipt of Paymment";
                    string body = messagebody;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential(fromAddress.Address, "xrtwdhjqxqvsurfn")
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return View("CashPaymentSuccessful");
            }
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", cashPayment.BillingId);
            return View("CashPaymentUnSuccessful");
        }
/*
        // GET: CashPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayment.FindAsync(id);
            if (cashPayment == null)
            {
                return NotFound();
            }
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", cashPayment.BillingId);
            return View(cashPayment);
        }

        // POST: CashPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentDate,PaymentId,PaymentAmount,BillingId,ReceiveReceipt")] CashPayment cashPayment)
        {
            if (id != cashPayment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashPaymentExists(cashPayment.PaymentId))
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
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", cashPayment.BillingId);
            return View(cashPayment);
        }

        // GET: CashPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayment
                .Include(c => c.Billing)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (cashPayment == null)
            {
                return NotFound();
            }

            return View(cashPayment);
        }

        // POST: CashPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashPayment = await _context.CashPayment.FindAsync(id);
            _context.CashPayment.Remove(cashPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashPaymentExists(int id)
        {
            return _context.CashPayment.Any(e => e.PaymentId == id);
        }*/
    }
}
