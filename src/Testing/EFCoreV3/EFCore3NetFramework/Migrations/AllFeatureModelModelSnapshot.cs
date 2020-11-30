﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Testing;

namespace EFCore3NetFramework.Migrations
{
    [DbContext(typeof(AllFeatureModel))]
    partial class AllFeatureModelModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Testing.AllPropertyTypesOptional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BParentOptional_1Id")
                        .HasColumnType("int");

                    b.Property<int?>("BParentOptional_2Id")
                        .HasColumnType("int");

                    b.Property<int?>("BParentRequiredId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("BParentRequired_1Id")
                        .HasColumnType("int");

                    b.Property<int?>("BParentRequired_2Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BParentOptional_1Id");

                    b.HasIndex("BParentOptional_2Id")
                        .IsUnique()
                        .HasFilter("[BParentOptional_2Id] IS NOT NULL");

                    b.HasIndex("BParentRequiredId")
                        .IsUnique();

                    b.HasIndex("BParentRequired_1Id")
                        .IsUnique()
                        .HasFilter("[BParentRequired_1Id] IS NOT NULL");

                    b.HasIndex("BParentRequired_2Id");

                    b.ToTable("BChilds");
                });

            modelBuilder.Entity("Testing.BParentCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BChildOptionalId")
                        .HasColumnType("int");

                    b.Property<int?>("BChildRequiredId")
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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BChildRequiredId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BChildRequiredId")
                        .IsUnique()
                        .HasFilter("[BChildRequiredId] IS NOT NULL");

                    b.ToTable("BParentOptionals");
                });

            modelBuilder.Entity("Testing.BParentRequired", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("BParentRequireds");
                });

            modelBuilder.Entity("Testing.BaseClassWithRequiredProperties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Master_Children_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Master_Children_Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("Testing.HiddenEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Masters");
                });

            modelBuilder.Entity("Testing.ParserTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasColumnName("Foo")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("RenamedColumns");
                });

            modelBuilder.Entity("Testing.UChild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UParentOptional_UChildCollection_Id")
                        .HasColumnType("int");

                    b.Property<int?>("UParentOptional_UChildOptional_Id")
                        .HasColumnType("int");

                    b.Property<int?>("UParentRequired_UChildCollection_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("UParentRequired_UChildOptional_Id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("UParentRequired_UChildRequired_Id")
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
                        .IsUnique()
                        .HasFilter("[UParentRequired_UChildRequired_Id] IS NOT NULL");

                    b.ToTable("UChilds");
                });

            modelBuilder.Entity("Testing.UParentCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UChildRequiredId")
                        .HasColumnType("int");

                    b.HasIndex("UChildRequiredId")
                        .IsUnique()
                        .HasFilter("[UChildRequiredId] IS NOT NULL");

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
                        .HasColumnName("ConcreteDerivedClassWithRequiredProperties_Property1")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ConcreteDerivedClassWithRequiredProperties");
                });

            modelBuilder.Entity("Testing.DerivedClass", b =>
                {
                    b.HasBaseType("Testing.BaseClass");

                    b.Property<string>("Property1")
                        .HasColumnName("DerivedClass_Property1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyInChild")
                        .HasColumnName("DerivedClass_PropertyInChild")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("DerivedClass");
                });

            modelBuilder.Entity("Testing.AllPropertyTypesOptional", b =>
                {
                    b.OwnsOne("Testing.OwnedType", "OwnedType", b1 =>
                        {
                            b1.Property<int>("AllPropertyTypesOptionalId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasForeignKey("BParentOptional_1Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testing.BParentOptional", "BParentOptional_2")
                        .WithOne("BChildOptional")
                        .HasForeignKey("Testing.BChild", "BParentOptional_2Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testing.BParentRequired", "BParentRequired")
                        .WithOne("BChildOptional")
                        .HasForeignKey("Testing.BChild", "BParentRequiredId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Testing.BParentRequired", "BParentRequired_1")
                        .WithOne("BChildRequired")
                        .HasForeignKey("Testing.BChild", "BParentRequired_1Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testing.BParentRequired", "BParentRequired_2")
                        .WithMany("BChildCollection")
                        .HasForeignKey("BParentRequired_2Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.BParentCollection", b =>
                {
                    b.HasOne("Testing.BChild", "BChildOptional")
                        .WithMany("BParentCollection_2")
                        .HasForeignKey("BChildOptionalId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testing.BChild", "BChildRequired")
                        .WithMany("BParentCollection")
                        .HasForeignKey("BChildRequiredId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Testing.BParentOptional", b =>
                {
                    b.HasOne("Testing.BChild", "BChildRequired")
                        .WithOne("BParentOptional")
                        .HasForeignKey("Testing.BParentOptional", "BChildRequiredId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Testing.Child", b =>
                {
                    b.HasOne("Testing.Master", null)
                        .WithMany("Children")
                        .HasForeignKey("Master_Children_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Testing.Child", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testing.UChild", b =>
                {
                    b.HasOne("Testing.UParentOptional", null)
                        .WithMany("UChildCollection")
                        .HasForeignKey("UParentOptional_UChildCollection_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testing.UParentOptional", null)
                        .WithOne("UChildOptional")
                        .HasForeignKey("Testing.UChild", "UParentOptional_UChildOptional_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testing.UParentRequired", null)
                        .WithMany("UChildCollection")
                        .HasForeignKey("UParentRequired_UChildCollection_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Testing.UParentRequired", null)
                        .WithOne("UChildOptional")
                        .HasForeignKey("Testing.UChild", "UParentRequired_UChildOptional_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Testing.UParentRequired", null)
                        .WithOne("UChildRequired")
                        .HasForeignKey("Testing.UChild", "UParentRequired_UChildRequired_Id")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Testing.UParentCollection", b =>
                {
                    b.HasOne("Testing.UChild", "UChildOptional")
                        .WithMany()
                        .HasForeignKey("UChildOptionalId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testing.UChild", "UChildRequired")
                        .WithMany()
                        .HasForeignKey("UChildRequiredId");
                });

            modelBuilder.Entity("Testing.UParentOptional", b =>
                {
                    b.HasOne("Testing.UChild", "UChildRequired")
                        .WithOne()
                        .HasForeignKey("Testing.UParentOptional", "UChildRequiredId")
                        .OnDelete(DeleteBehavior.NoAction);
                });
#pragma warning restore 612, 618
        }
    }
}
