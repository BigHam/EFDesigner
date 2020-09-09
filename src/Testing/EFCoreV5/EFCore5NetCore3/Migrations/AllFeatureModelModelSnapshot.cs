﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Testing;

namespace EFCore5NetCore3.Migrations
{
    [DbContext(typeof(AllFeatureModel))]
    partial class AllFeatureModelModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-preview.8.20407.4");

            modelBuilder.Entity("Testing.AllPropertyTypesOptional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Id1")
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

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id", "Id1");

                    b.ToTable("AllPropertyTypesOptionals");
                });

            modelBuilder.Entity("Testing.AllPropertyTypesRequired", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

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

                    b.Property<int?>("BParentOptional_1_Id")
                        .HasColumnType("int");

                    b.Property<int?>("BParentOptional_2_Id")
                        .HasColumnType("int");

                    b.Property<int?>("BParentRequired_1_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("BParentRequired_2_Id")
                        .HasColumnType("int");

                    b.Property<int?>("BParentRequired_Id")
                        .HasColumnType("int");

                    b.Property<string>("Property1a")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("BParentOptional_1_Id");

                    b.HasIndex("BParentOptional_2_Id")
                        .IsUnique()
                        .HasFilter("[BParentOptional_2_Id] IS NOT NULL");

                    b.HasIndex("BParentRequired_1_Id")
                        .IsUnique();

                    b.HasIndex("BParentRequired_2_Id");

                    b.HasIndex("BParentRequired_Id")
                        .IsUnique()
                        .HasFilter("[BParentRequired_Id] IS NOT NULL");

                    b.ToTable("BChilds");
                });

            modelBuilder.Entity("Testing.BParentCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("BChildOptional_Id")
                        .HasColumnType("int");

                    b.Property<int?>("BChildRequired_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BChildOptional_Id");

                    b.HasIndex("BChildRequired_Id");

                    b.ToTable("BParentCollections");
                });

            modelBuilder.Entity("Testing.BParentOptional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("BChildRequired_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BChildRequired_Id")
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

            modelBuilder.Entity("Testing.BaseClassWithRequiredProperties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Property0")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BaseClassWithRequiredProperties");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseClassWithRequiredProperties");
                });

            modelBuilder.Entity("Testing.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("Child_Children_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Parent_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Child_Children_Id");

                    b.HasIndex("Parent_Id");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("Testing.HiddenEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Property1")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HiddenEntities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("HiddenEntity");
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

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

                    b.Property<int?>("UChild_UChildCollection_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UChild_UChildCollection_Id");

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

                    b.Property<int?>("UChildRequiredId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UChildOptionalId");

                    b.HasIndex("UChildRequiredId");

                    b.ToTable("UParentCollections");
                });

            modelBuilder.Entity("Testing.UParentRequired", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Property1ab")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UChild_UChildOptional_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("UChild_UChildRequired_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UChild_UChildOptional_Id")
                        .IsUnique();

                    b.HasIndex("UChild_UChildRequired_Id")
                        .IsUnique();

                    b.ToTable("UParentRequireds");
                });

            modelBuilder.Entity("Testing.AbstractBaseClass", b =>
                {
                    b.HasBaseType("Testing.BaseClassWithRequiredProperties");

                    b.HasDiscriminator().HasValue("AbstractBaseClass");
                });

            modelBuilder.Entity("Testing.BaseClass", b =>
                {
                    b.HasBaseType("Testing.BaseClassWithRequiredProperties");

                    b.HasDiscriminator().HasValue("BaseClass");
                });

            modelBuilder.Entity("Testing.UParentOptional", b =>
                {
                    b.HasBaseType("Testing.HiddenEntity");

                    b.Property<string>("PropertyInChild")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("hello");

                    b.Property<int?>("UChildRequired_Id")
                        .HasColumnType("int");

                    b.Property<int?>("UChild_UChildOptional_Id")
                        .HasColumnType("int");

                    b.HasIndex("UChildRequired_Id")
                        .IsUnique()
                        .HasFilter("[UChildRequired_Id] IS NOT NULL");

                    b.HasIndex("UChild_UChildOptional_Id")
                        .IsUnique()
                        .HasFilter("[UChild_UChildOptional_Id] IS NOT NULL");

                    b.HasDiscriminator().HasValue("UParentOptional");
                });

            modelBuilder.Entity("Testing.ConcreteDerivedClass", b =>
                {
                    b.HasBaseType("Testing.AbstractBaseClass");

                    b.Property<string>("Property1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyInChild")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ConcreteDerivedClass");
                });

            modelBuilder.Entity("Testing.ConcreteDerivedClassWithRequiredProperties", b =>
                {
                    b.HasBaseType("Testing.AbstractBaseClass");

                    b.Property<string>("Property1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ConcreteDerivedClassWithRequiredProperties_Property1");

                    b.HasDiscriminator().HasValue("ConcreteDerivedClassWithRequiredProperties");
                });

            modelBuilder.Entity("Testing.DerivedClass", b =>
                {
                    b.HasBaseType("Testing.BaseClass");

                    b.Property<string>("Property1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DerivedClass_Property1");

                    b.Property<string>("PropertyInChild")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DerivedClass_PropertyInChild");

                    b.HasDiscriminator().HasValue("DerivedClass");
                });

            modelBuilder.Entity("Testing.AllPropertyTypesOptional", b =>
                {
                    b.OwnsOne("Testing.OwnedType", "OwnedType", b1 =>
                        {
                            b1.Property<int>("AllPropertyTypesOptionalId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .UseIdentityColumn();

                            b1.Property<int>("AllPropertyTypesOptionalId1")
                                .HasColumnType("int");

                            b1.Property<float?>("SingleAttr")
                                .HasColumnType("real");

                            b1.HasKey("AllPropertyTypesOptionalId", "AllPropertyTypesOptionalId1");

                            b1.ToTable("AllPropertyTypesOptionals");

                            b1.WithOwner()
                                .HasForeignKey("AllPropertyTypesOptionalId", "AllPropertyTypesOptionalId1");
                        });
                });

            modelBuilder.Entity("Testing.BChild", b =>
                {
                    b.HasOne("Testing.BParentOptional", "BParentOptional_1")
                        .WithMany("BChildCollection")
                        .HasForeignKey("BParentOptional_1_Id");

                    b.HasOne("Testing.BParentOptional", "BParentOptional_2")
                        .WithOne("BChildOptional")
                        .HasForeignKey("Testing.BChild", "BParentOptional_2_Id");

                    b.HasOne("Testing.BParentRequired", "BParentRequired_1")
                        .WithOne("BChildRequired")
                        .HasForeignKey("Testing.BChild", "BParentRequired_1_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Testing.BParentRequired", "BParentRequired_2")
                        .WithMany("BChildCollection")
                        .HasForeignKey("BParentRequired_2_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Testing.BParentRequired", "BParentRequired")
                        .WithOne("BChildOptional")
                        .HasForeignKey("Testing.BChild", "BParentRequired_Id");
                });

            modelBuilder.Entity("Testing.BParentCollection", b =>
                {
                    b.HasOne("Testing.BChild", "BChildOptional")
                        .WithMany("BParentCollection_2")
                        .HasForeignKey("BChildOptional_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Testing.BChild", "BChildRequired")
                        .WithMany("BParentCollection")
                        .HasForeignKey("BChildRequired_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.BParentOptional", b =>
                {
                    b.HasOne("Testing.BChild", "BChildRequired")
                        .WithOne("BParentOptional")
                        .HasForeignKey("Testing.BParentOptional", "BChildRequired_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.Child", b =>
                {
                    b.HasOne("Testing.Master", null)
                        .WithMany("Children")
                        .HasForeignKey("Child_Children_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Testing.Child", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("Parent_Id");
                });

            modelBuilder.Entity("Testing.UChild", b =>
                {
                    b.HasOne("Testing.UParentOptional", null)
                        .WithMany("UChildCollection")
                        .HasForeignKey("UChild_UChildCollection_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Testing.UParentRequired", null)
                        .WithMany("UChildCollection")
                        .HasForeignKey("UChild_UChildCollection_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.UParentCollection", b =>
                {
                    b.HasOne("Testing.UChild", "UChildOptional")
                        .WithMany()
                        .HasForeignKey("UChildOptionalId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Testing.UChild", "UChildRequired")
                        .WithMany()
                        .HasForeignKey("UChildRequiredId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Testing.UParentRequired", b =>
                {
                    b.HasOne("Testing.UChild", "UChildOptional")
                        .WithOne()
                        .HasForeignKey("Testing.UParentRequired", "UChild_UChildOptional_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Testing.UChild", "UChildRequired")
                        .WithOne()
                        .HasForeignKey("Testing.UParentRequired", "UChild_UChildRequired_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.UParentOptional", b =>
                {
                    b.HasOne("Testing.UChild", "UChildRequired")
                        .WithOne()
                        .HasForeignKey("Testing.UParentOptional", "UChildRequired_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Testing.UChild", "UChildOptional")
                        .WithOne()
                        .HasForeignKey("Testing.UParentOptional", "UChild_UChildOptional_Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
