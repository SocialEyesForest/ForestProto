using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SocialEyesForest.Common;
using SocialEyesForest.Models;

namespace SocialEyesForest.Controllers
{
    public class EventosController : Controller
    {
        public const int SRID = 4326;
        private GeoContext db = new GeoContext();

        // GET: api/Eventos
        public ActionResult Listar()
        {
            var result = db.Eventos.Select(t => new EventoDto
            {
                Id = t.Id,
                FechaEvento = t.FechaEvento,
                Lat = t.Localizacion.Latitude??0,
                Lng = t.Localizacion.Longitude??0,
                Ubicacion = t.Ubicacion,
                IdTipoEvento = t.IdTipoEvento,
                IdMotivo = t.IdMotivo,
                SubMotivo = t.SubMotivo,
                Observaciones = t.Observaciones
            }).ToList();
            
            return new JsonNetResult { Data = result, Formatting = Formatting.None };
        }

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
                catch (DbUpdateConcurrencyException)  // TODO: Manejar el error
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

        public ActionResult Post(EventoDto eventoDto)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return null; // TODO: Manejar el error
            }
            var evento = new Evento
            {
                Id = eventoDto.Id,
                FechaEvento = DateTime.Now, //eventoDto.FechaEvento,
                Localizacion = DbGeography.FromText(string.Format("POINT({0} {1})", eventoDto.Lng, eventoDto.Lat), SRID),
                Ubicacion = eventoDto.Ubicacion,
                IdTipoEvento = eventoDto.IdTipoEvento,
                IdMotivo = eventoDto.IdMotivo,
                SubMotivo = eventoDto.SubMotivo,
                Observaciones = eventoDto.Observaciones
            };
            db.Eventos.Add(evento);
            db.SaveChanges();
            return new JsonNetResult { Data = evento, Formatting = Formatting.None };
        }

        public ActionResult PostMedia(int id, HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file == null || file.ContentLength <= 0) return null; // TODO: Manejar el error

            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid() + extension;

            var evento = db.Eventos.Find(id);
            if (evento == null) return null; // TODO: manejar error
            var media = db.Media.Create();
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

            var path = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
            file.SaveAs(path);
            return new JsonNetResult {Data = media, Formatting = Formatting.None};
        }

        [HttpPost]
        public async Task<JsonResult> PostImage(int id)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    Media media;
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        var extension = Path.GetExtension(fileContent.FileName);
                        var fileName = Guid.NewGuid().ToString() + extension;

                        var evento = db.Eventos.Find(id);
                        if (evento == null) return null; // TODO: manejar error
                        media = new Media {IdEvento = id, NombreArchivo = fileName, TipoMedia = 1};
                        db.Media.Add(media);

                        //var fileName = Path.GetFileName(fileContent);
                        var path = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Proceso fallido");
            }

            return Json("Archivo guardado satisfactoriamente");
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