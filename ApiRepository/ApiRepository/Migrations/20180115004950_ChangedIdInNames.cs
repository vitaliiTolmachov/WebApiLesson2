using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApiRepository.Migrations
{
    public partial class ChangedIdInNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Departments_DepartmentIdId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryIdId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryIdId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryIdId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "DepartmentIdId",
                table: "Categories",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_DepartmentIdId",
                table: "Categories",
                newName: "IX_Categories_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Departments_DepartmentId",
                table: "Categories",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Departments_DepartmentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "CategoryIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryIdId");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Categories",
                newName: "DepartmentIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_DepartmentId",
                table: "Categories",
                newName: "IX_Categories_DepartmentIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Departments_DepartmentIdId",
                table: "Categories",
                column: "DepartmentIdId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryIdId",
                table: "Products",
                column: "CategoryIdId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
