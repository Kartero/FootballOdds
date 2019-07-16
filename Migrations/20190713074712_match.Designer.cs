﻿// <auto-generated />
using System;
using FootballOdds.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FootballOdds.Migrations
{
    [DbContext(typeof(FootballOddsContext))]
    [Migration("20190713074712_match")]
    partial class match
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FootballOdds.Models.ResourceModels.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Awaygoals");

                    b.Property<int?>("Awayteam");

                    b.Property<int>("Homegoals");

                    b.Property<int?>("Hometeam");

                    b.Property<DateTime>("MatchDay");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.HasIndex("Awayteam");

                    b.HasIndex("Hometeam");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("FootballOdds.Models.ResourceModels.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Team");
                });

            modelBuilder.Entity("FootballOdds.Models.ResourceModels.Match", b =>
                {
                    b.HasOne("FootballOdds.Models.ResourceModels.Team", "TeamAway")
                        .WithMany()
                        .HasForeignKey("Awayteam");

                    b.HasOne("FootballOdds.Models.ResourceModels.Team", "TeamHome")
                        .WithMany()
                        .HasForeignKey("Hometeam");
                });
#pragma warning restore 612, 618
        }
    }
}
