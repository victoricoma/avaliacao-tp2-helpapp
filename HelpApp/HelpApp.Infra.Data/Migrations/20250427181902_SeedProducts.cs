using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpApp.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Description", "Price", "Stock", "Image", "CategoryId" },
                values: new object[,]
                {
                    { 1, "Notebook", "Core i7 16GB RAM", 4999.90m, 15, "notebook.jpg", 2 },
                    { 2, "Caneta", "Azul ponta fina", 4.90m, 200, "caneta.jpg", 1 },
                    { 3, "Caderno", "Preto cor Varias Materias", 24.90m, 500, "caderno.jpg", 1 },
                    { 4, "Tablet"," Tablet de 5 in 4 GB de RAM ",1200m, 20,"tablet.jpg",2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "Products",
               keyColumn: "Id",
               keyValues: new object[] { 1,2,3,4 });
        }
    }
}


