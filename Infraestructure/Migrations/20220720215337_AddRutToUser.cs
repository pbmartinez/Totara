using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddRutToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rut",
                table: "Usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nombre", "Rut", "Suspended", "Username" },
                values: new object[,]
                {
                    { 13, "vvillagran@dl.cl", "VALESKA VILLAGR", "16782703-9", false, "16782703-9" },
                    { 15, "17768779-0@pilotoapp.com", "Lilian Carolina Gonzalez Silva", "", false, "17768779-0" },
                    { 19, "25338108-6@pilotoapp.com", "Alejandro Sarmiento Millares", "", false, "25338108-6" },
                    { 20, "26402139-1@pilotoapp.com", "ANA GABRIELA DUR", "26402139-1", false, "26402139-1" },
                    { 81, "17050718-5@pilotoapp.com", "Pamela de los Angeles Lira Morales", "17050718-5", false, "17050718-5" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DropColumn(
                name: "Rut",
                table: "Usuarios");
        }
    }
}
