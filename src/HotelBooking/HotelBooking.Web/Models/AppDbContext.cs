using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Web.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingRoom> BookingRooms { get; set; }

    public virtual DbSet<BookingStatus> BookingStatuses { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomCategory> RoomCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())", "DF_Bookings_CreatedAt");

            entity.HasOne(d => d.Guest).WithMany(p => p.Bookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Guests");

            entity.HasOne(d => d.Status).WithMany(p => p.Bookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_BookingStatuses");
        });

        modelBuilder.Entity<BookingRoom>(entity =>
        {
            entity.HasOne(d => d.Booking).WithMany(p => p.BookingRooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingRooms_Bookings");

            entity.HasOne(d => d.Room).WithMany(p => p.BookingRooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingRooms_Rooms");
        });

        modelBuilder.Entity<BookingStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.IsAvailable).HasDefaultValue(true, "DF_Rooms_IsAvailable");

            entity.HasOne(d => d.Category).WithMany(p => p.Rooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_RoomCategories");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}