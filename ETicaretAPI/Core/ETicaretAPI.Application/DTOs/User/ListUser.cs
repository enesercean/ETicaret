using System;

namespace ETicaretAPI.Application.DTOs.User
{
    public class ListUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
