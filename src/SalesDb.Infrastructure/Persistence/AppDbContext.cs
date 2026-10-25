using Microsoft.EntityFrameworkCore;
using SalesDb.Domain.Entities;
using SalesDb.Domain.Interfaces;

namespace SalesDb.Infrastructure.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Employee>().Property(x => x.Id).HasColumnName("EmployeeID");
        b.Entity<Order>().Property(x => x.TotalAmount).HasPrecision(18, 2);
        // EF Core: lưu OrderStatus enum thành chuỗi trong database.
        b.Entity<Order>().Property(x => x.Status).HasConversion<string>();
        b.Entity<Order>()
            .HasMany(x => x.OrderDetails)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
        b.Entity<Order>().Navigation(x => x.OrderDetails).UsePropertyAccessMode(PropertyAccessMode.Field);
        b.Entity<OrderDetail>().Property(x => x.Price).HasPrecision(18, 2);
        b.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
        b.Entity<Product>().HasMany(x => x.ProductImages).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
        b.Entity<Product>()
            .Navigation(p => p.ProductImages)
            .HasField("_images")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        b.Entity<Order>().HasOne(o => o.Employee).WithMany().HasForeignKey(o => o.EmployeeID);
        b.Entity<ProductImage>()
         .HasIndex(x => x.ProductId)
         .IsUnique()
         .HasFilter("[IsPrimary] = 1");
        b.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
