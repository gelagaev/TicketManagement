using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommentAndTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ticket_TicketId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "AuthorUserId",
                schema: "dbo",
                table: "Tickets",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "dbo",
                table: "Comments",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TicketId",
                schema: "dbo",
                table: "Comments",
                newName: "IX_Comments_TicketId");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedId",
                schema: "dbo",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                schema: "dbo",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                schema: "dbo",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedId",
                schema: "dbo",
                table: "Tickets",
                column: "AssignedId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AuthorId",
                schema: "dbo",
                table: "Tickets",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                schema: "dbo",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                schema: "dbo",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tickets_TicketId",
                schema: "dbo",
                table: "Comments",
                column: "TicketId",
                principalSchema: "dbo",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedId",
                schema: "dbo",
                table: "Tickets",
                column: "AssignedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AuthorId",
                schema: "dbo",
                table: "Tickets",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tickets_TicketId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedId",
                schema: "dbo",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AuthorId",
                schema: "dbo",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                schema: "dbo",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssignedId",
                schema: "dbo",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AuthorId",
                schema: "dbo",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AuthorId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AssignedId",
                schema: "dbo",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                schema: "dbo",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "Comments",
                schema: "dbo",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Ticket",
                newName: "AuthorUserId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Comment",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TicketId",
                table: "Comment",
                newName: "IX_Comment_TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ticket_TicketId",
                table: "Comment",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id");
        }
    }
}
