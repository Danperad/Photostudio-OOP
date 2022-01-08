﻿using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotostudioDLL.Entities;

namespace PhotostudioDLL;

public sealed class ApplicationContext : DbContext
{
    internal ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.Migrate();
        ContextDB.AddDB(this);
    }

    internal static DbContextOptions<ApplicationContext> GetDb()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
        if (!File.Exists("appsettings.json"))
        {
            using var sw = File.Open("appsettings.json", FileMode.Create, FileAccess.Write);
            sw.Write(JsonSerializer.SerializeToUtf8Bytes(new AppConfig()));
        }

        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        return optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<People>();
        modelBuilder.Entity<Employee>(EntityConfigure.EmployeeConfigure);
        modelBuilder.Entity<Client>(EntityConfigure.ClientConfigure);
        modelBuilder.Entity<Order>(EntityConfigure.OrderConfigure);
        modelBuilder.Entity<EmployeeProfile>(EntityConfigure.EmployeeProfileConfigure);

        modelBuilder.Entity<Role>(EntityConfigure.RoleDataConfigure);
        modelBuilder.Entity<Employee>(EntityConfigure.EmployeeDataConfigure);
        modelBuilder.Entity<EmployeeProfile>(EntityConfigure.EmployeeProfileDataConfigure);
        modelBuilder.Entity<Client>(EntityConfigure.ClientDataConfigure);
    }

    public static void LoadDb()
    {
        new ApplicationContext(GetDb());
    }

    private class AppConfig
    {
        public AppConfig()
        {
            ConnectionStrings = new Conn();
        }

        private Conn ConnectionStrings { get; }

        private class Conn
        {
            public Conn()
            {
                DefaultConnection = "Host=;Port=;Database=;Username=;Password=";
            }

            private string DefaultConnection { get; }
        }
    }

    #region Tables

    public DbSet<Client> Client { get; set; } = null!;
    public DbSet<Contract> Contract { get; set; } = null!;
    public DbSet<Employee> Employee { get; set; } = null!;
    public DbSet<Role> Role { get; set; } = null!;
    public DbSet<Equipment> Equipment { get; set; } = null!;
    public DbSet<Inventory> Inventory { get; set; } = null!;
    public DbSet<Service> Service { get; set; } = null!;
    public DbSet<Hall> Hall { get; set; } = null!;
    public DbSet<RentedItem> RentedItem { get; set; } = null!;
    public DbSet<ServiceProvided> ServiceProvided { get; set; } = null!;
    public DbSet<Order> Order { get; set; } = null!;

    #endregion
}