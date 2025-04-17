using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierContactPeople.GetSupplierContactPeopleByUser
{
    public class GetSupplierContactPeopleByUserQueryHandler : IRequestHandler<GetSupplierContactPeopleByUserQueryRequest, List<GetSupplierContactPeopleByUserQueryResponse>>
    {
        readonly ISupplierContactPeopleService _supplierContactPeopleService;
        readonly IUserService _userService;
        readonly IRoleService _roleService;

        public GetSupplierContactPeopleByUserQueryHandler(ISupplierContactPeopleService supplierContactPeopleService, IUserService userService, IRoleService roleService)
        {
            _supplierContactPeopleService = supplierContactPeopleService;
            _userService = userService;
            _roleService = roleService;
        }
        public async Task<List<GetSupplierContactPeopleByUserQueryResponse>> Handle(GetSupplierContactPeopleByUserQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserId();

            var scp = await _supplierContactPeopleService.GetUsersForSupplierByUserIdAsync(Guid.Parse(userId));

            var userIdList = scp.Select(scp => scp.UserId.ToString()).ToList();

            var users = await _userService.GetUsersByList(userIdList);

            var usersRoleList = await _roleService.GetUserRoleAuditsByUserIdsAsync(userIdList);

            List<GetSupplierContactPeopleByUserQueryResponse> response = new List<GetSupplierContactPeopleByUserQueryResponse>();

            foreach (var contactPerson in scp)
            {
                var user = users.FirstOrDefault(u => u.Id == contactPerson.UserId.ToString());

                if (user != null)
                {
                    // Kullanıcının en güncel rol ataması
                    var userRole = usersRoleList
                        .Where(r => r.UserId == user.Id)
                        .OrderByDescending(r => r.UpdatedDate)
                        .FirstOrDefault();

                    response.Add(new GetSupplierContactPeopleByUserQueryResponse
                    {
                        Id = contactPerson.UserId,
                        UserName = user.UserName,
                        Name = user.NameSurname ?? "No Name",
                        Email = user.Email,
                        Role = userRole?.RoleName ?? "No Role",
                        AssignedDate = userRole?.UpdatedDate.ToString("yyyy-MM-dd HH:mm") ?? "-"
                    });
                }
            }
            return response;
        }
    }
}
