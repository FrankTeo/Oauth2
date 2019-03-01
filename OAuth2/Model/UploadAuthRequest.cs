using System;
namespace OAuth2.Model
{
    public class UploadAuthRequest : Request
    {
        public UploadAuthRequest()
        {
        }

        public string Address = "https://api.dev.az.nz/v1/upload";
    }
}
