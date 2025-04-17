using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ETicaretAPI.Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandCategory> BrandCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierAddress> SupplierAddresses { get; set; }
        public DbSet<SupplierContactPerson> SupplierContactPeople { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<SupplierOrderItem> SupplierOrderItems { get; set; }
        public DbSet<CompletedSupplierOrder> CompletedSupplierOrders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductLike> ProductLikes { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<SupplierRegistrationRequest> SupplierRegistrationRequests { get; set; }
        public DbSet<UserRoleAudit> UserRoleAudits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(iul => new { iul.LoginProvider, iul.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(iut => new { iut.UserId, iut.LoginProvider, iut.Name });

            modelBuilder.Entity<AppUser>()
                .HasAlternateKey(u => u.Id);

            // Basket - BasketItem ilişki
            modelBuilder.Entity<Basket>()
                .HasMany(b => b.BasketItems)
                .WithOne(bi => bi.Basket)
                .HasForeignKey(bi => bi.BasketId);

            // BasketItem - Product ilişki
            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Product)
                .WithMany(p => p.BasketItems)
                .HasForeignKey(bi => bi.ProductId);

            // Basket - User ilişki
            modelBuilder.Entity<Basket>()
                .HasOne(b => b.User)
                .WithMany(u => u.Baskets)
                .HasForeignKey(b => b.UserId)
                .HasPrincipalKey(u => u.Id);

            // Decimal tipi için veritabanında uygun bir alan tipi belirleme
            modelBuilder.Entity<InvoiceFile>()
                .Property(i => i.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(o => o.Id);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderTrackingNumber)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CompletedOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CompletedOrder>(c => c.Id);

            modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId }); // Composite Key (Bileşik Anahtar)

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false);

            modelBuilder.Entity<BrandCategory>()
                .HasKey(bc => new { bc.BrandId, bc.CategoryId });

            modelBuilder.Entity<BrandCategory>()
                .HasOne(bc => bc.Brand)
                .WithMany(b => b.BrandCategories)
                .HasForeignKey(bc => bc.BrandId)
                .OnDelete(DeleteBehavior.Cascade); // Silme davranışı

            modelBuilder.Entity<BrandCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BrandCategories)
                .HasForeignKey(bc => bc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Silme davranışı

            // Product ve Brand arasındaki ilişki
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products) // Bir markanın birden fazla ürünü olabilir
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull); // Markası silindiğinde ürünlerin markası null yapılacak

            // Bir ürünün sadece bir tedarikçisi olabilir ve tedarikçi silindiğinde ürünler de silinir.
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tedarikçi silindiğinde, ilişkili adresleri de sil.
            modelBuilder.Entity<SupplierAddress>()
                .HasOne(sa => sa.Supplier)
                .WithMany(s => s.SupplierAddresses)
                .HasForeignKey(sa => sa.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tedarikçi silindiğinde, ilişkili yetkili kişiler de sil.
            modelBuilder.Entity<SupplierContactPerson>()
                .HasOne(scp => scp.Supplier)
                .WithMany(s => s.SupplierContactPeople)
                .HasForeignKey(scp => scp.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            // SupplierContactPerson -> AppUser ilişkisi
            modelBuilder.Entity<SupplierContactPerson>()
                .HasOne(scp => scp.User)
                .WithMany()  // AppUser tarafında liste tutmayacağız, tek yönlü ilişki
                .HasForeignKey(scp => scp.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Kullanıcı silindiğinde yetkili kişi kaydı silinmesin
            //----
            modelBuilder.Entity<SupplierContactPerson>()
                .HasOne(scp => scp.User)
                .WithMany()  // AppUser tarafında liste tutmadığımız için burası boş.
                .HasForeignKey(scp => scp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierContactPerson>()
                .HasOne(scp => scp.User)
                .WithMany(u => u.SupplierContactPeople) // Kullanıcı, hangi tedarikçilerle ilişkili olduğunu bilecek
                .HasForeignKey(scp => scp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // SupplierOrder konfigürasyonu
            modelBuilder.Entity<SupplierOrder>(entity =>
            {
                entity.HasKey(so => so.Id);

                entity.Property(so => so.OrderTrackingNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(so => so.Description)
                      .HasMaxLength(500);

                entity.Property(so => so.Address)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(so => so.TotalPrice)
                      .IsRequired();

                entity.HasOne(so => so.Order)
                      .WithMany()
                      .HasForeignKey(so => so.OrderId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(so => so.Supplier)
                      .WithMany()
                      .HasForeignKey(so => so.SupplierId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(so => so.SupplierOrderItems)
                      .WithOne(soi => soi.SupplierOrder)
                      .HasForeignKey(soi => soi.SupplierOrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // SupplierOrderItem konfigürasyonu
            modelBuilder.Entity<SupplierOrderItem>(entity =>
            {
                entity.HasKey(soi => soi.Id);

                entity.Property(soi => soi.Quantity)
                      .IsRequired();

                entity.Property(soi => soi.Price)
                      .IsRequired();

                entity.HasOne(soi => soi.SupplierOrder)
                      .WithMany(so => so.SupplierOrderItems)
                      .HasForeignKey(soi => soi.SupplierOrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(soi => soi.Product)
                      .WithMany()
                      .HasForeignKey(soi => soi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // CompletedSupplierOrder konfigürasyonu
            modelBuilder.Entity<CompletedSupplierOrder>(entity =>
            {
                entity.HasKey(cso => cso.Id);

                entity.Property(cso => cso.OrderTrackingNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne(cso => cso.SupplierOrder)
                      .WithMany()
                      .HasForeignKey(cso => cso.Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Order - SupplierOrder ilişkisi
            modelBuilder.Entity<Order>()
                .HasMany(o => o.SupplierOrders) // Order'ın birden fazla SupplierOrder'ı olabilir
                .WithOne(so => so.Order) // SupplierOrder'ın bir Order'ı olabilir
                .HasForeignKey(so => so.OrderId); // ForeignKey olarak OrderId kullanılır

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment) // Order'ın bir Payment'ı var
                .WithOne(p => p.Order)  // Payment'ın bir Order'ı var
                .HasForeignKey<Payment>(p => p.OrderId) // Payment'ın OrderId'si foreign key
                .OnDelete(DeleteBehavior.Cascade); // Order silindiğinde Payment da silinsin

            // Payment tablosu yapılandırması
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(50);
                entity.Property(p => p.CardNumber).IsRequired().HasMaxLength(16);
                entity.Property(p => p.CardHolderName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.ExpirationDate).IsRequired();
                entity.Property(p => p.CVV).IsRequired().HasMaxLength(3);
                entity.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18, 2)");
                entity.Property(p => p.PaymentDate).IsRequired();
                entity.Property(p => p.PaymentStatus).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<ProductLike>()
                .HasKey(pl => new { pl.UserId, pl.ProductId });

            modelBuilder.Entity<ProductLike>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.ProductLikes)
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductLike>()
                .HasOne(pl => pl.Product)
                .WithMany(p => p.ProductLikes)
                .HasForeignKey(pl => pl.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductRating>(entity =>
            {
                entity.HasKey(pr => pr.Id);

                entity.Property(pr => pr.RatingValue)
                    .IsRequired()
                    .HasAnnotation("Range", new[] { 1, 5 });

                // Bir kullanıcı bir ürüne sadece bir kez puan verebilir
                entity.HasIndex(pr => new { pr.UserId, pr.ProductId }).IsUnique();

                entity.HasOne(pr => pr.Product)
                    .WithMany(p => p.ProductRatings)
                    .HasForeignKey(pr => pr.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pr => pr.User)
                    .WithMany(u => u.ProductRatings)
                    .HasForeignKey(pr => pr.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Address>(entity =>
                {
                    entity.HasKey(a => a.Id);

                    entity.Property(a => a.Street).IsRequired().HasMaxLength(200);
                    entity.Property(a => a.City).IsRequired().HasMaxLength(100);
                    entity.Property(a => a.State).IsRequired().HasMaxLength(100);
                    entity.Property(a => a.PostalCode).IsRequired().HasMaxLength(20);
                    entity.Property(a => a.Country).IsRequired().HasMaxLength(100);

                    entity.HasOne(a => a.User)
                        .WithMany(u => u.Addresses)
                        .HasForeignKey(a => a.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // SupplierRegistrationRequest yapılandırması
                modelBuilder.Entity<SupplierRegistrationRequest>(entity =>
                {
                    entity.HasKey(sr => sr.Id);

                    // Temel Bilgiler
                    entity.Property(sr => sr.BusinessName)
                        .IsRequired()
                        .HasMaxLength(255);

                    entity.Property(sr => sr.BusinessType)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(sr => sr.FirstName)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(sr => sr.LastName)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(sr => sr.Email)
                        .IsRequired()
                        .HasMaxLength(255);

                    entity.Property(sr => sr.Phone)
                        .IsRequired()
                        .HasMaxLength(20);

                    // İşletme Detayları
                    entity.Property(sr => sr.TaxId)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(sr => sr.BusinessAddress)
                        .IsRequired()
                        .HasMaxLength(500);

                    entity.Property(sr => sr.City)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(sr => sr.State)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(sr => sr.PostalCode)
                        .IsRequired()
                        .HasMaxLength(20);

                    entity.Property(sr => sr.Country)
                        .IsRequired()
                        .HasMaxLength(100);

                    // Ürün Bilgileri
                    entity.Property(sr => sr.ProductCategories)
                        .IsRequired()
                        .HasColumnType("text"); // JSON verisi için text tipi

                    entity.Property(sr => sr.ProductDescription)
                        .IsRequired()
                        .HasMaxLength(1000);

                    entity.Property(sr => sr.AveragePrice)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(sr => sr.ShippingMethod)
                        .IsRequired()
                        .HasMaxLength(100);

                    // Kullanıcı İlişkileri    
                    // Form gönderen kullanıcı ile ilişki
                    entity.Property(sr => sr.CreatedById)
                        .IsRequired()
                        .HasMaxLength(450); // Identity kullanıcı ID uzunluğu

                    entity.HasOne(sr => sr.CreatedBy)
                        .WithMany()
                        .HasForeignKey(sr => sr.CreatedById)
                        .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silindiğinde kayıt silinmesin

                    // Durum Bilgisi
                    entity.Property(sr => sr.Status)
                        .IsRequired()
                        .HasDefaultValue(RegistrationStatus.Pending);

                    entity.Property(sr => sr.RejectionReason)
                        .HasMaxLength(500);

                    entity.Property(sr => sr.ReviewedById)
                        .HasMaxLength(450); // Identity kullanıcı ID uzunluğu

                    // İnceleme yapan kullanıcı ile ilişki
                    entity.HasOne(sr => sr.ReviewedBy)
                        .WithMany()
                        .HasForeignKey(sr => sr.ReviewedById)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.SetNull); // Kullanıcı silindiğinde null olsun

                    // Kayıt tamamlanma durumu
                    entity.Property(sr => sr.IsTransferred)
                        .IsRequired()
                        .HasDefaultValue(false);
                });
            });

            modelBuilder.Entity<UserRoleAudit>(entity =>
            {
                entity.HasKey(ura => ura.Id);

                entity.Property(ura => ura.UserId)
                    .IsRequired()
                    .HasMaxLength(450); // Identity kullanıcı ID'leri genellikle bu uzunluktadır

                entity.Property(ura => ura.RoleId)
                    .IsRequired()
                    .HasMaxLength(450); // Identity rol ID'leri genellikle bu uzunluktadır

                entity.Property(ura => ura.RoleName)
                    .IsRequired()
                    .HasMaxLength(256); // IdentityRole'de bu uzunluk kullanılır

                entity.Property(ura => ura.ActionType)
                    .IsRequired();

                entity.Property(ura => ura.ActionByUserId)
                    .HasMaxLength(450);

                entity.Property(ura => ura.Reason)
                    .HasMaxLength(500);

                entity.Property(ura => ura.IsActive)
                    .IsRequired()
                    .HasDefaultValue(true);

                // UserId ile AppUser tablosuna ilişki kurma
                entity.HasOne<AppUser>()
                    .WithMany()
                    .HasForeignKey(ura => ura.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Rol silme durumunda audit kayıtları korunsun
                entity.HasOne<AppRole>()
                    .WithMany()
                    .HasForeignKey(ura => ura.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

                // ActionByUserId ile AppUser tablosuna isteğe bağlı ilişki kurma
                entity.HasOne<AppUser>()
                    .WithMany()
                    .HasForeignKey(ura => ura.ActionByUserId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                // Kullanıcı ve rol bazlı indeks oluşturma (performans için)
                entity.HasIndex(ura => ura.UserId);
                entity.HasIndex(ura => ura.RoleId);
                entity.HasIndex(ura => new { ura.UserId, ura.RoleId });
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
