using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCRUD.Migrations
{
    public partial class AddEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(35)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(35)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(60)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
