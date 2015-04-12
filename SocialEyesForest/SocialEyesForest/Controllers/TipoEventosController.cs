using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using SocialEyesForest.Common;
using SocialEyesForest.Models;
using Newtonsoft.Json;

namespace SocialEyesForest.Controllers
{
    public class TipoEventosController : ApiController
    {
        private GeoContext db = new GeoContext();

        // GET: api/TipoEventos
        [System.Web.Mvc.Route("api/TipoEventos")]
        [System.Web.Mvc.HttpGet]
        public JsonNetResult GetTipoEventos()
        {
            var result = db.TipoEventos.Select(t => new {t.Id, t.Nombre}).ToList();
            return new JsonNetResult { Data = result, Formatting = Formatting.None };
        }

        // GET: api/TipoEventos/5
        [System.Web.Mvc.Route("api/TipoEvento/{id}")]
        [System.Web.Mvc.HttpGet]
        //[ResponseType(typeof(TipoEvento))]
        public IHttpActionResult GetTipoEvento(int id)
        {
            TipoEvento tipoEvento = db.TipoEventos.Find(id);
            if (tipoEvento == null)
            {
                return NotFound();
            }

            return Ok(tipoEvento);
        }
        /*
        // PUT: api/TipoEventos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoEvento(int id, TipoEvento tipoEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoEvento.Id)
            {
                return BadRequest();
            }

            db.Entry(tipoEvento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoEventoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TipoEventos
        [ResponseType(typeof(TipoEvento))]
        public IHttpActionResult PostTipoEvento(TipoEvento tipoEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoEventos.Add(tipoEvento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoEvento.Id }, tipoEvento);
        }

        // DELETE: api/TipoEventos/5
        [ResponseType(typeof(TipoEvento))]
        public IHttpActionResult DeleteTipoEvento(int id)
        {
            TipoEvento tipoEvento = db.TipoEventos.Find(id);
            if (tipoEvento == null)
            {
                return NotFound();
            }

            db.TipoEventos.Remove(tipoEvento);
            db.SaveChanges();

            return Ok(tipoEvento);
        }
        */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoEventoExists(int id)
        {
            return db.TipoEventos.Count(e => e.Id == id) > 0;
        }
    }
}