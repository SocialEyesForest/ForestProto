using System.Text;
using System.Web.Mvc;

namespace SocialEyesForest.Controllers
{
    public class ControllerJson : Controller
    {
        protected internal virtual JsonResult Json(object data, JsonRequestBehavior behavior, int maxJsonLength)
        {
            return Json(data, null, null, behavior, maxJsonLength);
        }
        protected internal virtual JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior, int maxJsonLength)
        {
            return new JsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = maxJsonLength
            };
        }
    }
}