namespace YourLibrary.API.Helpers
{
    public interface ICurrentLoginUserService
    {
        public int UserId { get; }
        public string FullName { get; }
        public string Email { get; }
        List<string> Roles { get; set; }
        bool IsAdmin { get; }

    }
}
