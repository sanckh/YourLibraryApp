using System.Security.Claims;

namespace YourLibrary.API.Helpers
{
    public class CurrentLoginUserService : ICurrentLoginUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentLoginUserService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public int UserId => Convert.ToInt32(_contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public string FullName => _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value + " " +
            _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Surname)?.Value;

        public string Email => _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        public List<string> Roles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsAdmin => throw new NotImplementedException();

    }
}
