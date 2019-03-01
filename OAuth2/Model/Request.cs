using System;
using System.Web;
using System.Web.Script.Serialization;

namespace OAuth2.Model
{
    public abstract class Request : IRequest
    {
        public string GetJsonRequestMessage()
        {
            var json = new JavaScriptSerializer();
            return json.Serialize(this);
        }
    }
}
