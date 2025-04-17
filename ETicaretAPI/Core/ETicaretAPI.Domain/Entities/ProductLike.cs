
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Domain.Entities
{
    public class ProductLike : BaseEntity
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }

        public AppUser User { get; set; }
        public Product Product { get; set; }
    }
}
