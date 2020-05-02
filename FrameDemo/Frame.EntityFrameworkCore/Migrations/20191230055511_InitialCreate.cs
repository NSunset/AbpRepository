using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Frame.EntityFrameworkCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Design_AmountConf",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Image_Kind = table.Column<int>(nullable: false),
                    Image_Type = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design_AmountConf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Design_LifeStatus",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusName = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    IsShow = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design_LifeStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Design_UserInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    OpenId = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    RealName = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    IdCard = table.Column<string>(nullable: true),
                    IdCardFront = table.Column<string>(nullable: true),
                    IdCardReverse = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    IsValidIdCard = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manage_Permission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PerName = table.Column<string>(maxLength: 100, nullable: true),
                    PerValue = table.Column<string>(maxLength: 200, nullable: true),
                    FatherId = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Ioc = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manage_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manage_Role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    FatherId = table.Column<long>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manage_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manage_UserInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    Passwd = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manage_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Design_Style",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    StyleOrderNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    Image_Kind = table.Column<int>(nullable: false),
                    Image_Type = table.Column<int>(nullable: false),
                    StyleName = table.Column<string>(nullable: true),
                    AuditStatus = table.Column<int>(nullable: false),
                    PayStatus = table.Column<int>(nullable: false),
                    RefuseMsg = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    PayTime = table.Column<DateTime>(nullable: true),
                    AuditTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design_Style", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Design_Style_Design_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "Design_UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manage_PayHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    WXOrderNumber = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    ActualAmount = table.Column<decimal>(nullable: false),
                    WithholdingTaxAmount = table.Column<decimal>(nullable: false),
                    TaxRate = table.Column<decimal>(nullable: false),
                    IncludeOrderNum = table.Column<int>(nullable: false),
                    PayStatus = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manage_PayHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manage_PayHistory_Design_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "Design_UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manage_Role_Permission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PermissionId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    ManageRoleId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manage_Role_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manage_Role_Permission_Manage_Role_ManageRoleId",
                        column: x => x.ManageRoleId,
                        principalTable: "Manage_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Manage_User_Role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    ManageUserInfoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manage_User_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manage_User_Role_Manage_UserInfo_ManageUserInfoId",
                        column: x => x.ManageUserInfoId,
                        principalTable: "Manage_UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Design_StyleCategory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StyleId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design_StyleCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Design_StyleCategory_Design_Style_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Design_Style",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Design_StyleImgDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    StyleId = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design_StyleImgDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Design_StyleImgDetail_Design_Style_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Design_Style",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Design_StyleLifeLine",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    StyleId = table.Column<long>(nullable: false),
                    LifeStatusId = table.Column<long>(nullable: false),
                    ActionResult = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsLastAction = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design_StyleLifeLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Design_StyleLifeLine_Design_Style_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Design_Style",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manage_PayDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    StyleId = table.Column<long>(nullable: false),
                    ManagePayHistoryId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manage_PayDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manage_PayDetail_Manage_PayHistory_ManagePayHistoryId",
                        column: x => x.ManagePayHistoryId,
                        principalTable: "Manage_PayHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Design_Style_UserId",
                table: "Design_Style",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Design_StyleCategory_StyleId",
                table: "Design_StyleCategory",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Design_StyleImgDetail_StyleId",
                table: "Design_StyleImgDetail",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Design_StyleLifeLine_StyleId",
                table: "Design_StyleLifeLine",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Manage_PayDetail_ManagePayHistoryId",
                table: "Manage_PayDetail",
                column: "ManagePayHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Manage_PayHistory_UserId",
                table: "Manage_PayHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Manage_Role_Permission_ManageRoleId",
                table: "Manage_Role_Permission",
                column: "ManageRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Manage_User_Role_ManageUserInfoId",
                table: "Manage_User_Role",
                column: "ManageUserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Design_AmountConf");

            migrationBuilder.DropTable(
                name: "Design_LifeStatus");

            migrationBuilder.DropTable(
                name: "Design_StyleCategory");

            migrationBuilder.DropTable(
                name: "Design_StyleImgDetail");

            migrationBuilder.DropTable(
                name: "Design_StyleLifeLine");

            migrationBuilder.DropTable(
                name: "Manage_PayDetail");

            migrationBuilder.DropTable(
                name: "Manage_Permission");

            migrationBuilder.DropTable(
                name: "Manage_Role_Permission");

            migrationBuilder.DropTable(
                name: "Manage_User_Role");

            migrationBuilder.DropTable(
                name: "Design_Style");

            migrationBuilder.DropTable(
                name: "Manage_PayHistory");

            migrationBuilder.DropTable(
                name: "Manage_Role");

            migrationBuilder.DropTable(
                name: "Manage_UserInfo");

            migrationBuilder.DropTable(
                name: "Design_UserInfo");
        }
    }
}
