using System;

namespace ETicaretAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
