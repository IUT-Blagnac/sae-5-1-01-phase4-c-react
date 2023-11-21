﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.Data;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(EntityContext))]
    partial class EntityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("backend.Data.Models.Challenge", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("creator_team_id")
                        .HasColumnType("uuid");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("target_team_id")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("creator_team_id");

                    b.HasIndex("target_team_id");

                    b.ToTable("challenge");
                });

            modelBuilder.Entity("backend.Data.Models.RoleUser", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("role_user");
                });

            modelBuilder.Entity("backend.Data.Models.Team", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("team");
                });

            modelBuilder.Entity("backend.Data.Models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("role_id")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("role_id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("backend.Data.Models.UserTeam", b =>
                {
                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("team_id")
                        .HasColumnType("uuid");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("user_id", "team_id");

                    b.HasIndex("team_id");

                    b.ToTable("user_equipe");
                });

            modelBuilder.Entity("backend.Data.Models.Challenge", b =>
                {
                    b.HasOne("backend.Data.Models.Team", "creator_team")
                        .WithMany("creator_challenge")
                        .HasForeignKey("creator_team_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Data.Models.Team", "target_team")
                        .WithMany("target_challenge")
                        .HasForeignKey("target_team_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("creator_team");

                    b.Navigation("target_team");
                });

            modelBuilder.Entity("backend.Data.Models.User", b =>
                {
                    b.HasOne("backend.Data.Models.RoleUser", "role_user")
                        .WithMany("users")
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role_user");
                });

            modelBuilder.Entity("backend.Data.Models.UserTeam", b =>
                {
                    b.HasOne("backend.Data.Models.Team", "team")
                        .WithMany("user_team")
                        .HasForeignKey("team_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Data.Models.User", "user")
                        .WithMany("user_team")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("team");

                    b.Navigation("user");
                });

            modelBuilder.Entity("backend.Data.Models.RoleUser", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("backend.Data.Models.Team", b =>
                {
                    b.Navigation("creator_challenge");

                    b.Navigation("target_challenge");

                    b.Navigation("user_team");
                });

            modelBuilder.Entity("backend.Data.Models.User", b =>
                {
                    b.Navigation("user_team");
                });
#pragma warning restore 612, 618
        }
    }
}
