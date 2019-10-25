using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkflowCore.Persistence.Oracle.Migrations
{
    public partial class ControlStructures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionError_ExecutionPointer_ExecutionPointerId",
                schema: "WFC",
                table: "ExecutionError");

            migrationBuilder.DropIndex(
                name: "IX_ExecutionError_ExecutionPointerId",
                schema: "WFC",
                table: "ExecutionError");

            migrationBuilder.DropColumn(
                name: "PathTerminator",
                schema: "WFC",
                table: "ExecutionPointer");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "WFC",
                table: "ExecutionError");

            migrationBuilder.RenameColumn(
                name: "ConcurrentFork",
                schema: "WFC",
                table: "ExecutionPointer",
                newName: "RetryCount");

            migrationBuilder.AlterColumn<string>(
                name: "StepName",
                schema: "WFC",
                table: "ExecutionPointer",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "WFC",
                table: "ExecutionPointer",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventKey",
                schema: "WFC",
                table: "ExecutionPointer",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Children",
                schema: "WFC",
                table: "ExecutionPointer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContextItem",
                schema: "WFC",
                table: "ExecutionPointer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PredecessorId",
                schema: "WFC",
                table: "ExecutionPointer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExecutionPointerId",
                schema: "WFC",
                table: "ExecutionError",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "WorkflowId",
                schema: "WFC",
                table: "ExecutionError",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Children",
                schema: "WFC",
                table: "ExecutionPointer");

            migrationBuilder.DropColumn(
                name: "ContextItem",
                schema: "WFC",
                table: "ExecutionPointer");

            migrationBuilder.DropColumn(
                name: "PredecessorId",
                schema: "WFC",
                table: "ExecutionPointer");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                schema: "WFC",
                table: "ExecutionError");

            migrationBuilder.RenameColumn(
                name: "RetryCount",
                schema: "WFC",
                table: "ExecutionPointer",
                newName: "ConcurrentFork");

            migrationBuilder.AlterColumn<string>(
                name: "StepName",
                schema: "WFC",
                table: "ExecutionPointer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "WFC",
                table: "ExecutionPointer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventKey",
                schema: "WFC",
                table: "ExecutionPointer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PathTerminator",
                schema: "WFC",
                table: "ExecutionPointer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "ExecutionPointerId",
                schema: "WFC",
                table: "ExecutionError",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "WFC",
                table: "ExecutionError",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionError_ExecutionPointerId",
                schema: "WFC",
                table: "ExecutionError",
                column: "ExecutionPointerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionError_ExecutionPointer_ExecutionPointerId",
                schema: "WFC",
                table: "ExecutionError",
                column: "ExecutionPointerId",
                principalSchema: "WFC",
                principalTable: "ExecutionPointer",
                principalColumn: "PersistenceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
