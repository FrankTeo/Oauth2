﻿using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using OAuth2.Model;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.IO;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Runtime;

namespace OAuth2
{
    public class Entrance
    {
        private static JavaScriptSerializer _json;
        public Entrance()
        {
            _json = new JavaScriptSerializer();
        }

        #region GetToken
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>GetToken</returns>
        public IResponse GetToken()
        {
            var tr = new TokenRequest();
            var req = (HttpWebRequest)WebRequest.Create(tr.Address);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tr.client_id + ":" + tr.client_secret));
            req.Headers.Add("Authorization", "Basic " + encoded);

            byte[] data = Encoding.UTF8.GetBytes(tr.GetDefaultContent());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            HttpWebResponse resp = null;
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return ResponseMaker<TokenResponse>(resp);
        }
        #endregion

        #region UploadAuth
        /// <summary>
        /// invoke https://api.dev.az.nz/v1/upload
        /// </summary>
        /// <returns>UploadAuthResponse instance</returns>
        /// <param name="tokenResponse">TokenResponse.</param>
        public IResponse UploadAuth(TokenResponse tokenResponse)
        {
            var ur = new UploadAuthRequest();
            var req = (HttpWebRequest)WebRequest.Create(ur.Address);
            req.Headers.Add("Authorization", $"{tokenResponse.token_type} {tokenResponse.access_token}");
            HttpWebResponse resp = null;

            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return ResponseMaker<UploadAuthResponse>(resp);
        }
        #endregion

        #region UploadFile
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <returns>null</returns>
        /// <param name="tokenResponse">Token response.</param>
        /// <param name="uploadAuthResponse">Upload auth response.</param>
        public void UploadFile(TokenResponse tokenResponse, UploadAuthResponse uploadAuthResponse)
        {
            var tr = new TokenRequest();
            var FileName = "gofishing.txt";
            var PathName = Environment.CurrentDirectory + "/../../";

            try
            {
                AmazonS3Config config = new AmazonS3Config
                {
                    RegionEndpoint = Amazon.RegionEndpoint.APSoutheast2,
                    UseHttp = false
                };
                var s3Client = new AmazonS3Client(
                    uploadAuthResponse.credentials.AccessKeyId, 
                    uploadAuthResponse.credentials.SecretAccessKey, 
                    uploadAuthResponse.credentials.SessionToken,
                    config
                    );
                var transferUtility = new TransferUtility(s3Client);
                var request = new TransferUtilityUploadRequest
                {
                    BucketName = uploadAuthResponse.BucketName,
                    Key = uploadAuthResponse.KeyPrefix + FileName,
                    FilePath = PathName + FileName
                };
 
                transferUtility.Upload(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        /// <summary>
        /// Responses the maker.
        /// </summary>
        /// <returns>The maker.</returns>
        /// <param name="response">Response.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private static T ResponseMaker<T>(HttpWebResponse response) where T : Response, new()
        {
            T t = new T
            {
                ResponseCode = response.StatusCode
            };
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var ret = reader.ReadToEnd();
                t = _json.Deserialize<T>(ret);
                return t;
            }

            t.ErrorMessage = reader.ReadToEnd();
            return t;
        }
    }
}
