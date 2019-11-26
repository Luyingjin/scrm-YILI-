﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Pb.Wechat.EntityFrameworkCore;
using System;

namespace Pb.Wechat.Migrations.KfDb
{
    [DbContext(typeof(KfDbContext))]
    [Migration("20180404150302_create_db_kf")]
    partial class create_db_kf
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pb.Wechat.CustomerServiceOnlines.CustomerServiceOnline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("InviteExpireTime");

                    b.Property<string>("InviteStatus");

                    b.Property<string>("InviteWx");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("KfAccount");

                    b.Property<string>("KfHeadingUrl");

                    b.Property<string>("KfId");

                    b.Property<string>("KfNick");

                    b.Property<string>("KfPassWord");

                    b.Property<string>("KfWx");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LocalHeadFilePath");

                    b.Property<string>("LocalHeadingUrl");

                    b.Property<int>("MpID");

                    b.Property<string>("PreKfAccount");

                    b.Property<string>("PublicNumberAccount");

                    b.HasKey("Id");

                    b.ToTable("CustomerServiceOnlines");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerServiceResponseTexts.CustomerServiceResponseText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MpID");

                    b.Property<string>("ResponseText");

                    b.Property<string>("ResponseType");

                    b.HasKey("Id");

                    b.ToTable("CustomerServiceResponseTexts");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerServiceWorkTimes.CustomerServiceWorkTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AfternoonEndHour");

                    b.Property<string>("AfternoonEndMinute");

                    b.Property<string>("AfternoonStartHour");

                    b.Property<string>("AfternoonStartMinute");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("MorningEndHour");

                    b.Property<string>("MorningEndMinute");

                    b.Property<string>("MorningStartHour");

                    b.Property<string>("MorningStartMinute");

                    b.Property<int>("MpID");

                    b.Property<string>("WeekDay");

                    b.HasKey("Id");

                    b.ToTable("CustomerServiceWorkTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
