﻿// <auto-generated />
using System;
using LuaEngine.Scripts.WebApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LuaEngine.Scripts.WebApi.Migrations
{
    [DbContext(typeof(ScriptsContext))]
    partial class ScriptsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LuaEngine.Scripts.WebApi.Models.ProcessScript", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("uuid")
                        .HasColumnName("source_id");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("process_scripts", (string)null);
                });

            modelBuilder.Entity("LuaEngine.Scripts.WebApi.Models.RuleScript", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean")
                        .HasColumnName("enabled");

                    b.Property<bool>("Prefilter")
                        .HasColumnType("boolean")
                        .HasColumnName("prefilter");

                    b.Property<int>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("priority");

                    b.Property<Guid>("ProcessScriptId")
                        .HasColumnType("uuid")
                        .HasColumnName("process_script_id");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("uuid")
                        .HasColumnName("source_id");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("rule_scripts", (string)null);
                });

            modelBuilder.Entity("LuaEngine.Scripts.WebApi.Models.ScriptVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_id");

                    b.Property<Guid?>("ProcessScriptId")
                        .HasColumnType("uuid")
                        .HasColumnName("process_script_id");

                    b.Property<Guid?>("RuleScriptId")
                        .HasColumnType("uuid")
                        .HasColumnName("rule_script_id");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint")
                        .HasColumnName("type");

                    b.Property<int>("Version")
                        .HasColumnType("integer")
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProcessScriptId");

                    b.HasIndex("RuleScriptId");

                    b.HasIndex("Type");

                    b.ToTable("script_versions", (string)null);
                });

            modelBuilder.Entity("LuaEngine.Scripts.WebApi.Models.ScriptVersion", b =>
                {
                    b.HasOne("LuaEngine.Scripts.WebApi.Models.ScriptVersion", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("LuaEngine.Scripts.WebApi.Models.ProcessScript", "ProcessScript")
                        .WithMany()
                        .HasForeignKey("ProcessScriptId");

                    b.HasOne("LuaEngine.Scripts.WebApi.Models.RuleScript", "RuleScript")
                        .WithMany()
                        .HasForeignKey("RuleScriptId");

                    b.Navigation("Parent");

                    b.Navigation("ProcessScript");

                    b.Navigation("RuleScript");
                });
#pragma warning restore 612, 618
        }
    }
}
