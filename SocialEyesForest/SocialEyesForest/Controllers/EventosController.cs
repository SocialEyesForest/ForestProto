using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SocialEyesForest.Common;
using SocialEyesForest.Models;

namespace SocialEyesForest.Controllers
{
    public class EventosController : Controller
    {
        private GeoContext db = new GeoContext();

        // GET: api/Eventos
        public ActionResult Listar()
        {
            var result = db.Eventos.ToList();
            return new JsonNetResult { Data = result, Formatting = Formatting.None };
        }

        // GET: api/Eventos/5
        public ActionResult GetEvento(int id)
        {
            var result = db.Eventos.Find(id);
            return new JsonNetResult { Data = result, Formatting = Formatting.None };
        }

        public ActionResult Grabar(int id, Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(id))
                    {
                        //return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new JsonNetResult {Data = evento, Formatting = Formatting.None};
            }
            return null;
        }

        public ActionResult PostEvento(Evento evento)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return null;
            }
            db.Eventos.Add(evento);
            db.SaveChanges();
            return new JsonNetResult { Data = evento, Formatting = Formatting.None };
        }

        public ActionResult PostMedia(int id, HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file == null || file.ContentLength <= 0) return null; // TODO: Manejar el error

            var extension = Path.GetExtension(file.FileName);
            var fileName = new Guid().ToString() + extension;

            var evento = db.Eventos.Find(id);
            if (evento == null) return null; // TODO: manejar error
            var media = db.Media.Create();
            media.Id = id;
            media.IdEvento = id;
            media.NombreArchivo = fileName;
            switch (extension)
            {
                case ".jpg":
                case ".png":
                case ".gif":
                case ".bmp":
                    media.TipoMedia = 1;
                    break;
                case ".avi":
                case ".mov":
                case ".3gp":
                    media.TipoMedia = 2;
                    break;
                default:
                    media.TipoMedia = 0;
                    break;
            }
            db.SaveChanges();

            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            file.SaveAs(path);
            return new JsonNetResult {Data = media, Formatting = Formatting.None};
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoExists(int id)
        {
            return db.Eventos.Count(e => e.Id == id) > 0;
        }
    }
}