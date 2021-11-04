using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteDeFrais.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompteComptable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Compte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompteComptable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DemandeConge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebutConge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRepriseConge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    EmployeeIDConsumed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandeConge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Decimal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
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
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupeFrais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrixUPro = table.Column<float>(type: "real", nullable: true),
                    PrixUPerso = table.Column<float>(type: "real", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupeFrais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoyenPaiement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<bool>(type: "bit", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoyenPaiement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviseCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOrdreMission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAbroad = table.Column<bool>(type: "bit", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOrdreMission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WFDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    AffectedToId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WFDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeDepense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompteComptableID = table.Column<int>(type: "int", nullable: true),
                    TVA = table.Column<float>(type: "real", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeDepense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeDepense_CompteComptable_CompteComptableID",
                        column: x => x.CompteComptableID,
                        principalTable: "CompteComptable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeGroupe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAffectation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFinAffectation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupeFraisID = table.Column<int>(type: "int", nullable: false),
                    EmployeeIDConsumed = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGroupe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeGroupe_GroupeFrais_GroupeFraisID",
                        column: x => x.GroupeFraisID,
                        principalTable: "GroupeFrais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdreMission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaysIdConsumed = table.Column<int>(type: "int", nullable: true),
                    EmployeeIDConsumed = table.Column<int>(type: "int", nullable: false),
                    TypeMissionOrderId = table.Column<int>(type: "int", nullable: false),
                    NumeroOM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdreMission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdreMission_TypeOrdreMission_TypeMissionOrderId",
                        column: x => x.TypeMissionOrderId,
                        principalTable: "TypeOrdreMission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupeFraisDepense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupeFraisID = table.Column<int>(type: "int", nullable: false),
                    TypeDepenseID = table.Column<int>(type: "int", nullable: false),
                    Plafond = table.Column<float>(type: "real", nullable: false),
                    Forfait = table.Column<float>(type: "real", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupeFraisDepense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupeFraisDepense_GroupeFrais_GroupeFraisID",
                        column: x => x.GroupeFraisID,
                        principalTable: "GroupeFrais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupeFraisDepense_TypeDepense_TypeDepenseID",
                        column: x => x.TypeDepenseID,
                        principalTable: "TypeDepense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotesFrais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateNote = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeIDConsumed = table.Column<int>(type: "int", nullable: false),
                    Validateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdreMissionId = table.Column<int>(type: "int", nullable: true),
                    TotalKm = table.Column<float>(type: "real", nullable: false),
                    TotalTTC = table.Column<float>(type: "real", nullable: false),
                    TotalRembourser = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesFrais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotesFrais_OrdreMission_OrdreMissionId",
                        column: x => x.OrdreMissionId,
                        principalTable: "OrdreMission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Depenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDepense = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Facturable = table.Column<bool>(type: "bit", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceCommande = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TVA = table.Column<float>(type: "real", nullable: false),
                    TTC = table.Column<float>(type: "real", nullable: false),
                    TotalRemboursable = table.Column<float>(type: "real", nullable: false),
                    NotesFraisID = table.Column<int>(type: "int", nullable: true),
                    TypeDepenseID = table.Column<int>(type: "int", nullable: true),
                    MoyenPaiementID = table.Column<int>(type: "int", nullable: true),
                    DeviseIDConsumed = table.Column<int>(type: "int", nullable: true),
                    ExchangeRate = table.Column<float>(type: "real", nullable: false),
                    PaysIDConsumed = table.Column<int>(type: "int", nullable: true),
                    Warning = table.Column<bool>(type: "bit", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Depenses_MoyenPaiement_MoyenPaiementID",
                        column: x => x.MoyenPaiementID,
                        principalTable: "MoyenPaiement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Depenses_NotesFrais_NotesFraisID",
                        column: x => x.NotesFraisID,
                        principalTable: "NotesFrais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Depenses_TypeDepense_TypeDepenseID",
                        column: x => x.TypeDepenseID,
                        principalTable: "TypeDepense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsNoteFrais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NotesFraisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsNoteFrais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsNoteFrais_NotesFrais_NotesFraisId",
                        column: x => x.NotesFraisId,
                        principalTable: "NotesFrais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FraisKilometriques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Depart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arrivee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreTrajets = table.Column<float>(type: "real", nullable: false),
                    DistanceParcourue = table.Column<float>(type: "real", nullable: false),
                    DistanceParcouruetotal = table.Column<float>(type: "real", nullable: false),
                    TotalTTC = table.Column<float>(type: "real", nullable: false),
                    TotalRemboursable = table.Column<float>(type: "real", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeVehicule = table.Column<bool>(type: "bit", nullable: false),
                    DepartMaps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArriveeMaps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotesFraisId = table.Column<int>(type: "int", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FraisKilometriques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FraisKilometriques_NotesFrais_NotesFraisId",
                        column: x => x.NotesFraisId,
                        principalTable: "NotesFrais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsDepenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DepensesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsDepenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsDepenses_Depenses_DepensesId",
                        column: x => x.DepensesId,
                        principalTable: "Depenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depenses_MoyenPaiementID",
                table: "Depenses",
                column: "MoyenPaiementID");

            migrationBuilder.CreateIndex(
                name: "IX_Depenses_NotesFraisID",
                table: "Depenses",
                column: "NotesFraisID");

            migrationBuilder.CreateIndex(
                name: "IX_Depenses_TypeDepenseID",
                table: "Depenses",
                column: "TypeDepenseID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsDepenses_DepensesId",
                table: "DocumentsDepenses",
                column: "DepensesId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsNoteFrais_NotesFraisId",
                table: "DocumentsNoteFrais",
                column: "NotesFraisId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGroupe_GroupeFraisID",
                table: "EmployeeGroupe",
                column: "GroupeFraisID");

            migrationBuilder.CreateIndex(
                name: "IX_FraisKilometriques_NotesFraisId",
                table: "FraisKilometriques",
                column: "NotesFraisId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupeFraisDepense_GroupeFraisID",
                table: "GroupeFraisDepense",
                column: "GroupeFraisID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupeFraisDepense_TypeDepenseID",
                table: "GroupeFraisDepense",
                column: "TypeDepenseID");

            migrationBuilder.CreateIndex(
                name: "IX_NotesFrais_OrdreMissionId",
                table: "NotesFrais",
                column: "OrdreMissionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdreMission_TypeMissionOrderId",
                table: "OrdreMission",
                column: "TypeMissionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeDepense_CompteComptableID",
                table: "TypeDepense",
                column: "CompteComptableID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandeConge");

            migrationBuilder.DropTable(
                name: "Devise");

            migrationBuilder.DropTable(
                name: "DocumentsDepenses");

            migrationBuilder.DropTable(
                name: "DocumentsNoteFrais");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "EmployeeGroupe");

            migrationBuilder.DropTable(
                name: "FraisKilometriques");

            migrationBuilder.DropTable(
                name: "GroupeFraisDepense");

            migrationBuilder.DropTable(
                name: "pay");

            migrationBuilder.DropTable(
                name: "WFDocument");

            migrationBuilder.DropTable(
                name: "Depenses");

            migrationBuilder.DropTable(
                name: "GroupeFrais");

            migrationBuilder.DropTable(
                name: "MoyenPaiement");

            migrationBuilder.DropTable(
                name: "NotesFrais");

            migrationBuilder.DropTable(
                name: "TypeDepense");

            migrationBuilder.DropTable(
                name: "OrdreMission");

            migrationBuilder.DropTable(
                name: "CompteComptable");

            migrationBuilder.DropTable(
                name: "TypeOrdreMission");
        }
    }
}
