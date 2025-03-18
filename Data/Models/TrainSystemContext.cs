using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrainSystem.Data.Models;

public partial class TrainSystemContext : DbContext
{
    public TrainSystemContext()
    {
    }

    public TrainSystemContext(DbContextOptions<TrainSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Train_System;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeesId).HasName("PK__Employee__268C6A6C61700F2F");

            entity.Property(e => e.EmployeesId).HasColumnName("employees_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
            entity.Property(e => e.TrainId).HasColumnName("train_id");

            entity.HasOne(d => d.Train).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Employees__train__440B1D61");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RoutesId).HasName("PK__Routes__B1CE2743E1F9551A");

            entity.Property(e => e.RoutesId).HasColumnName("routes_id");
            entity.Property(e => e.ArrivalStation)
                .HasMaxLength(100)
                .HasColumnName("arrival_station");
            entity.Property(e => e.ArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("arrival_time");
            entity.Property(e => e.DepartureStation)
                .HasMaxLength(100)
                .HasColumnName("departure_station");
            entity.Property(e => e.DepartureTime)
                .HasColumnType("datetime")
                .HasColumnName("departure_time");
            entity.Property(e => e.TrainId).HasColumnName("train_id");

            entity.HasOne(d => d.Train).WithMany(p => p.Routes)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Routes__train_id__398D8EEE");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketsId).HasName("PK__Tickets__9BF66790D24C5404");

            entity.Property(e => e.TicketsId).HasColumnName("tickets_id");
            entity.Property(e => e.PassengerName)
                .HasMaxLength(100)
                .HasColumnName("passenger_name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.RouteId).HasColumnName("route_id");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(10)
                .HasColumnName("seat_number");
            entity.Property(e => e.TrainId).HasColumnName("train_id");

            entity.HasOne(d => d.Route).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__Tickets__route_i__412EB0B6");

            entity.HasOne(d => d.Train).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Tickets__train_i__403A8C7D");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.TracksId).HasName("PK__Tracks__2C299185E81E21FA");

            entity.HasIndex(e => e.TrackNumber, "UQ__Tracks__1C938728125579C4").IsUnique();

            entity.Property(e => e.TracksId).HasColumnName("tracks_id");
            entity.Property(e => e.StationName)
                .HasMaxLength(100)
                .HasColumnName("station_name");
            entity.Property(e => e.TrackNumber).HasColumnName("track_number");
            entity.Property(e => e.TrainId).HasColumnName("train_id");

            entity.HasOne(d => d.Train).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Tracks__train_id__3D5E1FD2");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.TrainId).HasName("PK__Trains__9F1BFABD0F27AA22");

            entity.HasIndex(e => e.TrainNumber, "UQ__Trains__55C242D1097CAF33").IsUnique();

            entity.Property(e => e.TrainId).HasColumnName("train_ID");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.TrainNumber)
                .HasMaxLength(10)
                .HasColumnName("train_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
