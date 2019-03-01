using System;
using System.Text;

namespace OAuth2.Model
{
    public class TokenRequest : Request
    {
        public TokenRequest()
        {
        }

        public string GetDefaultContent()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("grant_type={0}", grant_type);
            stringBuilder.AppendFormat("&client_id={0}", client_id);
            stringBuilder.AppendFormat("&scope={0}", scope);
            stringBuilder.AppendFormat("&client_secret={0}", client_secret);
            return stringBuilder.ToString();
        }

        public string Address = "https://auth.dev.az.nz/oauth2/token/";

        public string client_id = "45mvsvgqcrfs4f73jlp85paaqq";
        public string client_secret = "tmbe69ekvku1h6180oj11ugf8b37ovigadfrsro1il65q8596ps";
        public string scope = "keeper/document.archive keeper/document.read";
        public string grant_type = "client_credentials";
 
    }
}
