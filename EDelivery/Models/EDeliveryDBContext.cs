using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EDelivery.Models
{
    public partial class EDeliveryDBContext : DbContext
    {
        public EDeliveryDBContext()
        {
        }

        public EDeliveryDBContext(DbContextOptions<EDeliveryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConfigurationDelivery> ConfigurationDelivery { get; set; }
        public virtual DbSet<CuisineType> CuisineType { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<DeliveryCompany> DeliveryCompany { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<FoodCategory> FoodCategory { get; set; }
        public virtual DbSet<FoodOrder> FoodOrder { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<RestaurantType> RestaurantType { get; set; }
        public virtual DbSet<WorkingHours> WorkingHours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=s01-test.database.windows.net;Database=EDeliveryDB;User Id=fildzanovM;Password=martin1$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigurationDelivery>(entity =>
            {
                entity.HasKey(e => e.ConfigurationValue);

                entity.Property(e => e.ConfigurationValue).ValueGeneratedNever();

                entity.Property(e => e.ConfigurationDescription)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ConfigurationName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProgrameCode)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CuisineType>(entity =>
            {
                entity.Property(e => e.CuisineTypeId).HasColumnName("CuisineTypeID");

                entity.Property(e => e.CuisineTypeName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerFirstName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerLastName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPassword)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerTelephone)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__Member__4BAC3F29");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");

                entity.Property(e => e.DeliveryName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Delivery)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Delivery__OrderI__6EF57B66");
            });

            modelBuilder.Entity<DeliveryCompany>(entity =>
            {
                entity.Property(e => e.DeliveryCompanyId).HasColumnName("DeliveryCompanyID");

                entity.Property(e => e.DeliveryCompanyEmail)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryCompanyName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryCompanyPassword)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryCompanyTelephone)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.DeliveryCompany)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DeliveryC__Membe__4CA06362");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.DeliveryCompanyId).HasColumnName("DeliveryCompanyID");

                entity.Property(e => e.DriverFirstName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DriverImage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DriverLastName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DriverTelephone)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeliveryCompany)
                    .WithMany(p => p.Driver)
                    .HasForeignKey(d => d.DeliveryCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Driver__Delivery__2C3393D0");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.Property(e => e.FoodId).HasColumnName("FoodID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.FoodDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.FoodImage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FoodName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Food)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__CategoryID__1DE57479");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Food)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__Restaurant__1CF15040");
            });

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FoodOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FoodOrder)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodOrder__Custo__20C1E124");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.AddressName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Location__Custom__1A14E395");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__Location__Restau__1920BF5C");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.MemberEmail)
                    .IsRequired()
                    .HasMaxLength(510)
                    .IsUnicode(false);

                entity.Property(e => e.MemberPassword)
                    .IsRequired()
                    .HasMaxLength(510)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");

                entity.Property(e => e.FoodId).HasColumnName("FoodID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__FoodI__24927208");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Order__239E4DCF");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.CuisineTypeId).HasColumnName("CuisineTypeID");

                entity.Property(e => e.RestaurantDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RestaurantEmail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantImage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RestaurantPassword)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantTelephone)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.CuisineType)
                    .WithMany(p => p.Restaurant)
                    .HasForeignKey(d => d.CuisineTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__Cuisi__36B12243");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Restaurant)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__Membe__4AB81AF0");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Restaurant)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__TypeI__164452B1");
            });

            modelBuilder.Entity<RestaurantType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkingHours>(entity =>
            {
                entity.Property(e => e.WorkingHoursId).HasColumnName("WorkingHoursID");

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.WorkingHours)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkingHo__Resta__2F10007B");
            });
        }
    }
}
