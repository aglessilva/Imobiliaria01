using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Imobiliaria.Models;
using PagedList;

namespace Imobiliaria.Controllers
{
    public class AnunciosController : Controller
    {
        private DBConn db = new DBConn();

        // GET: Anuncios
        public ActionResult Index(string filtro, int? _page)
        {
            int page = _page ?? 1;

            List<Anuncio> _anuncios = null;
            if (!string.IsNullOrWhiteSpace(filtro))
                _anuncios = db.Anuncios.Where(p => p.Id > 1 && p.Titulo.ToUpper().Contains(filtro.ToUpper())).ToList();
            else
                _anuncios = db.Anuncios.ToList();
            IPagedList<Anuncio> lstAnuncio = new PagedList<Anuncio>(_anuncios.AsEnumerable(), page, 20);
            return View(lstAnuncio);
        }

        // GET: Anuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // GET: Anuncios/Create
        public ActionResult Create()
        {
            ViewBag.TipoImovel = ListTipoImovel() ;
            ViewBag.TipoAnuncio = ListNegocio();

            return View();
        }

        // POST: Anuncios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                anuncio.DataAnuncio = DateTime.Now;
                db.Anuncios.Add(anuncio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoImovel = ListTipoImovel();
            ViewBag.TipoAnuncio = ListNegocio();
            return View(anuncio);
        }

        // GET: Anuncios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.TipoImovel = ListTipoImovel();

            List<SelectListItem> lstNegocio = ListNegocio();

            lstNegocio.ForEach(x => {
                if (x.Value == anuncio.TipoAnuncio.ToString())
                    x.Selected = true;
            });
            ViewBag.TipoNegocio = lstNegocio;

            return View(anuncio);
        }

        // POST: Anuncios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                anuncio.DataAnuncio = DateTime.Now;
                db.Entry(anuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anuncio);
        }

        // GET: Anuncios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // POST: Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anuncio anuncio = db.Anuncios.Find(id);
            db.Anuncios.Remove(anuncio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [NonAction]
        protected List<SelectListItem> ListNegocio()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "Alguel" },
                new SelectListItem() { Value = "2", Text = "Venda" },
                new SelectListItem() { Value = "3", Text = "Alguel/Venda" },
            };
        }

        [NonAction]
        private List<SelectListItem> ListTipoImovel()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
                db.TipoImoveis.ToList().ForEach(item =>  {
                    lst.Add(new  SelectListItem() { Value = item.Id.ToString(), Text = item.Nome });
            });

            return lst.OrderBy(x => x.Text).ToList();
        }
    }
}
