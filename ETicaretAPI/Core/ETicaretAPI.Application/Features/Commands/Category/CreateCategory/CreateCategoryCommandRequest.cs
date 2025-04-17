using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandRequest : IRequest<CreateCategoryCommandResponse>
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
