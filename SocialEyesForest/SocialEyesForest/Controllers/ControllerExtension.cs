using System.Web.Mvc;

namespace SocialEyesForest.Controllers
{
    public static class ControllerExtension
    {
        public static JsonResult Json(this Controller controller, object data, JsonRequestBehavior behavior, int maxJsonLength)
        {
            return new JsonResult
            {
                Data = data,
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = behavior,
                MaxJsonLength = maxJsonLength
            };
        }
    }
}