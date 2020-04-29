using HealthcareProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareProject
{
    public class JobScheduling
    {
        private readonly healthcarev1Context _context;

       

        public JobScheduling(healthcarev1Context context)
        {
            _context = context;
        }
        

        public void ClearAppointment()
        {

            string localdate = DateTime.Today.ToShortDateString();
            var appt = _context.Appointment;
            foreach (var entry in appt)
            {
                Console.WriteLine(entry.ApptDate.ToShortDateString());
            }

            var appts = _context.Appointment.AsEnumerable().Where(app => app.ApptDate.ToShortDateString() == localdate).ToList();

            foreach (var record in appts)
            {
                _context.Appointment.Remove(record);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error from Clear Appointment.");
                Console.WriteLine(e.InnerException);
                throw;
            }

        }
        public void GenerateDailyReport()
        {



            string localdate = DateTime.Today.ToShortDateString();


            var docs = _context.VisitRecord.AsEnumerable().Where(rec => rec.VisitDate.ToShortDateString() == localdate).Select(rec => rec.DoctorId).Distinct().ToList();


            foreach (var doc in docs)
            {



                int NumPatients = (_context.VisitRecord.Where(rec => rec.DoctorId == doc)).Count();
                string DocName = _context.Doctor.Where(d => d.DoctorId == doc).Select(d => d.DoctorName).FirstOrDefault();
                DateTime date = DateTime.Today;

                var pats = _context.VisitRecord.Where(d => d.DoctorId == doc).Select(rec => rec.PatientId).Distinct().ToList();

                double Income = 0;

                foreach (var pat in pats)
                {
                    Income += _context.Billing.AsEnumerable().Where(bill => bill.PatientId == pat && bill.BillingDate.ToShortDateString() == localdate).Select(bill => bill.BillingAmount).Sum();

                }

                var dailyReport = new DailyReport
                {
                    DailyIncome = Income,
                    NoPatients = NumPatients,
                    DoctorName = DocName,
                    ReportDate = date,
                    ReportId = Guid.NewGuid().ToString()
                };


                _context.DailyReport.Add(dailyReport);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception d)
                {
                    Console.WriteLine("From DailyReport");
                    Console.WriteLine(d.InnerException);

                }

            }

        }

        public void GenerateMonthlyReport()
        {
            var rand = new Random();
            int Month = DateTime.Today.Month;


            var groupByDoc = _context.VisitRecord.AsEnumerable().Where(rec => Int32.Parse((rec.VisitDate.ToShortDateString().Split("/"))[0]) == Month).Select(rec => rec.DoctorId).Distinct().ToList();

            for (int i = 0; i<groupByDoc.Count(); i++)
            {

                string DocName = _context.Doctor.Where(rec => rec.DoctorId == groupByDoc.ElementAt(i)).Select(rec => rec.DoctorName).FirstOrDefault();

                int NumPatients = (_context.DailyReport.Where(rec => rec.DoctorName == DocName)).Sum(rec => rec.NoPatients);

                DateTime date = DateTime.Today;

                double Income = _context.DailyReport.AsEnumerable().Where(d => d.DoctorName == DocName && Int32.Parse((d.ReportDate.ToShortDateString().Split("/"))[0]) == Month).Sum(rec => rec.DailyIncome);

                var monthlyReport = new MonthlyReport
                {
                    Report_Id = rand.Next().ToString(),
                    NoPatients = NumPatients,
                    MonthlyIncome = Income,
                    ReportMonth = DateTime.Today,
                    DoctorName = DocName

                };


              _context.MonthlyReport.Add(monthlyReport);

                //try
                //{
                    _context.SaveChangesAsync();
                
               // }
                //catch (Exception d)
                //{
                //    Console.WriteLine("From DailyReport");
                //    Console.WriteLine(d.InnerException);

                //}

            }

           
        }
    }
}