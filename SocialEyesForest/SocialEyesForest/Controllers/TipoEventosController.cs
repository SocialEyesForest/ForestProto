using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using SocialEyesForest.Models;

namespace SocialEyesForest.Controllers
{
    public class TipoEventosController : ApiController
    {
        private GeoContext db = new GeoContext();

        // GET: api/TipoEventos
        public IQueryable<TipoEvento> GetTipoEventos()
        {
            return db.TipoEventos;
        }

        // GET: api/TipoEventos/5
        [ResponseType(typeof(TipoEvento))]
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