using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HealthcareProject.Models
{
    public partial class healthcarev1Context : DbContext
    {
        public healthcarev1Context()
        {
        }

        public healthcarev1Context(DbContextOptions<healthcarev1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Billing> Billing { get; set; }
        public virtual DbSet<CardPayment> CardPayment { get; set; }
        public virtual DbSet<CashPayment> CashPayment { get; set; }
        public virtual DbSet<CheckPayment> CheckPayment { get; set; }
        public virtual DbSet<DailyReport> DailyReport { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        public virtual DbSet<DoctorUnavailability> DoctorUnavailability { get; set; }
        public virtual DbSet<MedicalReport> MedicalReport { get; set; }
        public virtual DbSet<MedicalreportType> MedicalreportType { get; set; }
        public virtual DbSet<MisusedUser> MisusedUser { get; set; }
        public virtual DbSet<MonthlyReport> MonthlyReport { get; set; }
        public virtual DbSet<Nurse> Nurse { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<VisitRecord> VisitRecord { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.ApptId)
                    .HasName("PK__Appointm__E43EE99683657EA9");

                entity.HasIndex(e => e.PatientEmail)
                    .HasName("UQ__Appointm__F6D870187B6FF196")
                    .IsUnique();

                entity.Property(e => e.ApptId).HasColumnName("appt_id");

                entity.Property(e => e.AppointmentReason)
                    .HasColumnName("appointment_reason")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApptDate)
                    .HasColumnName("appt_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.PatientEmail)
                    .IsRequired()
                    .HasColumnName("patient_email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.VisitRecord).HasColumnName("visit_record");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__docto__34C8D9D1");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__Appointme__patie__33D4B598");
            });

            modelBuilder.Entity<Billing>(entity =>
            {
                entity.Property(e => e.BillingId).HasColumnName("billing_id");

                entity.Property(e => e.BillingAmount).HasColumnName("billing_amount");

                entity.Property(e => e.BillingDate)
                    .HasColumnName("billing_date")
                    .HasColumnType("date");

                entity.Property(e => e.Paid).HasColumnName("paid");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Billing)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Billing__patient__300424B4");
            });

            modelBuilder.Entity<CardPayment>(entity =>
            {
                entity.HasKey(e => e.ReferenceNo)
                    .HasName("PK__Card_Pay__8E861397E3D35FD9");

                entity.ToTable("Card_Payment");

                entity.Property(e => e.ReferenceNo)
                    .HasColumnName("reference_no")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BillingId).HasColumnName("billing_id");

                entity.Property(e => e.PaymentAmount).HasColumnName("payment_amount");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("payment_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Billing)
                    .WithMany(p => p.CardPayment)
                    .HasForeignKey(d => d.BillingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Card_Paym__billi__619B8048");
            });

            modelBuilder.Entity<CashPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK__Cash_Pay__ED1FC9EAA570F0BE");

                entity.ToTable("Cash_Payment");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.BillingId).HasColumnName("billing_id");

                entity.Property(e => e.PaymentAmount).HasColumnName("payment_amount");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("payment_date")
                    .HasColumnType("date");

                entity.Property(e => e.ReceiveReceipt).HasColumnName("receive_receipt");

                entity.HasOne(d => d.Billing)
                    .WithMany(p => p.CashPayment)
                    .HasForeignKey(d => d.BillingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cash_Paym__billi__6477ECF3");
            });

            modelBuilder.Entity<CheckPayment>(entity =>
            {
                entity.HasKey(e => e.CheckNo)
                    .HasName("PK__Check_Pa__C0EA50AF7A31AC3B");

                entity.ToTable("Check_Payment");

                entity.Property(e => e.CheckNo)
                    .HasColumnName("check_no")
                    .ValueGeneratedNever();

                entity.Property(e => e.BillingId)
                    .HasColumnName("billing_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PaymentAmount).HasColumnName("payment_amount");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("payment_date")
                    .HasColumnType("date");

                entity.Property(e => e.ReceiveReceipt).HasColumnName("receive_receipt");

                entity.HasOne(d => d.Billing)
                    .WithMany(p => p.CheckPayment)
                    .HasForeignKey(d => d.BillingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Check_Pay__billi__5EBF139D");
            });

            modelBuilder.Entity<DailyReport>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__daily_re__779B7C58D7D7300C");

                entity.ToTable("daily_report");

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DailyIncome).HasColumnName("daily_income");

                entity.Property(e => e.DoctorName)
                    .IsRequired()
                    .HasColumnName("doctor_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NoPatients).HasColumnName("no_patients");

                entity.Property(e => e.ReportDate)
                    .HasColumnName("report_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasIndex(e => e.DoctorEmail)
                    .HasName("UQ__Doctor__2046D6B53E341799")
                    .IsUnique();

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.DoctorEmail)
                    .IsRequired()
                    .HasColumnName("doctor_email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorName)
                    .IsRequired()
                    .HasColumnName("doctor_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<DoctorAvailability>(entity =>
            {
                entity.HasKey(e => e.AvailabilityId)
                    .HasName("PK__Doctor_a__86E3A801826D3B8A");

                entity.ToTable("Doctor_availability");

                entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");

                entity.Property(e => e.AvailableTime)
                    .HasColumnName("available_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorAvailability)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctor_av__docto__787EE5A0");
            });

            modelBuilder.Entity<DoctorUnavailability>(entity =>
            {
                entity.HasKey(e => e.UnavailabilityId)
                    .HasName("PK__Doctor_u__9B3B44342AF5EFFA");

                entity.ToTable("Doctor_unavailability");

                entity.Property(e => e.UnavailabilityId).HasColumnName("unavailability_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Unavailable)
                    .HasColumnName("unavailable")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorUnavailability)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctor_un__docto__7D439ABD");
            });

            modelBuilder.Entity<MedicalReport>(entity =>
            {
                entity.HasKey(e => e.MedicalreportName)
                    .HasName("PK__Medical___4756A6E81E75ACC1");

                entity.ToTable("Medical_Report");

                entity.Property(e => e.MedicalreportName)
                    .HasColumnName("medicalreport_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MedicalreportId).HasColumnName("medicalreport_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.ReportFile)
                    .IsRequired()
                    .HasColumnName("report_file")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.Medicalreport)
                    .WithMany(p => p.MedicalReport)
                    .HasForeignKey(d => d.MedicalreportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Medical_R__medic__4AB81AF0");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.MedicalReport)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Medical_R__patie__4BAC3F29");
            });

            modelBuilder.Entity<MedicalreportType>(entity =>
            {
                entity.HasKey(e => e.MedicalreportId)
                    .HasName("PK__medicalr__9B8597D55EE330C8");

                entity.ToTable("medicalreport_type");

                entity.Property(e => e.MedicalreportId).HasColumnName("medicalreport_id");

                entity.Property(e => e.ReportType)
                    .IsRequired()
                    .HasColumnName("report_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MisusedUser>(entity =>
            {
                entity.HasKey(e => e.UserEmail)
                    .HasName("PK__MisusedU__B0FBA21330C7B7C7");

                entity.Property(e => e.UserEmail)
                    .HasColumnName("user_email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Misusedcount).HasColumnName("misusedcount");
            });

            modelBuilder.Entity<MonthlyReport>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__monthly___779B7C583F7DA7C6");

                entity.ToTable("monthly_report");

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorName)
                    .IsRequired()
                    .HasColumnName("doctor_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyIncome).HasColumnName("monthly_income");

                entity.Property(e => e.NoPatients).HasColumnName("no_patients");

                entity.Property(e => e.ReportMonth)
                    .HasColumnName("report_month")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Nurse>(entity =>
            {
                entity.HasIndex(e => e.NurseEmail)
                    .HasName("UQ__Nurse__9B0A9690189821F7")
                    .IsUnique();

                entity.Property(e => e.NurseId).HasColumnName("nurse_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.NurseEmail)
                    .IsRequired()
                    .HasColumnName("nurse_email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NurseName)
                    .IsRequired()
                    .HasColumnName("nurse_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Nurse)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nurse__doctor_id__286302EC");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasIndex(e => e.PatientEmail)
                    .HasName("UQ__Patient__F6D870184DD3FFAC")
                    .IsUnique();

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Allergy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AppointmentMisused).HasColumnName("appointment_misused");

                entity.Property(e => e.Bloodpressure)
                    .HasColumnName("bloodpressure")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Height)
                    .HasColumnName("height")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Insurance)
                    .HasColumnName("insurance")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PatientAddress)
                    .HasColumnName("patient_address")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PatientEmail)
                    .IsRequired()
                    .HasColumnName("patient_email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PatientName)
                    .IsRequired()
                    .HasColumnName("patient_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pulse).HasColumnName("pulse");

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<VisitRecord>(entity =>
            {
                entity.HasKey(e => new { e.Visitid, e.PatientId })
                    .HasName("PK__Visit_Re__79C7ABB75F6BA05B");

                entity.ToTable("Visit_Record");

                entity.Property(e => e.Visitid)
                    .HasColumnName("visitid")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Prescription)
                    .IsRequired()
                    .HasColumnName("prescription")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.VisitDate)
                    .HasColumnName("visit_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.VisitReason)
                    .IsRequired()
                    .HasColumnName("visit_reason")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Visited).HasColumnName("visited");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.VisitRecord)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Visit_Rec__docto__59063A47");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.VisitRecord)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Visit_Rec__patie__5812160E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
