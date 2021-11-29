using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoliticalParties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalParties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnauthorizedAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnauthorizedAttempts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeselHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PeselSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    HasVoted = table.Column<bool>(type: "bit", nullable: false),
                    HasVoteRight = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoliticalPartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_PoliticalParties_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PoliticalParties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7082c8dc-3124-4b46-9225-aa9f721fe718"), "Piastowie" },
                    { new Guid("cb905638-5031-4ac0-8fb9-856fbe1dadc4"), "Dynastia Jagiellonów" },
                    { new Guid("6f7b4461-7f99-4a9a-aa18-21e6e1c17ada"), "Elekcyjni dla Polski" },
                    { new Guid("91ad0f92-4218-453e-946a-1ddab5def81f"), "Wazowie" }
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Name", "PoliticalPartyId", "Surename" },
                values: new object[,]
                {
                    { new Guid("5d44ab60-4280-439c-8143-2d932a273b5e"), "Mieszko", new Guid("7082c8dc-3124-4b46-9225-aa9f721fe718"), "I" },
                    { new Guid("7f257c8f-dc60-4dba-b642-b3b9c6a21266"), "Bolesław", new Guid("7082c8dc-3124-4b46-9225-aa9f721fe718"), "Chrobry" },
                    { new Guid("f3b0001e-9a37-4955-8b40-05cccdea8572"), "Władysław", new Guid("7082c8dc-3124-4b46-9225-aa9f721fe718"), "Łokietek" },
                    { new Guid("aee075de-3257-44ad-83a6-664460327912"), "Kazimierz", new Guid("7082c8dc-3124-4b46-9225-aa9f721fe718"), "Wielki" },
                    { new Guid("699ff9be-471a-41f6-afc6-e534c7aa6250"), "Władysław", new Guid("cb905638-5031-4ac0-8fb9-856fbe1dadc4"), "Jagiełło" },
                    { new Guid("13920626-d15e-4766-b595-5fef5369dae5"), "Władysław", new Guid("cb905638-5031-4ac0-8fb9-856fbe1dadc4"), "Warneńczyk" },
                    { new Guid("e912608b-98f1-4d31-8c42-07b4435ff4fc"), "Zygmunt", new Guid("cb905638-5031-4ac0-8fb9-856fbe1dadc4"), "Stary" },
                    { new Guid("98f1d798-f4db-4619-815e-fb740afdaa7c"), "Henryk", new Guid("6f7b4461-7f99-4a9a-aa18-21e6e1c17ada"), "Walezy" },
                    { new Guid("6c99866a-a474-469b-aafd-30e7d1b387b8"), "Anna", new Guid("6f7b4461-7f99-4a9a-aa18-21e6e1c17ada"), "Jagiellonka" },
                    { new Guid("f9fe4093-59f2-401f-b391-1763ff4821db"), "Stefan", new Guid("6f7b4461-7f99-4a9a-aa18-21e6e1c17ada"), "Batory" },
                    { new Guid("2e63f181-537a-4f87-b687-fa2c6615c14a"), "Zygmunt", new Guid("91ad0f92-4218-453e-946a-1ddab5def81f"), "Waza" },
                    { new Guid("a4e24540-2136-4e85-8188-a9f37220f30f"), "Władysław", new Guid("91ad0f92-4218-453e-946a-1ddab5def81f"), "Waza" },
                    { new Guid("6af5a729-f1bd-4778-b20e-9a98ded92dd9"), "Jan", new Guid("91ad0f92-4218-453e-946a-1ddab5def81f"), "Kazimierz" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PoliticalPartyId",
                table: "Candidates",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnauthorizedAttempts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "PoliticalParties");
        }
    }
}
