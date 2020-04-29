﻿// <auto-generated />
using System;
using HealthcareProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HealthcareProject.Migrations
{
    [DbContext(typeof(healthcarev1Context))]
    partial class healthcarev1ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HealthcareProject.Models.Appointment", b =>
                {
                    b.Property<int>("ApptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("appt_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppointmentReason")
                        .HasColumnName("appointment_reason")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<DateTime>("ApptDate")
                        .HasColumnName("appt_date")
                        .HasColumnType("date");

                    b.Property<int>("DoctorId")
                        .HasColumnName("doctor_id")
                        .HasColumnType("int");

                    b.Property<string>("PatientEmail")
                        .IsRequired()
                        .HasColumnName("patient_email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int?>("PatientId")
                        .HasColumnName("patient_id")
                        .HasColumnType("int");

                    b.Property<bool>("VisitRecord")
                        .HasColumnName("visit_record")
                        .HasColumnType("bit");

                    b.HasKey("ApptId")
                        .HasName("PK__Appointm__E43EE99683657EA9");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientEmail")
                        .IsUnique()
                        .HasName("UQ__Appointm__F6D870187B6FF196");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("HealthcareProject.Models.Billing", b =>
                {
                    b.Property<int>("BillingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("billing_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("BillingAmount")
                        .HasColumnName("billing_amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("BillingDate")
                        .HasColumnName("billing_date")
                        .HasColumnType("date");

                    b.Property<bool>("Paid")
                        .HasColumnName("paid")
                        .HasColumnType("bit");

                    b.Property<int>("PatientId")
                        .HasColumnName("patient_id")
                        .HasColumnType("int");

                    b.HasKey("BillingId");

                    b.HasIndex("PatientId");

                    b.ToTable("Billing");
                });

            modelBuilder.Entity("HealthcareProject.Models.CardPayment", b =>
                {
                    b.Property<string>("ReferenceNo")
                        .HasColumnName("reference_no")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("BillingId")
                        .HasColumnName("billing_id")
                        .HasColumnType("int");

                    b.Property<double>("PaymentAmount")
                        .HasColumnName("payment_amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnName("payment_date")
                        .HasColumnType("date");

                    b.HasKey("ReferenceNo")
                        .HasName("PK__Card_Pay__8E861397A8D126AE");

                    b.HasIndex("BillingId");

                    b.ToTable("Card_Payment");
                });

            modelBuilder.Entity("HealthcareProject.Models.CashPayment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("payment_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BillingId")
                        .HasColumnName("billing_id")
                        .HasColumnType("int");

                    b.Property<double>("PaymentAmount")
                        .HasColumnName("payment_amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnName("payment_date")
                        .HasColumnType("date");

                    b.HasKey("PaymentId")
                        .HasName("PK__Cash_Pay__ED1FC9EA1EB1019A");

                    b.HasIndex("BillingId");

                    b.ToTable("Cash_Payment");
                });

            modelBuilder.Entity("HealthcareProject.Models.CheckPayment", b =>
                {
                    b.Property<int>("CheckNo")
                        .HasColumnName("check_no")
                        .HasColumnType("int");

                    b.Property<int>("BillingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("billing_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("PaymentAmount")
                        .HasColumnName("payment_amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnName("payment_date")
                        .HasColumnType("date");

                    b.HasKey("CheckNo")
                        .HasName("PK__Check_Pa__C0EA50AF9F947D12");

                    b.HasIndex("BillingId");

                    b.ToTable("Check_Payment");
                });

            modelBuilder.Entity("HealthcareProject.Models.DailyReport", b =>
                {
                    b.Property<string>("ReportId")
                        .HasColumnName("report_id")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<double>("DailyIncome")
                        .HasColumnName("daily_income")
                        .HasColumnType("float");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnName("doctor_name")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("NoPatients")
                        .HasColumnName("no_patients")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnName("report_date")
                        .HasColumnType("date");

                    b.HasKey("ReportId")
                        .HasName("PK__daily_re__779B7C58D7D7300C");

                    b.ToTable("daily_report");
                });

            modelBuilder.Entity("HealthcareProject.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("doctor_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DoctorEmail")
                        .IsRequired()
                        .HasColumnName("doctor_email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnName("doctor_name")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<double>("Salary")
                        .HasColumnName("salary")
                        .HasColumnType("float");

                    b.HasKey("DoctorId");

                    b.HasIndex("DoctorEmail")
                        .IsUnique()
                        .HasName("UQ__Doctor__2046D6B53E341799");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("HealthcareProject.Models.DoctorUnavailability", b =>
                {
                    b.Property<DateTime>("Unavailability")
                        .HasColumnName("unavailability")
                        .HasColumnType("date");

                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("doctor_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Unavailability")
                        .HasName("PK__Doctor_u__1F48084FFCBB1AF4");

                    b.HasIndex("DoctorId");

                    b.ToTable("Doctor_unavailability");
                });

            modelBuilder.Entity("HealthcareProject.Models.MedicalReport", b =>
                {
                    b.Property<string>("MedicalreportName")
                        .HasColumnName("medicalreport_name")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<int>("MedicalreportId")
                        .HasColumnName("medicalreport_id")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnName("patient_id")
                        .HasColumnType("int");

                    b.Property<string>("ReportFile")
                        .IsRequired()
                        .HasColumnName("report_file")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.HasKey("MedicalreportName")
                        .HasName("PK__Medical___4756A6E81E75ACC1");

                    b.HasIndex("MedicalreportId");

                    b.HasIndex("PatientId");

                    b.ToTable("Medical_Report");
                });

            modelBuilder.Entity("HealthcareProject.Models.MedicalreportType", b =>
                {
                    b.Property<int>("MedicalreportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("medicalreport_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasColumnName("report_type")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("MedicalreportId")
                        .HasName("PK__medicalr__9B8597D55EE330C8");

                    b.ToTable("medicalreport_type");
                });

            modelBuilder.Entity("HealthcareProject.Models.MonthlyReport", b =>
                {
                    b.Property<DateTime>("ReportMonth")
                        .HasColumnName("report_month")
                        .HasColumnType("date");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnName("doctor_name")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<double>("MonthlyIncome")
                        .HasColumnName("monthly_income")
                        .HasColumnType("float");

                    b.Property<int>("NoPatients")
                        .HasColumnName("no_patients")
                        .HasColumnType("int");

                    b.HasKey("ReportMonth")
                        .HasName("PK__monthly___A34902B648820416");

                    b.ToTable("monthly_report");
                });

            modelBuilder.Entity("HealthcareProject.Models.Nurse", b =>
                {
                    b.Property<int>("NurseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("nurse_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoctorId")
                        .HasColumnName("doctor_id")
                        .HasColumnType("int");

                    b.Property<string>("NurseEmail")
                        .IsRequired()
                        .HasColumnName("nurse_email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("NurseName")
                        .IsRequired()
                        .HasColumnName("nurse_name")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<double>("Salary")
                        .HasColumnName("salary")
                        .HasColumnType("float");

                    b.HasKey("NurseId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("NurseEmail")
                        .IsUnique()
                        .HasName("UQ__Nurse__9B0A9690189821F7");

                    b.ToTable("Nurse");
                });

            modelBuilder.Entity("HealthcareProject.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("patient_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Allergy")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<int?>("AppointmentMisused")
                        .HasColumnName("appointment_misused")
                        .HasColumnType("int");

                    b.Property<string>("Bloodpressure")
                        .HasColumnName("bloodpressure")
                        .HasColumnType("varchar(6)")
                        .HasMaxLength(6)
                        .IsUnicode(false);

                    b.Property<string>("Height")
                        .HasColumnName("height")
                        .HasColumnType("varchar(6)")
                        .HasMaxLength(6)
                        .IsUnicode(false);

                    b.Property<string>("Insurance")
                        .HasColumnName("insurance")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("PatientAddress")
                        .HasColumnName("patient_address")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("PatientEmail")
                        .IsRequired()
                        .HasColumnName("patient_email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnName("patient_name")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<int?>("Pulse")
                        .HasColumnName("pulse")
                        .HasColumnType("int");

                    b.Property<string>("Ssn")
                        .HasColumnName("ssn")
                        .HasColumnType("varchar(11)")
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.Property<int?>("Weight")
                        .HasColumnName("weight")
                        .HasColumnType("int");

                    b.HasKey("PatientId");

                    b.HasIndex("PatientEmail")
                        .IsUnique()
                        .HasName("UQ__Patient__F6D870184DD3FFAC");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("HealthcareProject.Models.VisitRecord", b =>
                {
                    b.Property<DateTime>("VisitDate")
                        .HasColumnName("visit_date")
                        .HasColumnType("date");

                    b.Property<int>("PatientId")
                        .HasColumnName("patient_id")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnName("doctor_id")
                        .HasColumnType("int");

                    b.Property<string>("Prescription")
                        .IsRequired()
                        .HasColumnName("prescription")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("VisitReason")
                        .IsRequired()
                        .HasColumnName("visit_reason")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<bool>("Visited")
                        .HasColumnName("visited")
                        .HasColumnType("bit");

                    b.HasKey("VisitDate", "PatientId")
                        .HasName("PK__Visit_Re__153471D163ECCB4E");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Visit_Record");
                });

            modelBuilder.Entity("HealthcareProject.Models.Appointment", b =>
                {
                    b.HasOne("HealthcareProject.Models.Doctor", "Doctor")
                        .WithMany("Appointment")
                        .HasForeignKey("DoctorId")
                        .HasConstraintName("FK__Appointme__docto__34C8D9D1")
                        .IsRequired();

                    b.HasOne("HealthcareProject.Models.Patient", "Patient")
                        .WithMany("Appointment")
                        .HasForeignKey("PatientId")
                        .HasConstraintName("FK__Appointme__patie__33D4B598");
                });

            modelBuilder.Entity("HealthcareProject.Models.Billing", b =>
                {
                    b.HasOne("HealthcareProject.Models.Patient", "Patient")
                        .WithMany("Billing")
                        .HasForeignKey("PatientId")
                        .HasConstraintName("FK__Billing__patient__300424B4")
                        .IsRequired();
                });

            modelBuilder.Entity("HealthcareProject.Models.CardPayment", b =>
                {
                    b.HasOne("HealthcareProject.Models.Billing", "Billing")
                        .WithMany("CardPayment")
                        .HasForeignKey("BillingId")
                        .HasConstraintName("FK__Card_Paym__billi__47DBAE45")
                        .IsRequired();
                });

            modelBuilder.Entity("HealthcareProject.Models.CashPayment", b =>
                {
                    b.HasOne("HealthcareProject.Models.Billing", "Billing")
                        .WithMany("CashPayment")
                        .HasForeignKey("BillingId")
                        .HasConstraintName("FK__Cash_Paym__billi__44FF419A")
                        .IsRequired();
                });

            modelBuilder.Entity("HealthcareProject.Models.CheckPayment", b =>
                {
                    b.HasOne("HealthcareProject.Models.Billing", "Billing")
                        .WithMany("CheckPayment")
                        .HasForeignKey("BillingId")
                        .HasConstraintName("FK__Check_Pay__billi__4222D4EF")
                        .IsRequired();
                });

            modelBuilder.Entity("HealthcareProject.Models.DoctorUnavailability", b =>
                {
                    b.HasOne("HealthcareProject.Models.Doctor", "Doctor")
                        .WithMany("DoctorUnavailability")
                        .HasForeignKey("DoctorId")
                        .HasConstraintName("FK__Doctor_un__docto__3F466844")
                        .IsRequired();
                });

            modelBuilder.Entity("HealthcareProject.Models.MedicalReport", b =>
                {
                    b.HasOne("HealthcareProject.Models.MedicalreportType", "Medicalreport")
                        .WithMany("MedicalReport")
                        .HasForeignKey("MedicalreportId")
                        .HasConstraintName("FK__Medical_R__medic__4AB81AF0")
                        .IsRequired();

                    b.HasOne("HealthcareProject.Models.Patient", "Patient")
                        .WithMany("MedicalReport")
                        .HasForeignKey("PatientId")
                        .HasConstraintName("FK__Medical_R__patie__4BAC3F29")
                        .IsRequired();
                });

            modelBuilder.Entity("HealthcareProject.Models.Nurse", b =>
                {
                    b.HasOne("HealthcareProject.Models.Doctor", "Doctor")
                        .WithMany("Nurse")
                        .HasForeignKey("DoctorId")
                        .HasConstraintName("FK__Nurse__doctor_id__286302EC")
                        .IsRequired();
                });

            modelBuilder.Entity("HealthcareProject.Models.VisitRecord", b =>
                {
                    b.HasOne("HealthcareProject.Models.Doctor", "Doctor")
                        .WithMany("VisitRecord")
                        .HasForeignKey("DoctorId")
                        .HasConstraintName("FK__Visit_Rec__docto__3C69FB99")
                        .IsRequired();

                    b.HasOne("HealthcareProject.Models.Patient", "Patient")
                        .WithMany("VisitRecord")
                        .HasForeignKey("PatientId")
                        .HasConstraintName("FK__Visit_Rec__patie__3B75D760")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}