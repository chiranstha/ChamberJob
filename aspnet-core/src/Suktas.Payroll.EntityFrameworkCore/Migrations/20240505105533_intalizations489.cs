using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suktas.Payroll.Migrations
{
    /// <inheritdoc />
    public partial class intalizations489 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizedPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessNature = table.Column<int>(type: "int", nullable: false),
                    EstablishedYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyCategoryId = table.Column<int>(type: "int", nullable: false),
                    CompanyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Company_tbl_CompanyCategory_CompanyCategoryId",
                        column: x => x.CompanyCategoryId,
                        principalTable: "tbl_CompanyCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Company_tbl_CompanyType_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "tbl_CompanyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Dbo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommitmentYear = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobSkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Employee_tbl_JobSkill_JobSkillId",
                        column: x => x.JobSkillId,
                        principalTable: "tbl_JobSkill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Male = table.Column<int>(type: "int", nullable: false),
                    Female = table.Column<int>(type: "int", nullable: false),
                    Foreign = table.Column<int>(type: "int", nullable: false),
                    Impairment = table.Column<int>(type: "int", nullable: false),
                    SalaryStart = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaryEnd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AgeStart = table.Column<int>(type: "int", nullable: false),
                    AgeEnd = table.Column<int>(type: "int", nullable: false),
                    Parment = table.Column<int>(type: "int", nullable: false),
                    Temporary = table.Column<int>(type: "int", nullable: false),
                    Trainer = table.Column<int>(type: "int", nullable: false),
                    DailyWages = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employments_tbl_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tbl_Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_JobDemand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobSpecification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    JobSkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_JobDemand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_JobDemand_tbl_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tbl_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_JobDemand_tbl_JobSkill_JobSkillId",
                        column: x => x.JobSkillId,
                        principalTable: "tbl_JobSkill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_JobApply",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Document = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    JobDemandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_JobApply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_JobApply_tbl_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tbl_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_JobApply_tbl_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tbl_Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_JobApply_tbl_JobDemand_JobDemandId",
                        column: x => x.JobDemandId,
                        principalTable: "tbl_JobDemand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employments_CompanyId",
                table: "Employments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_TenantId",
                table: "Employments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Company_CompanyCategoryId",
                table: "tbl_Company",
                column: "CompanyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Company_CompanyTypeId",
                table: "tbl_Company",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Company_TenantId",
                table: "tbl_Company",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Employee_JobSkillId",
                table: "tbl_Employee",
                column: "JobSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Employee_TenantId",
                table: "tbl_Employee",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_JobApply_CompanyId",
                table: "tbl_JobApply",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_JobApply_EmployeeId",
                table: "tbl_JobApply",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_JobApply_JobDemandId",
                table: "tbl_JobApply",
                column: "JobDemandId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_JobApply_TenantId",
                table: "tbl_JobApply",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_JobDemand_CompanyId",
                table: "tbl_JobDemand",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_JobDemand_JobSkillId",
                table: "tbl_JobDemand",
                column: "JobSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_JobDemand_TenantId",
                table: "tbl_JobDemand",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employments");

            migrationBuilder.DropTable(
                name: "tbl_JobApply");

            migrationBuilder.DropTable(
                name: "tbl_Employee");

            migrationBuilder.DropTable(
                name: "tbl_JobDemand");

            migrationBuilder.DropTable(
                name: "tbl_Company");
        }
    }
}
