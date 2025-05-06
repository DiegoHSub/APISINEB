namespace APISINEB.Models.Dtos
{
    public class UserLoginResponseDto
    {
        public Users User { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
