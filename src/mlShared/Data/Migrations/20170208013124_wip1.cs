using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mlShared.Data.Migrations
{
    public partial class wip1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worlds_AspNetUsers_OwnerId",
                table: "Worlds");

            migrationBuilder.DropIndex(
                name: "IX_Worlds_OwnerId",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "MultiverseId",
                table: "Worlds");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Worlds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Worlds",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Worlds",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_OwnerId1",
                table: "Worlds",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Worlds_AspNetUsers_OwnerId1",
                table: "Worlds",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worlds_AspNetUsers_OwnerId1",
                table: "Worlds");

            migrationBuilder.DropIndex(
                name: "IX_Worlds_OwnerId1",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Worlds");

            migrationBuilder.AddColumn<Guid>(
                name: "MultiverseId",
                table: "Worlds",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Worlds",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_OwnerId",
                table: "Worlds",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Worlds_AspNetUsers_OwnerId",
                table: "Worlds",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
