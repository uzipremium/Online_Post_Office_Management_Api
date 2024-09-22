namespace Online_Post_Office_Management_Api.DTO.Response
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
