using System;
namespace OAuth2.Model
{
    public class TokenResponse : Response
    {
        public TokenResponse()
        {
        }

        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
    }
}
