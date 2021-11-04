using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conge.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FiscalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeRegister = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplementAdresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "droitConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoisAffectation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_droitConge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroPersonne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDateCin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceCin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidityDateRP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecruitementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TitularizationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelGSM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Langue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanDroitCongeIDConsumed = table.Column<int>(type: "int", nullable: true),
                    RegimeTravailID = table.Column<int>(type: "int", nullable: true),
                    ConsultantExterne = table.Column<bool>(type: "bit", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "joursFeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_joursFeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "planDroitConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planDroitConge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "seuils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seuil = table.Column<float>(type: "real", nullable: false),
                    Valeur = table.Column<float>(type: "real", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seuils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "typeConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CongeAnnuel = table.Column<bool>(type: "bit", nullable: false),
                    NombreJours = table.Column<float>(type: "real", nullable: true),
                    NombreFois = table.Column<int>(type: "int", nullable: true),
                    Period = table.Column<int>(type: "int", nullable: true),
                    soldeNegatif = table.Column<bool>(type: "bit", nullable: false),
                    IsRemplacement = table.Column<bool>(type: "bit", nullable: false),
                    notification = table.Column<bool>(type: "bit", nullable: false),
                    messageNotification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeConge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "details_DroitConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Droit = table.Column<float>(type: "real", nullable: false),
                    DroitMisAJour = table.Column<float>(type: "real", nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    DroitCongeId = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_details_DroitConge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_details_DroitConge_droitConge_DroitCongeId",
                        column: x => x.DroitCongeId,
                        principalTable: "droitConge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToAnc = table.Column<long>(type: "bigint", nullable: false),
                    JourIncrimente = table.Column<float>(type: "real", nullable: false),
                    PlanDroitCongeID = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_anciente_planDroitConge_PlanDroitCongeID",
                        column: x => x.PlanDroitCongeID,
                        principalTable: "planDroitConge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "demandeConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDemande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDebutConge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRepriseConge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NbrConge = table.Column<float>(type: "real", nullable: false),
                    IdRemplacant = table.Column<int>(type: "int", nullable: false),
                    IsApremDebut = table.Column<bool>(type: "bit", nullable: false),
                    IsApremRetour = table.Column<bool>(type: "bit", nullable: false),
                    NumeroDemande = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solde = table.Column<float>(type: "real", nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NbrBonification = table.Column<float>(type: "real", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    TypeCongeID = table.Column<int>(type: "int", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demandeConge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_demandeConge_typeConge_TypeCongeID",
                        column: x => x.TypeCongeID,
                        principalTable: "typeConge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mvtConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoldeApres = table.Column<float>(type: "real", nullable: false),
                    NbreJours = table.Column<float>(type: "real", nullable: false),
                    TypeCongeID = table.Column<int>(type: "int", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    Sens = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mvtConge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mvtConge_typeConge_TypeCongeID",
                        column: x => x.TypeCongeID,
                        principalTable: "typeConge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "soldeConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Solde = table.Column<float>(type: "real", nullable: false),
                    Annee = table.Column<int>(type: "int", nullable: false),
                    TypeCongeID = table.Column<int>(type: "int", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_soldeConge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_soldeConge_typeConge_TypeCongeID",
                        column: x => x.TypeCongeID,
                        principalTable: "typeConge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documentsConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DemandeCongeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentsConge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_documentsConge_demandeConge_DemandeCongeId",
                        column: x => x.DemandeCongeId,
                        principalTable: "demandeConge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "titreConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDemande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDebutConge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRepriseConge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NbrConge = table.Column<float>(type: "real", nullable: false),
                    IsApremDebut = table.Column<bool>(type: "bit", nullable: false),
                    IsApremRetour = table.Column<bool>(type: "bit", nullable: false),
                    NumeroTitre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NbrBonification = table.Column<float>(type: "real", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    DelegationId = table.Column<int>(type: "int", nullable: true),
                    TypeCongeID = table.Column<int>(type: "int", nullable: false),
                    NumeroDemande = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    IdRemplacant = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_titreConge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_titreConge_typeConge_TypeCongeID",
                        column: x => x.TypeCongeID,
                        principalTable: "typeConge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "delegation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    IdRemplacant = table.Column<int>(type: "int", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    TitreId = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delegation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_delegation_titreConge_TitreId",
                        column: x => x.TitreId,
                        principalTable: "titreConge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anciente_PlanDroitCongeID",
                table: "anciente",
                column: "PlanDroitCongeID");

            migrationBuilder.CreateIndex(
                name: "IX_delegation_TitreId",
                table: "delegation",
                column: "TitreId");

            migrationBuilder.CreateIndex(
                name: "IX_demandeConge_TypeCongeID",
                table: "demandeConge",
                column: "TypeCongeID");

            migrationBuilder.CreateIndex(
                name: "IX_details_DroitConge_DroitCongeId",
                table: "details_DroitConge",
                column: "DroitCongeId");

            migrationBuilder.CreateIndex(
                name: "IX_documentsConge_DemandeCongeId",
                table: "documentsConge",
                column: "DemandeCongeId");

            migrationBuilder.CreateIndex(
                name: "IX_mvtConge_TypeCongeID",
                table: "mvtConge",
                column: "TypeCongeID");

            migrationBuilder.CreateIndex(
                name: "IX_soldeConge_TypeCongeID",
                table: "soldeConge",
                column: "TypeCongeID");

            migrationBuilder.CreateIndex(
                name: "IX_titreConge_DelegationId",
                table: "titreConge",
                column: "DelegationId");

            migrationBuilder.CreateIndex(
                name: "IX_titreConge_TypeCongeID",
                table: "titreConge",
                column: "TypeCongeID");

            migrationBuilder.AddForeignKey(
                name: "FK_titreConge_delegation_DelegationId",
                table: "titreConge",
                column: "DelegationId",
                principalTable: "delegation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_delegation_titreConge_TitreId",
                table: "delegation");

            migrationBuilder.DropTable(
                name: "anciente");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "details_DroitConge");

            migrationBuilder.DropTable(
                name: "documentsConge");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "joursFeries");

            migrationBuilder.DropTable(
                name: "mvtConge");

            migrationBuilder.DropTable(
                name: "seuils");

            migrationBuilder.DropTable(
                name: "soldeConge");

            migrationBuilder.DropTable(
                name: "planDroitConge");

            migrationBuilder.DropTable(
                name: "droitConge");

            migrationBuilder.DropTable(
                name: "demandeConge");

            migrationBuilder.DropTable(
                name: "titreConge");

            migrationBuilder.DropTable(
                name: "delegation");

            migrationBuilder.DropTable(
                name: "typeConge");
        }
    }
}
