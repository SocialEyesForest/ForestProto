using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SocialEyesForest.Models
{
    /*
     * http://telldontask.wordpress.com/2010/05/26/posting-an-array-of-complex-types-using-jquery-json-to-asp-net-mvc/
     */
    public class JsonModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!IsJSONRequest(controllerContext))
            {
                return base.BindModel(controllerContext, bindingContext);
            }

            // get the JSON data that's been posted
            var request = controllerContext.HttpContext.Request;
            StreamReader reader = new StreamReader(request.InputStream);
            // fix from comments
            request.InputStream.Seek(0, 0);
            var jsonStringData = reader.ReadToEnd();

            return new JavaScriptSerializer()
                .Deserialize(jsonStringData, bindingContext.ModelMetadata.ModelType);
        }

        static bool IsJSONRequest(ControllerContext controllerContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            return contentType.Contains("application/json");
        }
    }
}
