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
        public static DateTime Now { get; }

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

            DailyReport dailyReport;

            string localdate = DateTime.Today.ToShortDateString();

            //Doctors that served that day that shows their 
            //var doctor_info = _context.VisitRecord
            //    .FromSqlRaw($"Select DoctorId where  VisitDate = \'{0}\' ", localdate).ToArray();

            var groupByDoc = _context.VisitRecord.AsEnumerable().Where(rec => rec.VisitDate.ToShortDateString() == localdate).GroupBy(rec => rec.DoctorId);



            //var doctor_key = _context.VisitRecord
            //    .Where(c => c.VisitDate < DateTime.Now)
            //    .Include(c => c.Doctor).Distinct().ToList();


            //var visited_patient = _context.VisitRecord
            //     .Where(c => c.VisitDate < DateTime.Now)
            //    .Include(c => c.DoctorId);

            ///*   var doctor_id_list = visited_patient.Include(c => c.DoctorId).Distinct
            //       ().ToList();*/

            //var patient_list = _context.Billing
            //   .Where(c => c.BillingDate < DateTime.Now).ToList();

            ////Psuedo code
            ////Get list of doctors that serve patients that day
            ////Get total amount of bill generated for each patient id for that day
            ////Get total amount generated

            //IDictionary<int, double> dict = new Dictionary<int, double>();
            //foreach (var x in patient_list)
            //{
            //    if (dict.ContainsKey(x.PatientId))
            //    {
            //        double current_amount = dict[x.PatientId];
            //        current_amount += x.BillingAmount;
            //        dict[x.PatientId] = current_amount;
            //    }
            //    else
            //    {
            //        dict[x.PatientId] = x.BillingAmount;
            //    }
            //}

            //foreach (var x in doctor_key)
            //{
            //    int report_id = 1;
            //    var patient_list_for_each_doctor = visited_patient.Include(c => c.PatientId).Where(c => c.DoctorId == x.DoctorId).Where(c => c.VisitDate == localdate).ToArray();
            //    int no_of_patient = visited_patient.Include(c => c.PatientId).Where(c => c.DoctorId == x.DoctorId).Where(c => c.VisitDate == localdate).Count();
            //    string doctor_name = x.Doctor.DoctorName;
            //    double earnedamount = 0;
            //    foreach (var y in patient_list_for_each_doctor)
            //    {

            //        if (dict.ContainsKey(y.PatientId))
            //        {
            //            earnedamount += dict[y.PatientId];
            //        }

            //    }
            //   /* var addreport = new DailyReport { ReportId = report_id, ReportDate = DateTime.Today, DailyIncome = earnedamount, DoctorName = doctor_name, NoPatients = no_of_patient };
            //    _context.Add(addreport);
            //    _context.SaveChanges();*/
            //}

        }
    }
}