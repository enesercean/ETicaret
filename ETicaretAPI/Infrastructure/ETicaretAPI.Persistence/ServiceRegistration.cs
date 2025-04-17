using ETicaretAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Application.Repositories.CustomerRepositories;
using ETicaretAPI.Persistence.Repositories.CustomerRepositories;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using ETicaretAPI.Persistence.Repositories.ProductRepositories;
using ETicaretAPI.Persistence.Repositories.File;
using ETicaretAPI.Persistence.Repositories.ProductImageFile;
using ETicaretAPI.Persistence.Repositories.InvoiceFile;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Persistence.Services;
using ETicaretAPI.Application.Abstractions.Services.Authentications;
using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Persistence.Repositories.Basket;
using ETicaretAPI.Application.Repositories.BasketItem;
using ETicaretAPI.Persistence.Repositories.BasketItem;
using ETicaretAPI.Application.Repositories.CompletedOrderRepositories;
using ETicaretAPI.Persistence.Repositories.CompletedOrder;
using ETicaretAPI.Application.Repositories.ProductCategory;
using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Persistence.Repositories.ProductCategory;
using ETicaretAPI.Persistence.Repositories.Category;
using ETicaretAPI.Application.Repositories.BrandCategory;
using ETicaretAPI.Persistence.Repositories.BrandCategory;
using ETicaretAPI.Application.Repositories.Brand;
using ETicaretAPI.Persistence.Repositories.Brand;
using ETicaretAPI.Application.Repositories.SupplierAddress;
using ETicaretAPI.Persistence.Repositories.SupplierAddress;
using ETicaretAPI.Application.Repositories.Supplier;
using ETicaretAPI.Persistence.Repositories.Supplier;
using ETicaretAPI.Application.Repositories.SupplierContactPerson;
using ETicaretAPI.Persistence.Repositories.SupplierContactPerson;
using ETicaretAPI.Persistence.Repositories.SupplierOrder;
using ETicaretAPI.Application.Repositories.SupplierOrder;
using ETicaretAPI.Persistence.Repositories.SupplierOrderItem;
using ETicaretAPI.Application.Repositories.SupplierOrderItem;
using ETicaretAPI.Persistence.Repositories.CompletedSupplierOrder;
using ETicaretAPI.Application.Repositories.CompletedSupplierOrder;
using ETicaretAPI.Application.Repositories.ProductLike;
using ETicaretAPI.Persistence.Repositories.ProductLike;
using ETicaretAPI.Application.Repositories.ProductRating;
using ETicaretAPI.Persistence.Repositories.ProductRating;
using ETicaretAPI.Application.Repositories.Address;
using ETicaretAPI.Persistence.Repositories.Address;
using ETicaretAPI.Application.Repositories.ISupplierRegistrationRequest;
using ETicaretAPI.Persistence.Repositories.SupplierRegistrationRequest;
using ETicaretAPI.Application.Repositories.UserRoleAudit;
using ETicaretAPI.Persistence.Repositories.UserRoleAudit;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ETicaretAPIDbContext>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
            services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();
            services.AddScoped<IProductCategoryReadRepository,ProductCategoryReadRepository>();
            services.AddScoped<IProductCategoryWriteRepository,ProductCategoryWriteRepository>();
            services.AddScoped<ICategoryWriteRepository,CategoryWriteRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<IBrandCategoryReadRepository, BrandCategoryReadRepository>();
            services.AddScoped<IBrandCategoryWriteRepository, BrandCategoryWriteRepository>();
            services.AddScoped<IBrandWriteRepository, BrandWriteRepository>();
            services.AddScoped<IBrandReadRepository, BrandReadRepository>();
            services.AddScoped<ISupplierAddressWriteRepository, SupplierAddressWriteRepository>();
            services.AddScoped<ISupplierAddressReadRepository, SupplierAddressReadRepository>();
            services.AddScoped<ISupplierReadRepository, SupplierReadRepository>();
            services.AddScoped<ISupplierWriteRepository, SupplierWriteRepository>();
            services.AddScoped<ISupplierContactPeopleReadRepository, SupplierContactPersonReadRepository>();
            services.AddScoped<ISupplierContactPeopleWriteRepository, SupplierContactPersonWriteRepository>();
            services.AddScoped<ISupplierOrderReadRepository, SupplierOrderReadRepository>();
            services.AddScoped<ISupplierOrderWriteRepository, SupplierOrderWriteRepository>();
            services.AddScoped<ISupplierOrderItemReadRepository, SupplierOrderItemReadRepository>();
            services.AddScoped<ISupplierOrderItemWriteRepository, SupplierOrderItemWriteRepository>();
            services.AddScoped<ICompletedSupplierOrderReadRepository, CompletedSupplierOrderReadRepository>();
            services.AddScoped<ICompletedSupplierOrderWriteRepository, CompletedSupplierOrderWriteRepository>();
            services.AddScoped<IProductLikeReadRepository, ProductLikeReadRepository>();
            services.AddScoped<IProductLikeWriteRepository, ProductLikeWriteRepository>();
            services.AddScoped<IProductRatingReadRepository, ProductRatingReadRepository>();
            services.AddScoped<IProductRatingWriteRepository, ProductRatingWriteRepository>();
            services.AddScoped<IAddressReadRepository, AddressReadRepository>();
            services.AddScoped<IAddressWriteRepository, AddressWriteRepository>();
            services.AddScoped<ISupplierRegistrationRequestReadRepository, SupplierRegistrationRequestReadRepository>();
            services.AddScoped<ISupplierRegistrationRequestWriteRepository, SupplierRegistrationRequestWriteRepository>();
            services.AddScoped<IUserRoleAuditReadRepository, UserRoleAuditReadRepository>();
            services.AddScoped<IUserRoleAuditWriteRepository, UserRoleAuditWriteRepository>();



            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplierAddressService, SupplierAddressService>();
            services.AddScoped<ISupplierOrderService, SupplierOrderService>();
            services.AddScoped<ISupplierContactPeopleService, SupplierContactPeopleService>();
            services.AddScoped<IProductLikeService, ProductLikeService>();
            services.AddScoped<ISupplierOrderItemService, SupplierOrderItemService>();
            services.AddScoped<IProductRatingService, ProductRatingService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ISupplierRegistrationRequestService, SupplierRegistrationRequestService>();
            services.AddScoped<IRoleService, RoleService>();

        }
    }
}
