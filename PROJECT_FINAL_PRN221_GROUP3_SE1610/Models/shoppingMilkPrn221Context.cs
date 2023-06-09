﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class shoppingMilkPrn221Context : DbContext
    {
        public shoppingMilkPrn221Context()
        {
        }

        public shoppingMilkPrn221Context(DbContextOptions<shoppingMilkPrn221Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Milk> Milk { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("prn221db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("brand_name");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("Cart");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.CartId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cart_id");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("date")
                    .HasColumnName("dateCreate");

                entity.Property(e => e.MilkId).HasColumnName("milk_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Milk)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.MilkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Milk");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Category_Brand");
            });

            modelBuilder.Entity<Milk>(entity =>
            {
                entity.Property(e => e.MilkId).HasColumnName("milk_id");

                entity.Property(e => e.CateId).HasColumnName("cateId");

                entity.Property(e => e.Decription)
                    .HasMaxLength(50)
                    .HasColumnName("decription");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("imageURL");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Published)
                    .HasColumnType("date")
                    .HasColumnName("published");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Milk)
                    .HasForeignKey(d => d.CateId)
                    .HasConstraintName("FK_Milk_Category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Milk)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_Milk_User");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");

                entity.Property(e => e.Phone)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("Order_Detail");

                entity.Property(e => e.OrderDetailId).HasColumnName("orderDetail_id");

                entity.Property(e => e.MilkId).HasColumnName("milkId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Milk)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.MilkId)
                    .HasConstraintName("FK_Order_Detail_Milk");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Order_Detail_Order");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Passwork)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("passwork");

                entity.Property(e => e.Phone)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
