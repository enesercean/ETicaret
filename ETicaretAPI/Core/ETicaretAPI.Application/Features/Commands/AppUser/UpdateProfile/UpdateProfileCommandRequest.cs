using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.UpdateProfile
{
    public class UpdateProfileCommandRequest : IRequest<UpdateProfileCommandResponse>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
