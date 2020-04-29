using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareProject.Migrations
{
    public partial class Datetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateTable(
                name: "daily_report",
                columns: table => new
                {
                    report_id = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    report_date = table.Column<DateTime>(type: "date", nullable: false),
                    no_patients = table.Column<int>(nullable: false),
                    doctor_name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    daily_income = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__daily_re__779B7C58D7D7300C", x => x.report_id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    doctor_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctor_name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    salary = table.Column<double>(nullable: false),
                    doctor_email = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.doctor_id);
                });

            migrationBuilder.CreateTable(
                name: "medicalreport_type",
                columns: table => new
                {
                    medicalreport_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_type = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__medicalr__9B8597D55EE330C8", x => x.medicalreport_id);
                });

            migrationBuilder.CreateTable(
                name: "MisusedUser",
                columns: table => new
                {
                    user_email = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    misusedcount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MisusedU__B0FBA21330C7B7C7", x => x.user_email);
                });

            migrationBuilder.CreateTable(
                name: "monthly_report",
                columns: table => new
                {
                    report_month = table.Column<DateTime>(type: "date", nullable: false),
                    no_patients = table.Column<int>(nullable: false),
                    doctor_name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    monthly_income = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__monthly___A34902B648820416", x => x.report_month);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    patient_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patient_name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    patient_email = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    patient_address = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    phone = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ssn = table.Column<string>(unicode: false, maxLength: 11, nullable: true),
                    weight = table.Column<int>(nullable: true),
                    height = table.Column<string>(unicode: false, maxLength: 6, nullable: true),
                    insurance = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    bloodpressure = table.Column<string>(unicode: false, maxLength: 6, nullable: true),
                    pulse = table.Column<int>(nullable: true),
                    appointment_misused = table.Column<int>(nullable: true),
                    Allergy = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.patient_id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor_availability",
                columns: table => new
                {
                    availability_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctor_id = table.Column<int>(nullable: false),
                    available_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctor_a__86E3A801826D3B8A", x => x.availability_id);
                    table.ForeignKey(
                        name: "FK__Doctor_av__docto__787EE5A0",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctor_unavailability",
                columns: table => new
                {
                    unavailability_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctor_id = table.Column<int>(nullable: false),
                    unavailable = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctor_u__9B3B44342AF5EFFA", x => x.unavailability_id);
                    table.ForeignKey(
                        name: "FK__Doctor_un__docto__7D439ABD",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nurse",
                columns: table => new
                {
                    nurse_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nurse_name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    salary = table.Column<double>(nullable: false),
                    nurse_email = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    doctor_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurse", x => x.nurse_id);
                    table.ForeignKey(
                        name: "FK__Nurse__doctor_id__286302EC",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    appt_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appt_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    appointment_reason = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    patient_email = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    patient_id = table.Column<int>(nullable: true),
                    visit_record = table.Column<bool>(nullable: false),
                    doctor_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Appointm__E43EE99683657EA9", x => x.appt_id);
                    table.ForeignKey(
                        name: "FK__Appointme__docto__34C8D9D1",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Appointme__patie__33D4B598",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "patient_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Billing",
                columns: table => new
                {
                    billing_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billing_date = table.Column<DateTime>(type: "date", nullable: false),
                    billing_amount = table.Column<double>(nullable: false),
                    paid = table.Column<bool>(nullable: false),
                    patient_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billing", x => x.billing_id);
                    table.ForeignKey(
                        name: "FK__Billing__patient__300424B4",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "patient_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medical_Report",
                columns: table => new
                {
                    medicalreport_name = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    report_file = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    medicalreport_id = table.Column<int>(nullable: false),
                    patient_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medical___4756A6E81E75ACC1", x => x.medicalreport_name);
                    table.ForeignKey(
                        name: "FK__Medical_R__medic__4AB81AF0",
                        column: x => x.medicalreport_id,
                        principalTable: "medicalreport_type",
                        principalColumn: "medicalreport_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Medical_R__patie__4BAC3F29",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "patient_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visit_Record",
                columns: table => new
                {
                    visitid = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    patient_id = table.Column<int>(nullable: false),
                    visit_reason = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    prescription = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    visit_date = table.Column<DateTime>(type: "date", nullable: false),
                    visited = table.Column<bool>(nullable: false),
                    doctor_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Visit_Re__79C7ABB75F6BA05B", x => new { x.visitid, x.patient_id });
                    table.ForeignKey(
                        name: "FK__Visit_Rec__docto__59063A47",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Visit_Rec__patie__5812160E",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "patient_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Card_Payment",
                columns: table => new
                {
                    reference_no = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    payment_amount = table.Column<double>(nullable: false),
                    payment_date = table.Column<DateTime>(type: "date", nullable: false),
                    billing_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Card_Pay__8E861397E3D35FD9", x => x.reference_no);
                    table.ForeignKey(
                        name: "FK__Card_Paym__billi__619B8048",
                        column: x => x.billing_id,
                        principalTable: "Billing",
                        principalColumn: "billing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cash_Payment",
                columns: table => new
                {
                    payment_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_date = table.Column<DateTime>(type: "date", nullable: false),
                    payment_amount = table.Column<double>(nullable: false),
                    billing_id = table.Column<int>(nullable: false),
                    receive_receipt = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cash_Pay__ED1FC9EAA570F0BE", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK__Cash_Paym__billi__6477ECF3",
                        column: x => x.billing_id,
                        principalTable: "Billing",
                        principalColumn: "billing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Check_Payment",
                columns: table => new
                {
                    check_no = table.Column<int>(nullable: false),
                    payment_amount = table.Column<double>(nullable: false),
                    payment_date = table.Column<DateTime>(type: "date", nullable: false),
                    billing_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    receive_receipt = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Check_Pa__C0EA50AF7A31AC3B", x => x.check_no);
                    table.ForeignKey(
                        name: "FK__Check_Pay__billi__5EBF139D",
                        column: x => x.billing_id,
                        principalTable: "Billing",
                        principalColumn: "billing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_doctor_id",
                table: "Appointment",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Appointm__F6D870187B6FF196",
                table: "Appointment",
                column: "patient_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_patient_id",
                table: "Appointment",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_patient_id",
                table: "Billing",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Card_Payment_billing_id",
                table: "Card_Payment",
                column: "billing_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cash_Payment_billing_id",
                table: "Cash_Payment",
                column: "billing_id");

            migrationBuilder.CreateIndex(
                name: "IX_Check_Payment_billing_id",
                table: "Check_Payment",
                column: "billing_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Doctor__2046D6B53E341799",
                table: "Doctor",
                column: "doctor_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_availability_doctor_id",
                table: "Doctor_availability",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_unavailability_doctor_id",
                table: "Doctor_unavailability",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Report_medicalreport_id",
                table: "Medical_Report",
                column: "medicalreport_id");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Report_patient_id",
                table: "Medical_Report",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Nurse_doctor_id",
                table: "Nurse",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Nurse__9B0A9690189821F7",
                table: "Nurse",
                column: "nurse_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Patient__F6D870184DD3FFAC",
                table: "Patient",
                column: "patient_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visit_Record_doctor_id",
                table: "Visit_Record",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_Record_patient_id",
                table: "Visit_Record",
                column: "patient_id");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Card_Payment");

            migrationBuilder.DropTable(
                name: "Cash_Payment");

            migrationBuilder.DropTable(
                name: "Check_Payment");

            migrationBuilder.DropTable(
                name: "daily_report");

            migrationBuilder.DropTable(
                name: "Doctor_availability");

            migrationBuilder.DropTable(
                name: "Doctor_unavailability");

            migrationBuilder.DropTable(
                name: "Medical_Report");

            migrationBuilder.DropTable(
                name: "MisusedUser");

            migrationBuilder.DropTable(
                name: "monthly_report");

            migrationBuilder.DropTable(
                name: "Nurse");

            migrationBuilder.DropTable(
                name: "Visit_Record");

            migrationBuilder.DropTable(
                name: "Billing");

            migrationBuilder.DropTable(
                name: "medicalreport_type");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
