using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SocialEyesForest
{
    public class JsonNetResult : ActionResult
    {
        public JsonNetResult()
        {

        }

        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";
            if (Data != null)
            {
                response.Write(JsonConvert.SerializeObject(Data, Formatting.Indented,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            }

        }
    }
}