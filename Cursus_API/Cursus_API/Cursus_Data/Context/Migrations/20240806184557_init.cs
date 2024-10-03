using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cursus_Data.Context.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    BankId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    EmailTemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.EmailTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTransactions",
                columns: table => new
                {
                    FTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deposit = table.Column<double>(type: "float", nullable: false),
                    Withdraw = table.Column<double>(type: "float", nullable: false),
                    Withdrawal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactions", x => x.FTID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SystemTransaction",
                columns: table => new
                {
                    StId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTransaction", x => x.StId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBan = table.Column<bool>(type: "bit", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    IsMailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    IsGoogleAccount = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    Certification = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_Instructor_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoke = table.Column<bool>(type: "bit", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBehavior",
                columns: table => new
                {
                    UserBehaviorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Key1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Key2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Key3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date3 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBehavior", x => x.UserBehaviorId);
                    table.ForeignKey(
                        name: "FK_UserBehavior_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserComment",
                columns: table => new
                {
                    UserCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsHide = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComment", x => x.UserCommentId);
                    table.ForeignKey(
                        name: "FK_UserComment_User_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserComment_User_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDetail",
                columns: table => new
                {
                    UserDetailID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetail", x => x.UserDetailID);
                    table.ForeignKey(
                        name: "FK_UserDetail_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEmail",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailTemplateId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmail", x => new { x.UserID, x.EmailTemplateId });
                    table.ForeignKey(
                        name: "FK_UserEmail_EmailTemplate_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplate",
                        principalColumn: "EmailTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEmail_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    WlId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.WlId);
                    table.ForeignKey(
                        name: "FK_Wallet_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AssignNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Course_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookmark",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookmarkId = table.Column<int>(type: "int", nullable: false),
                    BookmarkedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmark", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Bookmark_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookmark_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseVersion",
                columns: table => new
                {
                    CourseVersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    MaintainDay = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVersion", x => x.CourseVersionId);
                    table.ForeignKey(
                        name: "FK_CourseVersion_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TrId);
                    table.ForeignKey(
                        name: "FK_Transaction_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseComment",
                columns: table => new
                {
                    CourseCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToCourseVersionId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsHide = table.Column<bool>(type: "bit", nullable: false),
                    IsComment = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseComment", x => x.CourseCommentId);
                    table.ForeignKey(
                        name: "FK_CourseComment_CourseVersion_ToCourseVersionId",
                        column: x => x.ToCourseVersionId,
                        principalTable: "CourseVersion",
                        principalColumn: "CourseVersionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseComment_User_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseRating",
                columns: table => new
                {
                    CourseRatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToCourseVersionId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRating", x => x.CourseRatingId);
                    table.ForeignKey(
                        name: "FK_CourseRating_CourseVersion_ToCourseVersionId",
                        column: x => x.ToCourseVersionId,
                        principalTable: "CourseVersion",
                        principalColumn: "CourseVersionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseRating_User_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseVersionDetail",
                columns: table => new
                {
                    CourseVersionDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseVersionId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    OldPrice = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    AlreadyEnrolled = table.Column<int>(type: "int", nullable: false),
                    CourseLearningTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVersionDetail", x => x.CourseVersionDetailId);
                    table.ForeignKey(
                        name: "FK_CourseVersionDetail_CourseVersion_CourseVersionId",
                        column: x => x.CourseVersionId,
                        principalTable: "CourseVersion",
                        principalColumn: "CourseVersionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseVersionEmail",
                columns: table => new
                {
                    CourseVersionId = table.Column<int>(type: "int", nullable: false),
                    EmailTemplateId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVersionEmail", x => new { x.CourseVersionId, x.EmailTemplateId });
                    table.ForeignKey(
                        name: "FK_CourseVersionEmail_CourseVersion_CourseVersionId",
                        column: x => x.CourseVersionId,
                        principalTable: "CourseVersion",
                        principalColumn: "CourseVersionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseVersionEmail_EmailTemplate_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplate",
                        principalColumn: "EmailTemplateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourse",
                columns: table => new
                {
                    EnrollCourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseVersionId = table.Column<int>(type: "int", nullable: false),
                    StartEnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndEnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Process = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourse", x => x.EnrollCourseId);
                    table.ForeignKey(
                        name: "FK_EnrollCourse_CourseVersion_CourseVersionId",
                        column: x => x.CourseVersionId,
                        principalTable: "CourseVersion",
                        principalColumn: "CourseVersionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollCourse_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseContent",
                columns: table => new
                {
                    CourseContentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseVersionDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseContent", x => x.CourseContentId);
                    table.ForeignKey(
                        name: "FK_CourseContent_CourseVersionDetail_CourseVersionDetailId",
                        column: x => x.CourseVersionDetailId,
                        principalTable: "CourseVersionDetail",
                        principalColumn: "CourseVersionDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseVersionDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_CourseVersionDetail_CourseVersionDetailId",
                        column: x => x.CourseVersionDetailId,
                        principalTable: "CourseVersionDetail",
                        principalColumn: "CourseVersionDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProcess",
                columns: table => new
                {
                    UserProcessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollCourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseContentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProcess", x => x.UserProcessId);
                    table.ForeignKey(
                        name: "FK_UserProcess_CourseContent_CourseContentId",
                        column: x => x.CourseContentId,
                        principalTable: "CourseContent",
                        principalColumn: "CourseContentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProcess_EnrollCourse_EnrollCourseId",
                        column: x => x.EnrollCourseId,
                        principalTable: "EnrollCourse",
                        principalColumn: "EnrollCourseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CreateBy", "CreateDate", "Description", "IsDelete", "Name", "ParentId", "Status", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { "CT0001", "Admin", new DateTime(2023, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "A modern, object-oriented programming language developed by Microsoft.", false, "C#", null, "Active", "Admin", new DateTime(2023, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CT0002", "Admin", new DateTime(2023, 1, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "A high-level, class-based, object-oriented programming language.", false, "Java", null, "Active", "Admin", new DateTime(2023, 1, 2, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CT0003", "Admin", new DateTime(2023, 1, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "An interpreted, high-level, general-purpose programming language.", false, "Python", null, "Active", "Admin", new DateTime(2023, 1, 3, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CT0005", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the C programming language.", false, "C", null, "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CT0009", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the Java programming language.", false, "NodeJs", null, "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "EmailTemplateId", "Body", "CreateBy", "CreateDate", "IsDelete", "Subject", "Type", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "\r\n                <html>\r\n                <body>\r\n                    <p>Hello {{UserName}},</p>\r\n                    <p>You have a new notification:</p>\r\n                    <p>{{NotificationMessage}}</p>\r\n                    <p>Best regards,<br>Your Company</p>\r\n                </body>\r\n                </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9183), false, "You Have a New Notification", "Notification", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9184) },
                    { 2, "\r\n                <html>\r\n                <body>\r\n                    <p>Hello {{UserName}},</p>\r\n                    <p>We received a request to reset your password. Click the link below to reset your password:</p>\r\n                    <p><a href='{{ResetLink}}'>Reset Password</a></p>\r\n                    <p>If you did not request a password reset, please ignore this email.</p>\r\n                    <p>Best regards,<br>Your Company</p>\r\n                </body>\r\n                </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9186), false, "Password Reset Request", "ForgetPassword", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9187) },
                    { 3, "\r\n                <html>\r\n                <body>\r\n                    <p>Hello {{UserName}},</p>\r\n                    <p>Your password has been successfully reset. You can now log in with your new password.</p>\r\n                    <p>If you did not request this change, please contact our support team immediately.</p>\r\n                    <p>Best regards,<br>Your Company</p>\r\n                </body>\r\n                </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9188), false, "Your Password Has Been Reset", "ResetPassword", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9188) },
                    { 4, "\r\n                <html>\r\n                <body>\r\n                    <p>Hello {{UserName}},</p>\r\n                    <p>Thank you for registering with us. Please confirm your account by clicking the link below:</p>\r\n                    <p><a href='{{ConfirmationLink}}'>Confirm Account</a></p>\r\n                    <p>If you did not register, please ignore this email.</p>\r\n                    <p>Best regards,<br>Your Company</p>\r\n                </body>\r\n                </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9189), false, "Account Confirmation", "ConfirmationAccount", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9190) },
                    { 5, "\r\n                <html>\r\n                <body>\r\n                    <p>Hello {{UserName}},</p>\r\n                    <p>We regret to inform you that your course titled '{{CourseTitle}}' has been rejected for the following reason:</p>\r\n                    <p>{{RejectionReason}}</p>\r\n                    <p>Best regards,<br>Your Company</p>\r\n                </body>\r\n                </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9191), false, "Course Rejection Notification", "RejectCourse", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9192) },
                    { 6, "\r\n                <html>\r\n                <body>\r\n                    <p>Hello {{UserName}},</p>\r\n                    <p>Congratulations! Your course titled '{{CourseTitle}}' has been approved and is now live.</p>\r\n                    <p>Best regards,<br>Your Company</p>\r\n                </body>\r\n                </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9193), false, "Course Approval Notification", "ApproveCourse", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9193) },
                    { 7, "\r\n                <html>\r\n                <body>\r\n                    <p>Hello {{UserName}},</p>\r\n                    <p>Your account has been successfully activated. You can now log in and start using our services.</p>\r\n                    <p>Best regards,<br>Your Company</p>\r\n                </body>\r\n                </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9194), false, "Account Activation", "ActivateUser", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9195) },
                    { 8, "\r\n                <html>\r\n                <body>\r\n                    <p>Hello {{UserName}},</p>\r\n                    <p>Your account has been deactivated. If you think this is a mistake, please contact our support team.</p>\r\n                    <p>Best regards,<br>Your Company</p>\r\n                </body>\r\n                </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9196), false, "Account Deactivation", "DeactivateUser", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9196) },
                    { 9, "\r\n            <html>\r\n            <body>\r\n                <p>Dear {{UserName}},</p>\r\n                <p>We are pleased to inform you that your course titled <strong>'{{CourseTitle}}'</strong> has been successfully reactivated after the maintenance period.</p>\r\n                \r\n                <p>We appreciate your patience and understanding during this time. If you have any questions or need further assistance, please feel free to reach out to us.</p>\r\n                \r\n                <p>Best regards,<br>\r\n                The [Your Company] Team</p>\r\n            </body>\r\n            </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9198), false, "Course Activation Notification", "ActivateCourse", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9198) },
                    { 10, "\r\n            <html>\r\n            <body>\r\n                <p>Hello {{UserName}},</p>\r\n                <p>Your course titled '{{CourseTitle}}' has been deactivated.</p>\r\n                <p>Reason for deactivation: {{DeactivationReason}}</p>\r\n                {{#if Duration}}\r\n                <p>Duration: {{Duration}}</p>\r\n                {{/if}}\r\n                <p>Best regards,<br>Your Company</p>\r\n            </body>\r\n            </html>", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9200), false, "Course Deactivation Notification", "DeactivateCourse", "System", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(9200) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Student" },
                    { 2, "Instructor" },
                    { 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CreateBy", "CreateDate", "Description", "IsDelete", "Name", "ParentId", "Status", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { "CT0004", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the C programming language.", false, "C++", "CT0005", "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CT0007", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the Java programming language.", false, "Java Spring boost", "CT0002", "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CT0008", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the Java programming language.", false, "Java themleaf", "CT0002", "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CT0010", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the Java programming language.", false, "ReactJs", "CT0009", "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Email", "FullName", "IsBan", "IsDelete", "IsGoogleAccount", "IsMailConfirmed", "Password", "Phone", "RoleID" },
                values: new object[,]
                {
                    { "US00000001", "admin1@gmail.com", "Admin 1", false, false, false, true, "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef", "+84111111111", 3 },
                    { "US00000002", "admin2@gmail.com", "Admin 2", false, false, false, true, "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef", "+84222222222", 3 },
                    { "US00000003", "instructor1@gmail.com", "Ins1", false, false, false, true, "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef", "+84111111113", 2 },
                    { "US00000004", "instructor2@gmail.com", "Ins2", false, false, false, true, "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef", "+84111111114", 2 },
                    { "US00000005", "student1@gmail.com", "Stu1", false, false, false, true, "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef", "+84111111114", 1 },
                    { "US00000006", "student2@gmail.com", "Stu2", false, false, false, true, "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef", "+84111111114", 1 },
                    { "US00000007", "student3@gmail.com", "Stu3", false, false, false, true, "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef", "+84111111114", 1 },
                    { "US00000008", "quocthangjk@gmail.com", "Stu4", false, false, false, true, "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef", "+84111111114", 1 }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CreateBy", "CreateDate", "Description", "IsDelete", "Name", "ParentId", "Status", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { "CT0006", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the C programming language.", false, "Go", "CT0004", "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CT0011", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the Java programming language.", false, "ReactJs Native", "CT0010", "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Instructor",
                columns: new[] { "InstructorId", "CardName", "CardNumber", "CardProvider", "Certification", "IsAccepted", "TaxNumber", "UserId" },
                values: new object[,]
                {
                    { "INS00000001", "CardName1", "Card001", "CardProvider1", "Certification1", true, "Tax001", "US00000003" },
                    { "INS00000002", "CardName2", "Card002", "CardProvider2", "Certification2", true, "Tax002", "US00000004" }
                });

            migrationBuilder.InsertData(
                table: "UserDetail",
                columns: new[] { "UserDetailID", "Address", "Avatar", "CreatedDate", "DateOfBirth", "IsActive", "UpdatedDate", "UserID" },
                values: new object[,]
                {
                    { "UD00000001", "123 TP HCM", null, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7952), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7968), "US00000001" },
                    { "UD00000002", "456 TP HN", null, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7969), new DateTime(1991, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7970), "US00000002" },
                    { "UD00000003", "123 TP NT", null, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7971), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7971), "US00000003" },
                    { "UD00000004", "1234 TP HCM", null, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7973), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7973), "US00000004" },
                    { "UD00000005", "1234 TP HCM", null, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7974), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7975), "US00000005" },
                    { "UD00000006", "1234 TP HCM", null, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7976), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7976), "US00000006" },
                    { "UD00000007", "1234 TP HCM", null, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7977), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7978), "US00000007" },
                    { "UD00000008", "1234 TP HCM", null, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7979), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(7979), "US00000008" }
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "WlId", "Amount", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { "WL00000001", 0.0, "system", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8191), "system", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8193), "US00000001" },
                    { "WL00000002", 0.0, "system", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8195), "system", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8195), "US00000002" },
                    { "WL00000003", 0.0, "system", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8197), "system", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8197), "US00000003" },
                    { "WL00000004", 0.0, "system", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8198), "system", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8199), "US00000004" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CreateBy", "CreateDate", "Description", "IsDelete", "Name", "ParentId", "Status", "UpdateBy", "UpdateDate" },
                values: new object[] { "CT0012", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "A general-purpose programming language created as an extension of the Java programming language.", false, "ReactJs Native Child", "CT0011", "Active", "Admin", new DateTime(2023, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseId", "AssignNumber", "CategoryId", "CourseRating", "InstructorId", "Title" },
                values: new object[,]
                {
                    { "CS0001", 0, "CT0001", 0m, "INS00000001", "C# Programming Basics" },
                    { "CS0002", 0, "CT0002", 0m, "INS00000001", "Java Fundamentals" },
                    { "CS0003", 0, "CT0003", 0m, "INS00000001", "Python for Beginners" },
                    { "CS0004", 0, "CT0004", 0m, "INS00000001", "Advanced C++ Programming" },
                    { "CS0005", 0, "CT0001", 0m, "INS00000002", "Advanced C# Topics" },
                    { "CS0006", 0, "CT0002", 0m, "INS00000002", "Java Spring Framework" },
                    { "CS0007", 0, "CT0003", 0m, "INS00000002", "Intermediate Python" },
                    { "CS0008", 0, "CT0004", 0m, "INS00000001", "Object-Oriented Design in C++" },
                    { "CS0009", 0, "CT0005", 0m, "INS00000001", "Introduction to C Programming" },
                    { "CS0010", 0, "CT0006", 0m, "INS00000001", "Concurrency in Go" },
                    { "CS0011", 0, "CT0007", 0m, "INS00000002", "Spring Boot Essentials" },
                    { "CS0012", 0, "CT0008", 0m, "INS00000002", "Thymeleaf Framework" },
                    { "CS0013", 0, "CT0009", 0m, "INS00000002", "Node.js Basics" },
                    { "CS0014", 0, "CT0010", 0m, "INS00000001", "React.js Fundamentals" },
                    { "CS0015", 0, "CT0011", 0m, "INS00000002", "React Native Development" },
                    { "CS0017", 0, "CT0001", 0m, "INS00000002", "Intermediate C# Programming" },
                    { "CS0018", 0, "CT0002", 0m, "INS00000001", "Java EE Development" },
                    { "CS0019", 0, "CT0003", 0m, "INS00000002", "Python Data Analysis" },
                    { "CS0020", 0, "CT0004", 0m, "INS00000002", "Advanced C++ Techniques" },
                    { "CS0021", 0, "CT0005", 0m, "INS00000002", "C Programming for Embedded Systems" },
                    { "CS0022", 0, "CT0006", 0m, "INS00000002", "Go Web Development" },
                    { "CS0023", 0, "CT0007", 0m, "INS00000002", "Spring Boot Microservices" },
                    { "CS0024", 0, "CT0008", 0m, "INS00000001", "Thymeleaf for Web Development" },
                    { "CS0025", 0, "CT0009", 0m, "INS00000001", "RESTful APIs with Node.js" },
                    { "CS0026", 0, "CT0010", 0m, "INS00000001", "React.js State Management" },
                    { "CS0027", 0, "CT0011", 0m, "INS00000001", "Advanced React Native UI Design" },
                    { "CS0016", 0, "CT0012", 0m, "INS00000001", "Advanced React Native Topics" },
                    { "CS0028", 0, "CT0012", 0m, "INS00000002", "React Native App Deployment" }
                });

            migrationBuilder.InsertData(
                table: "CourseVersion",
                columns: new[] { "CourseVersionId", "CourseId", "IsApproved", "IsUsed", "MaintainDay", "Status", "Version" },
                values: new object[,]
                {
                    { 1, "CS0001", true, false, null, "Activate", 1.01m },
                    { 2, "CS0002", true, false, null, "Activate", 1.01m },
                    { 3, "CS0003", true, false, null, "Activate", 1.01m },
                    { 4, "CS0004", true, false, null, "Activate", 1.01m },
                    { 5, "CS0005", true, false, null, "Activate", 1.01m },
                    { 6, "CS0006", true, false, null, "Activate", 1.01m },
                    { 7, "CS0007", true, false, null, "Activate", 1.01m },
                    { 8, "CS0001", true, true, null, "Activate", 1.02m },
                    { 9, "CS0002", true, true, null, "Activate", 1.02m },
                    { 10, "CS0003", true, true, null, "Activate", 1.02m },
                    { 11, "CS0004", true, true, null, "Activate", 1.02m },
                    { 12, "CS0005", true, true, null, "Activate", 1.02m },
                    { 13, "CS0006", true, true, null, "Activate", 1.02m },
                    { 14, "CS0007", true, true, null, "Activate", 1.02m },
                    { 15, "CS0008", true, true, null, "Activate", 1.01m },
                    { 16, "CS0009", true, true, null, "Activate", 1.01m },
                    { 17, "CS0010", true, true, null, "Activate", 1.01m },
                    { 18, "CS0011", true, true, null, "Activate", 1.01m },
                    { 19, "CS0012", true, true, null, "Activate", 1.01m },
                    { 20, "CS0013", true, true, null, "Activate", 1.01m },
                    { 21, "CS0014", true, true, null, "Activate", 1.01m },
                    { 22, "CS0015", true, true, null, "Activate", 1.01m },
                    { 24, "CS0017", true, true, null, "Activate", 1.01m },
                    { 25, "CS0018", true, true, null, "Activate", 1.01m },
                    { 26, "CS0019", true, true, null, "Activate", 1.01m },
                    { 27, "CS0020", true, true, null, "Activate", 1.01m },
                    { 28, "CS0021", true, true, null, "Activate", 1.01m },
                    { 29, "CS0022", true, true, null, "Activate", 1.01m },
                    { 30, "CS0023", true, true, null, "Activate", 1.01m },
                    { 31, "CS0024", true, true, null, "Activate", 1.01m },
                    { 32, "CS0025", true, true, null, "Activate", 1.01m },
                    { 33, "CS0026", true, true, null, "Activate", 1.01m },
                    { 34, "CS0027", true, true, null, "Activate", 1.01m },
                    { 23, "CS0016", true, true, null, "Activate", 1.01m },
                    { 35, "CS0028", true, true, null, "Activate", 1.01m }
                });

            migrationBuilder.InsertData(
                table: "CourseVersionDetail",
                columns: new[] { "CourseVersionDetailId", "AlreadyEnrolled", "CourseLearningTime", "CourseVersionId", "CreatedBy", "CreatedDate", "Description", "IsDelete", "OldPrice", "Price", "Rating", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "CVD0001", 12, "10 hours", 1, "LDQ", new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 100000.0, 4.0m, "LDQ", new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0002", 10, "20 hours", 2, "LDQ", new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 200000.0, 1.1m, "LDQ", new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0003", 30, "30 hours", 3, "LDQ", new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 25000.0, 3.2m, "LDQ", new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0004", 400, "40 hours", 4, "LDQ", new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 30000.0, 5.0m, "LDQ", new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0005", 120, "50 hours", 5, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 40000.0, 4.8m, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0006", 50, "60 hours", 6, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 20000.0, 2.5m, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0007", 83, "70 hours", 7, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 70000.0, 2.7m, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0008", 182, "60 hours", 8, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 90000.0, 4.5m, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0009", 53, "70 hours", 9, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 33000.0, 3.7m, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0010", 95, "15 hours", 10, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 29.989999999999998, 4.2m, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0011", 72, "25 hours", 11, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 39.990000000000002, 3.5m, "LDQ", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0012", 150, "35 hours", 12, "LDQ", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 49.990000000000002, 4.8m, "LDQ", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0013", 220, "45 hours", 13, "LDQ", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 59.990000000000002, 3.9m, "LDQ", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0014", 40, "55 hours", 14, "LDQ", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 69.989999999999995, 2.1m, "LDQ", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0015", 180, "65 hours", 15, "LDQ", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 79.989999999999995, 4.6m, "LDQ", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0016", 90, "75 hours", 16, "LDQ", new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 89.989999999999995, 3.2m, "LDQ", new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0017", 300, "85 hours", 17, "LDQ", new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 99.989999999999995, 4.9m, "LDQ", new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0018", 110, "95 hours", 18, "LDQ", new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 109.98999999999999, 3.8m, "LDQ", new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0019", 60, "105 hours", 19, "LDQ", new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 119.98999999999999, 2.7m, "LDQ", new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0020", 250, "115 hours", 20, "LDQ", new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 129.99000000000001, 4.5m, "LDQ", new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0021", 95, "125 hours", 21, "LDQ", new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 139.99000000000001, 3.1m, "LDQ", new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0022", 180, "135 hours", 22, "LDQ", new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 149.99000000000001, 4.2m, "LDQ", new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0024", 300, "155 hours", 24, "LDQ", new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 169.99000000000001, 4.7m, "LDQ", new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0025", 130, "165 hours", 25, "LDQ", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 179.99000000000001, 3.5m, "LDQ", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0026", 400, "175 hours", 26, "LDQ", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 189.99000000000001, 4.9m, "LDQ", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0027", 210, "185 hours", 27, "LDQ", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 199.99000000000001, 3.9m, "LDQ", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0028", 90, "195 hours", 28, "LDQ", new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 209.99000000000001, 2.6m, "LDQ", new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0029", 240, "205 hours", 29, "LDQ", new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 219.99000000000001, 4.3m, "LDQ", new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0030", 150, "215 hours", 30, "LDQ", new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 229.99000000000001, 3.7m, "LDQ", new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0031", 80, "225 hours", 31, "LDQ", new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 239.99000000000001, 2.9m, "LDQ", new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0032", 310, "235 hours", 32, "LDQ", new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 249.99000000000001, 4.6m, "LDQ", new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0033", 120, "245 hours", 33, "LDQ", new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 259.99000000000001, 3.4m, "LDQ", new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0034", 280, "255 hours", 34, "LDQ", new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 269.99000000000001, 4.8m, "LDQ", new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "EnrollCourse",
                columns: new[] { "EnrollCourseId", "CourseId", "CourseVersionId", "CreatedDate", "EndEnrollDate", "Process", "StartEnrollDate", "Status", "UserId" },
                values: new object[,]
                {
                    { "EC00000001", "CS0001", 8, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8967), new DateTime(2024, 8, 12, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8962), 0.0, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8961), "Enrolled", "US00000005" },
                    { "EC00000002", "CS0002", 9, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8972), new DateTime(2024, 8, 12, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8971), 0.0, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8971), "Enrolled", "US00000005" },
                    { "EC00000003", "CS0002", 9, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8974), new DateTime(2024, 8, 12, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8974), 0.0, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8973), "Enrolled", "US00000006" },
                    { "EC00000004", "CS0001", 8, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8977), new DateTime(2024, 8, 12, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8976), 0.0, new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8976), "Enrolled", "US00000008" }
                });

            migrationBuilder.InsertData(
                table: "CourseContent",
                columns: new[] { "CourseContentId", "CourseVersionDetailId", "CreatedBy", "CreatedDate", "IsDelete", "Time", "Title", "Type", "UpdatedBy", "UpdatedDate", "Url" },
                values: new object[,]
                {
                    { "CC00000001", "CVD0008", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8882), false, 2.0, "Introduction", "Document", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8883), "Link file" },
                    { "CC00000002", "CVD0008", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8885), false, 2.0, "Introduction", "Video", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8885), "Link file" },
                    { "CC00000003", "CVD0008", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8887), false, 2.0, "Introduction", "Silde", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8887), "Link file" },
                    { "CC00000004", "CVD0009", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8889), false, 2.0, "Introduction", "Silde", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8889), "Link file" },
                    { "CC00000005", "CVD0009", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8891), false, 2.0, "Introduction", "Silde", "INS00000001", new DateTime(2024, 8, 7, 1, 45, 53, 694, DateTimeKind.Local).AddTicks(8891), "Link file" }
                });

            migrationBuilder.InsertData(
                table: "CourseVersionDetail",
                columns: new[] { "CourseVersionDetailId", "AlreadyEnrolled", "CourseLearningTime", "CourseVersionId", "CreatedBy", "CreatedDate", "Description", "IsDelete", "OldPrice", "Price", "Rating", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "CVD0023", 70, "145 hours", 23, "LDQ", new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 159.99000000000001, 2.8m, "LDQ", new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CVD0035", 200, "265 hours", 35, "LDQ", new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Course description here", false, 0.0, 279.99000000000001, 3.3m, "LDQ", new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserProcess",
                columns: new[] { "UserProcessId", "CourseContentId", "EnrollCourseId", "IsComplete" },
                values: new object[,]
                {
                    { 1, "CC00000001", "EC00000001", false },
                    { 2, "CC00000002", "EC00000001", false },
                    { 3, "CC00000003", "EC00000001", true },
                    { 4, "CC00000004", "EC00000002", false },
                    { 5, "CC00000005", "EC00000002", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_CourseId",
                table: "Bookmark",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CourseId",
                table: "CartItems",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CategoryId",
                table: "Course",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_InstructorId",
                table: "Course",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseComment_FromUserId",
                table: "CourseComment",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseComment_ToCourseVersionId",
                table: "CourseComment",
                column: "ToCourseVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContent_CourseVersionDetailId",
                table: "CourseContent",
                column: "CourseVersionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRating_FromUserId",
                table: "CourseRating",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRating_ToCourseVersionId",
                table: "CourseRating",
                column: "ToCourseVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVersion_CourseId",
                table: "CourseVersion",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVersionDetail_CourseVersionId",
                table: "CourseVersionDetail",
                column: "CourseVersionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseVersionEmail_EmailTemplateId",
                table: "CourseVersionEmail",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourse_CourseId",
                table: "EnrollCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourse_CourseVersionId",
                table: "EnrollCourse",
                column: "CourseVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourse_UserId",
                table: "EnrollCourse",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_CourseVersionDetailId",
                table: "Image",
                column: "CourseVersionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_UserId",
                table: "Instructor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_CourseId",
                table: "OrderItem",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId",
                table: "Payment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PaymentId",
                table: "Transaction",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBehavior_UserId",
                table: "UserBehavior",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComment_FromUserId",
                table: "UserComment",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComment_ToUserId",
                table: "UserComment",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_UserID",
                table: "UserDetail",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEmail_EmailTemplateId",
                table: "UserEmail",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProcess_CourseContentId",
                table: "UserProcess",
                column: "CourseContentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProcess_EnrollCourseId",
                table: "UserProcess",
                column: "EnrollCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Bookmark");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "CourseComment");

            migrationBuilder.DropTable(
                name: "CourseRating");

            migrationBuilder.DropTable(
                name: "CourseVersionEmail");

            migrationBuilder.DropTable(
                name: "FinancialTransactions");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "SystemTransaction");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "UserBehavior");

            migrationBuilder.DropTable(
                name: "UserComment");

            migrationBuilder.DropTable(
                name: "UserDetail");

            migrationBuilder.DropTable(
                name: "UserEmail");

            migrationBuilder.DropTable(
                name: "UserProcess");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "CourseContent");

            migrationBuilder.DropTable(
                name: "EnrollCourse");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "CourseVersionDetail");

            migrationBuilder.DropTable(
                name: "CourseVersion");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
