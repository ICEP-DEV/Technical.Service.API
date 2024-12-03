﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechTrackers.Data;

#nullable disable

namespace TechTrackers.Data.Migrations
{
    [DbContext(typeof(TechTrackersDbContext))]
    [Migration("20241129063422_initial-database")]
    partial class initialdatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TechTrackers.Data.Model.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Escalation", b =>
                {
                    b.Property<int>("EscalationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EscalationId"));

                    b.Property<int>("Escalation_Delay")
                        .HasColumnType("int");

                    b.Property<int>("Escalation_Level")
                        .HasColumnType("int");

                    b.Property<int>("HODId")
                        .HasColumnType("int");

                    b.Property<int>("LogId")
                        .HasColumnType("int");

                    b.Property<string>("Notification_Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("EscalationId");

                    b.HasIndex("HODId");

                    b.HasIndex("LogId");

                    b.ToTable("Escalations");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FeedbackTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("LogId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("LogId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"));

                    b.Property<DateTime>("AssignedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("AssignedBy")
                        .HasColumnType("int");

                    b.Property<byte[]>("AttachmentFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EscalationLevel")
                        .HasColumnType("int");

                    b.Property<string>("IssueTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResolutionDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ResponseDue")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SLAId")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int?>("TechnicianId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.Property<int>("UserIssueId")
                        .HasColumnType("int");

                    b.HasKey("LogId");

                    b.HasIndex("AssignedBy");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CategoryId1");

                    b.HasIndex("SLAId");

                    b.HasIndex("StaffId");

                    b.HasIndex("TechnicianId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.LogChat", b =>
                {
                    b.Property<int>("LogChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogChatId"));

                    b.Property<int>("LogId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("LogChatId");

                    b.HasIndex("LogId");

                    b.HasIndex("SenderId");

                    b.ToTable("Log_chats");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.LogStatusHistory", b =>
                {
                    b.Property<int>("LogStatusHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogStatusHistoryId"));

                    b.Property<int>("ChangedByUserId")
                        .HasColumnType("int");

                    b.Property<int>("LogId")
                        .HasColumnType("int");

                    b.Property<string>("LogStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("LogStatusHistoryId");

                    b.HasIndex("ChangedByUserId");

                    b.HasIndex("LogId");

                    b.ToTable("Log_status_history");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<int>("LogId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReadStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("LogId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.SLA", b =>
                {
                    b.Property<int>("SLAId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SLAId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PriorityLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResolutionTimeframe")
                        .HasColumnType("int");

                    b.Property<int>("ResponseTimeframe")
                        .HasColumnType("int");

                    b.HasKey("SLAId");

                    b.ToTable("SLAs");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentId1")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DepartmentId1");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.UserOtp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<string>("OtpCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User_Otps");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserRoleId"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("User_Roles");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Escalation", b =>
                {
                    b.HasOne("TechTrackers.Data.Model.User", "HOD")
                        .WithMany()
                        .HasForeignKey("HODId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.Log", "Log")
                        .WithMany()
                        .HasForeignKey("LogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HOD");

                    b.Navigation("Log");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Feedback", b =>
                {
                    b.HasOne("TechTrackers.Data.Model.Log", "Log")
                        .WithMany()
                        .HasForeignKey("LogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Log");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Log", b =>
                {
                    b.HasOne("TechTrackers.Data.Model.User", "AssignedByUser")
                        .WithMany()
                        .HasForeignKey("AssignedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TechTrackers.Data.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.Category", null)
                        .WithMany("Logs")
                        .HasForeignKey("CategoryId1");

                    b.HasOne("TechTrackers.Data.Model.SLA", "SLA")
                        .WithMany()
                        .HasForeignKey("SLAId");

                    b.HasOne("TechTrackers.Data.Model.User", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.User", "Technician")
                        .WithMany()
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TechTrackers.Data.Model.User", null)
                        .WithMany("AssignedLogs")
                        .HasForeignKey("UserId");

                    b.HasOne("TechTrackers.Data.Model.User", null)
                        .WithMany("CreatedLogs")
                        .HasForeignKey("UserId1");

                    b.Navigation("AssignedByUser");

                    b.Navigation("Category");

                    b.Navigation("SLA");

                    b.Navigation("Staff");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.LogChat", b =>
                {
                    b.HasOne("TechTrackers.Data.Model.Log", "Log")
                        .WithMany()
                        .HasForeignKey("LogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Log");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.LogStatusHistory", b =>
                {
                    b.HasOne("TechTrackers.Data.Model.User", "ChangedBy")
                        .WithMany()
                        .HasForeignKey("ChangedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.Log", "Log")
                        .WithMany()
                        .HasForeignKey("LogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChangedBy");

                    b.Navigation("Log");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Notification", b =>
                {
                    b.HasOne("TechTrackers.Data.Model.Log", "Log")
                        .WithMany()
                        .HasForeignKey("LogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Log");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.User", b =>
                {
                    b.HasOne("TechTrackers.Data.Model.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.Department", null)
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId1");

                    b.HasOne("TechTrackers.Data.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.UserRole", b =>
                {
                    b.HasOne("TechTrackers.Data.Model.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechTrackers.Data.Model.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Category", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TechTrackers.Data.Model.User", b =>
                {
                    b.Navigation("AssignedLogs");

                    b.Navigation("CreatedLogs");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
