using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParamGeneral.Data.Migrations
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
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    PaysIdConsumed = table.Column<int>(type: "int", nullable: false),
                    DeviseIDConsumed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "departements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "devise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decimal = table.Column<int>(type: "int", nullable: false),
                    ExchangeRate = table.Column<float>(type: "real", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "emploi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emploi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "regimeTravail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParamWeek = table.Column<bool>(type: "bit", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regimeTravail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "typeHierarchyPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeHierarchyPosition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviseID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pay_devise_DeviseID",
                        column: x => x.DeviseID,
                        principalTable: "devise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Gender = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_employee_regimeTravail_RegimeTravailID",
                        column: x => x.RegimeTravailID,
                        principalTable: "regimeTravail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "paramGeneraux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebutAnneeBonifiation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinAnneeBonifiation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekEnd1 = table.Column<int>(type: "int", nullable: false),
                    WeekEnd2 = table.Column<int>(type: "int", nullable: false),
                    WeekEnd1_inclut = table.Column<bool>(type: "bit", nullable: false),
                    Souche = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekEnd2_inclut = table.Column<bool>(type: "bit", nullable: false),
                    AfficheOU = table.Column<bool>(type: "bit", nullable: false),
                    AfficheJB = table.Column<bool>(type: "bit", nullable: false),
                    TpHierPosID = table.Column<int>(type: "int", nullable: true),
                    Remplacant_autre_company = table.Column<bool>(type: "bit", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RaportName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paramGeneraux", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paramGeneraux_typeHierarchyPosition_TpHierPosID",
                        column: x => x.TpHierPosID,
                        principalTable: "typeHierarchyPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartementID = table.Column<int>(type: "int", nullable: false),
                    UniteID = table.Column<int>(type: "int", nullable: true),
                    EmploiID = table.Column<int>(type: "int", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_position_departements_DepartementID",
                        column: x => x.DepartementID,
                        principalTable: "departements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_position_emploi_EmploiID",
                        column: x => x.EmploiID,
                        principalTable: "emploi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_position_unite_UniteID",
                        column: x => x.UniteID,
                        principalTable: "unite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeVehicules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matricule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PuissanceFiscale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<bool>(type: "bit", nullable: false),
                    TiTulaireVehProf = table.Column<bool>(type: "bit", nullable: false),
                    TypeVehicule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TiTulaireCarteEssence = table.Column<bool>(type: "bit", nullable: false),
                    Plafond = table.Column<float>(type: "real", nullable: false),
                    ValiditeCarte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeVehicules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeVehicules_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wFDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Step = table.Column<int>(type: "int", nullable: false),
                    Cycle = table.Column<int>(type: "int", nullable: false),
                    Ordre = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestorId = table.Column<int>(type: "int", nullable: false),
                    AffectedToId = table.Column<int>(type: "int", nullable: false),
                    RemplacantId = table.Column<int>(type: "int", nullable: true),
                    AffectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wFDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wFDocuments_employee_AffectedToId",
                        column: x => x.AffectedToId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_wFDocuments_employee_RemplacantId",
                        column: x => x.RemplacantId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_wFDocuments_employee_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "affectationEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affectationEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_affectationEmployee_employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_affectationEmployee_position_PositionID",
                        column: x => x.PositionID,
                        principalTable: "position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hierarchyPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionSupID = table.Column<int>(type: "int", nullable: false),
                    TypeHierarchyPositionID = table.Column<int>(type: "int", nullable: false),
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    UserCreat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hierarchyPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hierarchyPosition_position_PositionID",
                        column: x => x.PositionID,
                        principalTable: "position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hierarchyPosition_typeHierarchyPosition_TypeHierarchyPositionID",
                        column: x => x.TypeHierarchyPositionID,
                        principalTable: "typeHierarchyPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_affectationEmployee_EmployeeID",
                table: "affectationEmployee",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_affectationEmployee_PositionID",
                table: "affectationEmployee",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_document_EmployeeId",
                table: "document",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_RegimeTravailID",
                table: "employee",
                column: "RegimeTravailID");

            migrationBuilder.CreateIndex(
                name: "IX_employeeVehicules_EmployeeId",
                table: "employeeVehicules",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_hierarchyPosition_PositionID",
                table: "hierarchyPosition",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_hierarchyPosition_TypeHierarchyPositionID",
                table: "hierarchyPosition",
                column: "TypeHierarchyPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_paramGeneraux_TpHierPosID",
                table: "paramGeneraux",
                column: "TpHierPosID");

            migrationBuilder.CreateIndex(
                name: "IX_pay_DeviseID",
                table: "pay",
                column: "DeviseID");

            migrationBuilder.CreateIndex(
                name: "IX_position_DepartementID",
                table: "position",
                column: "DepartementID");

            migrationBuilder.CreateIndex(
                name: "IX_position_EmploiID",
                table: "position",
                column: "EmploiID");

            migrationBuilder.CreateIndex(
                name: "IX_position_UniteID",
                table: "position",
                column: "UniteID");

            migrationBuilder.CreateIndex(
                name: "IX_wFDocuments_AffectedToId",
                table: "wFDocuments",
                column: "AffectedToId");

            migrationBuilder.CreateIndex(
                name: "IX_wFDocuments_RemplacantId",
                table: "wFDocuments",
                column: "RemplacantId");

            migrationBuilder.CreateIndex(
                name: "IX_wFDocuments_RequestorId",
                table: "wFDocuments",
                column: "RequestorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "affectationEmployee");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "employeeVehicules");

            migrationBuilder.DropTable(
                name: "hierarchyPosition");

            migrationBuilder.DropTable(
                name: "paramGeneraux");

            migrationBuilder.DropTable(
                name: "pay");

            migrationBuilder.DropTable(
                name: "wFDocuments");

            migrationBuilder.DropTable(
                name: "position");

            migrationBuilder.DropTable(
                name: "typeHierarchyPosition");

            migrationBuilder.DropTable(
                name: "devise");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "departements");

            migrationBuilder.DropTable(
                name: "emploi");

            migrationBuilder.DropTable(
                name: "unite");

            migrationBuilder.DropTable(
                name: "regimeTravail");
        }
    }
}
