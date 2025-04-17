using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(2)
                .WithMessage("Lütfen ürün adını en az 2 karakter giriniz...");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen stok bilgisi giriniz...")
                .Must(s => s >= 0)
                .WithMessage("Lütfen geçerli stok bilgisi giriniz...");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen fiyat giriniz...")
                .Must(s => s >= 0)
                .WithMessage("Lütfen geçerli bir fiyat giriniz...");
        }
    }
}
