// <auto-generated />
using AirportsApi.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AirportsApi.Migrations
{
    [DbContext(typeof(AirportsContext))]
    partial class AirportsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AirportsApi.Entities.Airport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("AirportsApi.Entities.Flight", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<double>("Bearing")
                        .HasColumnType("double precision");

                    b.Property<string>("DepartureId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DestinationId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<double>("TotalDistance")
                        .HasColumnType("double precision");

                    b.Property<double>("TraveledDistance")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("DepartureId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("AirportsApi.Entities.Flight", b =>
                {
                    b.HasOne("AirportsApi.Entities.Airport", "Departure")
                        .WithMany()
                        .HasForeignKey("DepartureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirportsApi.Entities.Airport", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departure");

                    b.Navigation("Destination");
                });
#pragma warning restore 612, 618
        }
    }
}
