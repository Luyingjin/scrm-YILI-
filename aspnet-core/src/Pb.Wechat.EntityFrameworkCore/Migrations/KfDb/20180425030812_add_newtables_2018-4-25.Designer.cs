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
    [Migration("20180425030812_add_newtables_2018-4-25")]
    partial class add_newtables_2018425
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pb.Wechat.CustomerArticleGroupItems.CustomerArticleGroupItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticleID");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("GroupID");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MpID");

                    b.Property<int?>("SortIndex");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CustomerArticleGroupItems");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerArticleGroups.CustomerArticleGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MpID");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CustomerArticleGroups");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerArticles.CustomerArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AUrl");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<string>("FilePathOrUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MpID");

                    b.Property<string>("PicMediaID");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CustomerArticles");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerInOutLogs.CustomerInOutLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("CustomerId");

                    b.Property<int>("InOutState");

                    b.HasKey("Id");

                    b.ToTable("CustomerInOutLogs");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerMediaImages.CustomerMediaImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("FilePathOrUrl")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("MediaID")
                        .HasMaxLength(200);

                    b.Property<int>("MpID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("CustomerMediaImages");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerMediaVideos.CustomerMediaVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<string>("FilePathOrUrl")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("MediaID");

                    b.Property<int>("MpID");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CustomerMediaVideos");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerMediaVoices.CustomerMediaVoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<string>("FilePathOrUrl")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("MediaID");

                    b.Property<int>("MpID");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CustomerMediaVoices");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerServiceConversationMsgs.CustomerServiceConversationMsg", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int?>("CustomerId");

                    b.Property<int?>("FanId");

                    b.Property<string>("MediaId");

                    b.Property<string>("MediaUrl");

                    b.Property<int>("MpID");

                    b.Property<string>("MsgContent");

                    b.Property<int>("MsgType");

                    b.Property<int>("Sender");

                    b.HasKey("Id");

                    b.ToTable("CustomerServiceConversationMsgs");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerServiceConversations.CustomerServiceConversation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConversationScore");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int?>("CustomerId");

                    b.Property<string>("CustomerOpenId");

                    b.Property<DateTime?>("EndTalkTime");

                    b.Property<int?>("FanId");

                    b.Property<string>("FanOpenId");

                    b.Property<string>("HeadImgUrl");

                    b.Property<long?>("LastConversationId");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MpID");

                    b.Property<string>("NickName");

                    b.Property<DateTime?>("StartTalkTime");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.ToTable("CustomerServiceConversations");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerServiceOnlines.CustomerServiceOnline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AutoJoin");

                    b.Property<int>("AutoJoinCount");

                    b.Property<bool>("AutoJoinReply");

                    b.Property<string>("AutoJoinReplyText")
                        .HasMaxLength(200);

                    b.Property<bool>("AutoLeaveReply");

                    b.Property<string>("AutoLeaveReplyText")
                        .HasMaxLength(200);

                    b.Property<string>("ConnectId");

                    b.Property<int>("ConnectState");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("CustomerServiceManager");

                    b.Property<string>("InviteExpireTime");

                    b.Property<string>("InviteStatus");

                    b.Property<string>("InviteWx");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("KfAccount");

                    b.Property<string>("KfHeadingUrl");

                    b.Property<string>("KfId");

                    b.Property<string>("KfNick");

                    b.Property<string>("KfPassWord");

                    b.Property<string>("KfType");

                    b.Property<string>("KfWx");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LocalHeadFilePath");

                    b.Property<string>("LocalHeadingUrl");

                    b.Property<string>("MessageToken");

                    b.Property<int>("MpID");

                    b.Property<int>("OnlineState");

                    b.Property<string>("OpenID");

                    b.Property<string>("PreKfAccount");

                    b.Property<string>("PublicNumberAccount");

                    b.HasKey("Id");

                    b.ToTable("CustomerServiceOnlines");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerServicePrivateResponseTexts.CustomerServicePrivateResponseText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("MediaId");

                    b.Property<int>("MpID");

                    b.Property<string>("OpenID");

                    b.Property<string>("PreviewImgUrl");

                    b.Property<string>("ReponseContentType");

                    b.Property<string>("ResponseText");

                    b.Property<string>("ResponseType");

                    b.Property<string>("Title");

                    b.Property<int>("UseCount");

                    b.HasKey("Id");

                    b.ToTable("CustomerServicePrivateResponseTexts");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerServiceResponseTexts.CustomerServiceResponseText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleDescription");

                    b.Property<string>("ArticleTitle");

                    b.Property<string>("ArticleUrl");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<string>("ImageName")
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MartialId");

                    b.Property<string>("MediaId");

                    b.Property<int>("MpID");

                    b.Property<string>("PreviewImgUrl");

                    b.Property<int?>("ReponseContentType");

                    b.Property<string>("ResponseText");

                    b.Property<string>("ResponseType");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.Property<int>("TypeId");

                    b.Property<string>("TypeName")
                        .HasMaxLength(50);

                    b.Property<int>("UseCount");

                    b.Property<string>("VoiceName")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("CustomerServiceResponseTexts");
                });

            modelBuilder.Entity("Pb.Wechat.CustomerServiceResponseTypes.CustomerServiceResponseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("TypeDescription")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("CustomerServiceResponseTypes");
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
