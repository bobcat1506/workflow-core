using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WorkflowCore.Persistence.Oracle.Migrations
{
    public partial class StepScope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Scope",
                schema: "WFC",
                table: "ExecutionPointer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "WFC",
                table: "ExecutionPointer",
                nullable: false,
                defaultValue: 0);           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scope",
                schema: "WFC",
                table: "ExecutionPointer");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "WFC",
                table: "ExecutionPointer");
            
        }
    }
}
