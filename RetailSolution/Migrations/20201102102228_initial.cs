using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSolution.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BaseSalary = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoursRegistry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeesId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Hours = table.Column<float>(nullable: false),
                    Shift = table.Column<bool>(nullable: false),
                    Sunday = table.Column<float>(nullable: false),
                    Monday = table.Column<float>(nullable: false),
                    Tuesday = table.Column<float>(nullable: false),
                    Wednesday = table.Column<float>(nullable: false),
                    Thursday = table.Column<float>(nullable: false),
                    Friday = table.Column<float>(nullable: false),
                    Saturday = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoursRegistry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoursRegistry_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoursRegistry_EmployeesId",
                table: "HoursRegistry",
                column: "EmployeesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoursRegistry");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
