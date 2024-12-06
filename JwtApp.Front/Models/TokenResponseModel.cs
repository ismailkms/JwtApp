namespace JwtApp.Front.Models
{
    public class TokenResponseModel
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
