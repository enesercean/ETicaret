using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.User.GetUserProfileDetail
{
    public class GetUserProfileDetailQueryResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string SubscriptionType { get; set; }
    }
}
