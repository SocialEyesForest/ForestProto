﻿using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using SocialEyesForest.Common;
using SocialEyesForest.Models;

namespace SocialEyesForest.Controllers
{
    public class TipoEventosController : Controller
    {
        private GeoContext db = new GeoContext();

        public JsonNetResult Listar()
        {
            var result = db.TipoEventos.Select(t => new {k = t.Id, t = t.Nombre}).ToList();
            return new JsonNetResult { Data = result, Formatting = Formatting.None };
        }

        public ActionResult Get(int id)
        {
            var result = db.TipoEventos.Find(id);
            return new JsonNetResult { Data = result, Formatting = Formatting.None };
        }
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