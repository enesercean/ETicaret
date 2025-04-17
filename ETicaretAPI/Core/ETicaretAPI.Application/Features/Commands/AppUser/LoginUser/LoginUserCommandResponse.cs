using ETicaretAPI.Application.DTOs;

namespace ETicaretAPI.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandResponse
    {

    }

    public class LoginUserSuccessResponse : LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }

    public class LoginUserFailureResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
