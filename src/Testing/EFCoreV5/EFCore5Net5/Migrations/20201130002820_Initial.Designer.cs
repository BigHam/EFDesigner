﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Testing;

namespace EFCore5Net5.Migrations
{
    [DbContext(typeof(AllFeatureModel))]
    [Migration("20201130002820_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BChildBParentCollection", b =>
                {
                    b.Property<int>("BChildCollectionId")
                        .HasColumnType("int");

                    b.Property<int>("BParentCollection_1Id")
                        .HasColumnType("int");

                    b.HasKey("BChildCollectionId", "BParentCollection_1Id");

                    b.HasIndex("BParentCollection_1Id");

                    b.ToTable("BChild_BParentCollection_1_x_BParentCollection_BChildCollection");
                });

            modelBuilder.Entity("Testing.AllPropertyTypesOptional", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<byte[]>("BinaryAttr")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool?>("BooleanAttr")
                        .HasColumnType("bit");

                    b.Property<byte?>("ByteAttr")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("DateTimeAttr")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset?>("DateTimeOffsetAttr")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal?>("DecimalAttr")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double?>("DoubleAttr")
                        .HasColumnType("float");

                    b.Property<Guid?>("GuidAttr")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short?>("Int16Attr")
                        .HasColumnType("smallint");

                    b.Property<int?>("Int32Attr")
                        .HasColumnType("int");

                    b.Property<long?>("Int64Attr")
                        .HasColumnType("bigint");

                    b.Property<float?>("SingleAttr")
                        .HasColumnType("real");

                    b.Property<string>("StringAttr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("TimeAttr")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("AllPropertyTypesOptionals");
                });

            modelBuilder.Entity("Testing.AllPropertyTypesRequired", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<byte[]>("BinaryAttr")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("BooleanAttr")
                        .HasColumnType("bit");

                    b.Property<byte>("ByteAttr")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("DateTimeAttr")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset>("DateTimeOffsetAttr")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("DecimalAttr")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("DoubleAttr")
                        .HasColumnType("float");

                    b.Property<Guid>("GuidAttr")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Int16Attr")
                        .HasColumnType("smallint");

                    b.Property<int>("Int32Attr")
                        .HasColumnType("int");

                    b.Property<long>("Int64Attr")
                        .HasColumnType("bigint");

                    b.Property<float>("SingleAttr")
                        .HasColumnType("real");

                    b.Property<string>("StringAttr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("TimeAttr")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("AllPropertyTypesRequireds");
                });

            modelBuilder.Entity("Testing.BChild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("BParentOptional_1Id")
                        .HasColumnType("int");

                    b.Property<int?>("BParentOptional_2Id")
                        .HasColumnType("int");

                    b.Property<int>("BParentRequiredId")
                        .HasColumnType("int");

                    b.Property<int>("BParentRequired_1Id")
                        .HasColumnType("int");

                    b.Property<int>("BParentRequired_2Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BParentOptional_1Id");

                    b.HasIndex("BParentOptional_2Id")
                        .IsUnique()
                        .HasFilter("[BParentOptional_2Id] IS NOT NULL");

                    b.HasIndex("BParentRequiredId")
                        .IsUnique();

                    b.HasIndex("BParentRequired_1Id")
                        .IsUnique();

                    b.HasIndex("BParentRequired_2Id");

                    b.ToTable("BChilds");
                });

            modelBuilder.Entity("Testing.BParentCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("BChildOptionalId")
                        .HasColumnType("int");

                    b.Property<int>("BChildRequiredId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BChildOptionalId");

                    b.HasIndex("BChildRequiredId");

                    b.ToTable("BParentCollections");
                });

            modelBuilder.Entity("Testing.BParentOptional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BChildRequiredId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BChildRequiredId")
                        .IsUnique();

                    b.ToTable("BParentOptionals");
                });

            modelBuilder.Entity("Testing.BParentRequired", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("BParentRequireds");
                });

            modelBuilder.Entity("Testing.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Master_Children_Id")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Master_Children_Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("Testing.Master", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("Masters");
                });

            modelBuilder.Entity("Testing.ParserTest", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<long>("foo")
                        .HasColumnType("bigint");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("name11")
                        .HasColumnType("int");

                    b.Property<int?>("name12")
                        .HasColumnType("int");

                    b.Property<int?>("name13")
                        .HasColumnType("int");

                    b.Property<int?>("name14")
                        .HasColumnType("int");

                    b.Property<string>("name15")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name16")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name17")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name18")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("name3")
                        .HasColumnType("int");

                    b.Property<int?>("name4")
                        .HasColumnType("int");

                    b.Property<int?>("name5")
                        .HasColumnType("int");

                    b.Property<int?>("name6")
                        .HasColumnType("int");

                    b.Property<string>("name7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParserTests");
                });

            modelBuilder.Entity("Testing.RenamedColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Foo")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("RenamedColumns");
                });

            modelBuilder.Entity("Testing.UChild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("UParentOptional_UChildCollection_Id")
                        .HasColumnType("int");

                    b.Property<int?>("UParentOptional_UChildOptional_Id")
                        .HasColumnType("int");

                    b.Property<int>("UParentRequired_UChildCollection_Id")
                        .HasColumnType("int");

                    b.Property<int>("UParentRequired_UChildOptional_Id")
                        .HasColumnType("int");

                    b.Property<int>("UParentRequired_UChildRequired_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UParentOptional_UChildCollection_Id");

                    b.HasIndex("UParentOptional_UChildOptional_Id")
                        .IsUnique()
                        .HasFilter("[UParentOptional_UChildOptional_Id] IS NOT NULL");

                    b.HasIndex("UParentRequired_UChildCollection_Id");

                    b.HasIndex("UParentRequired_UChildOptional_Id")
                        .IsUnique();

                    b.HasIndex("UParentRequired_UChildRequired_Id")
                        .IsUnique();

                    b.ToTable("UChilds");
                });

            modelBuilder.Entity("Testing.UParentCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("UChildOptionalId")
                        .HasColumnType("int");

                    b.Property<int>("UChildRequiredId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UChildOptionalId");

                    b.HasIndex("UChildRequiredId");

                    b.ToTable("UParentCollections");
                });

            modelBuilder.Entity("Testing.UParentOptional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("PropertyInChild")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UChildRequiredId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UChildRequiredId")
                        .IsUnique();

                    b.ToTable("UParentOptionals");
                });

            modelBuilder.Entity("Testing.UParentRequired", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("UParentRequireds");
                });

            modelBuilder.Entity("BChildBParentCollection", b =>
                {
                    b.HasOne("Testing.BChild", null)
                        .WithMany()
                        .HasForeignKey("BChildCollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testing.BParentCollection", null)
                        .WithMany()
                        .HasForeignKey("BParentCollection_1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.BChild", b =>
                {
                    b.HasOne("Testing.BParentOptional", "BParentOptional_1")
                        .WithMany("BChildCollection")
                        .HasForeignKey("BParentOptional_1Id");

                    b.HasOne("Testing.BParentOptional", "BParentOptional_2")
                        .WithOne("BChildOptional")
                        .HasForeignKey("Testing.BChild", "BParentOptional_2Id");

                    b.HasOne("Testing.BParentRequired", "BParentRequired")
                        .WithOne("BChildOptional")
                        .HasForeignKey("Testing.BChild", "BParentRequiredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testing.BParentRequired", "BParentRequired_1")
                        .WithOne("BChildRequired")
                        .HasForeignKey("Testing.BChild", "BParentRequired_1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testing.BParentRequired", "BParentRequired_2")
                        .WithMany("BChildCollection")
                        .HasForeignKey("BParentRequired_2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BParentOptional_1");

                    b.Navigation("BParentOptional_2");

                    b.Navigation("BParentRequired");

                    b.Navigation("BParentRequired_1");

                    b.Navigation("BParentRequired_2");
                });

            modelBuilder.Entity("Testing.BParentCollection", b =>
                {
                    b.HasOne("Testing.BChild", "BChildOptional")
                        .WithMany("BParentCollection_2")
                        .HasForeignKey("BChildOptionalId");

                    b.HasOne("Testing.BChild", "BChildRequired")
                        .WithMany("BParentCollection")
                        .HasForeignKey("BChildRequiredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BChildOptional");

                    b.Navigation("BChildRequired");
                });

            modelBuilder.Entity("Testing.BParentOptional", b =>
                {
                    b.HasOne("Testing.BChild", "BChildRequired")
                        .WithOne("BParentOptional")
                        .HasForeignKey("Testing.BParentOptional", "BChildRequiredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BChildRequired");
                });

            modelBuilder.Entity("Testing.Child", b =>
                {
                    b.HasOne("Testing.Master", null)
                        .WithMany("Children")
                        .HasForeignKey("Master_Children_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testing.Child", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Testing.UChild", b =>
                {
                    b.HasOne("Testing.UParentOptional", null)
                        .WithMany("UChildCollection")
                        .HasForeignKey("UParentOptional_UChildCollection_Id");

                    b.HasOne("Testing.UParentOptional", null)
                        .WithOne("UChildOptional")
                        .HasForeignKey("Testing.UChild", "UParentOptional_UChildOptional_Id");

                    b.HasOne("Testing.UParentRequired", null)
                        .WithMany("UChildCollection")
                        .HasForeignKey("UParentRequired_UChildCollection_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testing.UParentRequired", null)
                        .WithOne("UChildOptional")
                        .HasForeignKey("Testing.UChild", "UParentRequired_UChildOptional_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testing.UParentRequired", null)
                        .WithOne("UChildRequired")
                        .HasForeignKey("Testing.UChild", "UParentRequired_UChildRequired_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.UParentCollection", b =>
                {
                    b.HasOne("Testing.UChild", "UChildOptional")
                        .WithMany()
                        .HasForeignKey("UChildOptionalId");

                    b.HasOne("Testing.UChild", "UChildRequired")
                        .WithMany()
                        .HasForeignKey("UChildRequiredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UChildOptional");

                    b.Navigation("UChildRequired");
                });

            modelBuilder.Entity("Testing.UParentOptional", b =>
                {
                    b.HasOne("Testing.UChild", "UChildRequired")
                        .WithOne()
                        .HasForeignKey("Testing.UParentOptional", "UChildRequiredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UChildRequired");
                });

            modelBuilder.Entity("Testing.BChild", b =>
                {
                    b.Navigation("BParentCollection");

                    b.Navigation("BParentCollection_2");

                    b.Navigation("BParentOptional");
                });

            modelBuilder.Entity("Testing.BParentOptional", b =>
                {
                    b.Navigation("BChildCollection");

                    b.Navigation("BChildOptional");
                });

            modelBuilder.Entity("Testing.BParentRequired", b =>
                {
                    b.Navigation("BChildCollection");

                    b.Navigation("BChildOptional");

                    b.Navigation("BChildRequired")
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.Child", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Testing.Master", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Testing.UParentOptional", b =>
                {
                    b.Navigation("UChildCollection");

                    b.Navigation("UChildOptional");
                });

            modelBuilder.Entity("Testing.UParentRequired", b =>
                {
                    b.Navigation("UChildCollection");

                    b.Navigation("UChildOptional");

                    b.Navigation("UChildRequired");
                });
#pragma warning restore 612, 618
        }
    }
}
