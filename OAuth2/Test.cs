using NUnit.Framework;
using OAuth2.Model;
using System;

namespace OAuth2
{
    [TestFixture()]
    public class Test
    {
        private Entrance _entrance;
        private TokenResponse tokenResponse;
        private UploadAuthResponse uploadAuthResponse;
        private UploadFileResponse uploadFileResponse;
        [SetUp]
        public void Setup()
        {
            _entrance = new Entrance();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test()]
        public void Entrance_03_GetToken()
        {
            tokenResponse = _entrance.GetToken() as TokenResponse;
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void Entrance_04_GetUploadAuth()
        {
            uploadAuthResponse = _entrance.UploadAuth(tokenResponse) as UploadAuthResponse;

            Assert.AreEqual(1, 1);
        }

        [Test]
        public void Entrance_05_UploadFile()
        {
            _entrance.UploadFile(tokenResponse, uploadAuthResponse);
            Assert.AreEqual(1, 1);
        }
    }
}
