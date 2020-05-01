using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareProject.Models;
using HealthcareProject.Services;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareProject.Controllers
{
 
    public class CheckPaymentsController : Controller
    {
        private readonly healthcarev1Context _context;

        public CheckPaymentsController(healthcarev1Context context)
        {
            _context = context;
        }

        // GET: CheckPayments
        [Authorize(Roles = "Staff, CEO")]
        public async Task<IActionResult> Index()
        {
            var healthcarev1Context = _context.CheckPayment.Include(c => c.Billing);
            return View(await healthcarev1Context.ToListAsync());
        }

        /* // GET: CheckPayments/Details/5
         public async Task<IActionResult> Details(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var checkPayment = await _context.CheckPayment
                 .Include(c => c.Billing)
                 .FirstOrDefaultAsync(m => m.CheckNo == id);
             if (checkPayment == null)
             {
                 return NotFound();
             }

             return View(checkPayment);
         }*/

        [Authorize(Roles = "Staff")]
        // GET: CheckPayments/Create
        public IActionResult Create(int billing_id)
        {
            ViewData["PaymentAmount"] = new SelectList(_context.Billing.Where(c=>c.BillingId==billing_id), "BillingId", "BillingAmount");
            ViewData["BillingId"] = new SelectList(_context.Billing.Where(c=>c.BillingId==billing_id), "BillingId", "BillingId");

            return View();
        }

        // POST: CheckPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Staff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckNo,PaymentAmount,PaymentDate,BillingId,ReceiveReceipt")] CheckPayment checkPayment)
        {
            if (ModelState.IsValid)

            {
                Console.WriteLine("Check Payment" + checkPayment.BillingId + checkPayment.PaymentAmount
                  );
                _context.Add(checkPayment);
                await _context.SaveChangesAsync();
                
                
                //Update Paid Status in invoice

                var billing = await _context.Billing
               .FirstOrDefaultAsync(m => m.BillingId == checkPayment.BillingId);
            
                billing.Paid = true;
                await _context.SaveChangesAsync();


                //Send receipt 
                if (checkPayment.ReceiveReceipt == true)
                {
                    var get_patient_id = _context.Billing.Where(c => c.BillingId == checkPayment.BillingId).FirstOrDefault().PatientId;
                    var get_patient_email = _context.Patient.Where(c => c.PatientId == get_patient_id).FirstOrDefault().PatientEmail;
                    var get_patient_name = _context.Patient.Where(c => c.PatientId == get_patient_id).FirstOrDefault().PatientName;



                    /* SendReceipt receipt = new SendReceipt();
                         receipt.Sendemail(get_patient_email, checkPayment.PaymentAmount, checkPayment.BillingId);*/


                    //Send an email
                    //Sending email, make a different class file for this.
                    var fromAddress = new MailAddress("phunyal.utsav1@gmail.com", "Healthcare Service");
                    var toAddress = new MailAddress(get_patient_email, get_patient_name);
                    string messagebody = "You paid your invoice of amount " + checkPayment.PaymentAmount + " and billing Id " + checkPayment.BillingId;
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

                    Console.WriteLine(get_patient_email);
                    Console.WriteLine(get_patient_name);



                }
                return View("CheckPaymentSuccessful");
            }
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", checkPayment.BillingId);
            return View("CheckPaymentSuccessful");
        }

        /*// GET: CheckPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkPayment = await _context.CheckPayment.FindAsync(id);
            if (checkPayment == null)
            {
                return NotFound();
            }
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", checkPayment.BillingId);
            return View(checkPayment);
        }

        // POST: CheckPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CheckNo,PaymentAmount,PaymentDate,BillingId,ReceiveReceipt")] CheckPayment checkPayment)
        {
            if (id != checkPayment.CheckNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckPaymentExists(checkPayment.CheckNo))
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
            ViewData["BillingId"] = new SelectList(_context.Billing, "BillingId", "BillingId", checkPayment.BillingId);
            return View(checkPayment);
        }

        // GET: CheckPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkPayment = await _context.CheckPayment
                .Include(c => c.Billing)
                .FirstOrDefaultAsync(m => m.CheckNo == id);
            if (checkPayment == null)
            {
                return NotFound();
            }

            return View(checkPayment);
        }

        // POST: CheckPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkPayment = await _context.CheckPayment.FindAsync(id);
            _context.CheckPayment.Remove(checkPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckPaymentExists(int id)
        {
            return _context.CheckPayment.Any(e => e.CheckNo == id);
        }*/
    }
}
