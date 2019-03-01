using System;
namespace OAuth2.Model
{
    public class UploadAuthResponse : Response
    {
        public UploadAuthResponse()
        {
        }

        public class Credentials
        {
            public string AccessKeyId { get; set; }
            public DateTime Expiration { get; set; }
            public string SecretAccessKey { get; set; }
            public string SessionToken { get; set; }
        }

        public string BucketName { get; set; }
        public Credentials credentials { get; set; }
        public string KeyPrefix { get; set; }

    }
}
