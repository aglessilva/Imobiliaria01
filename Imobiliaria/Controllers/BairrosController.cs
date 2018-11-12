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
    public class BairrosController : Controller
    {
        private DBConn db = new DBConn();

        // GET: Bairros
        public ActionResult Index(string filtro, int _page = 1)
        {
             
            List<Bairro> _bairros = null;

            if (string.IsNullOrWhiteSpace(filtro))
                _bairros = db.Bairros.OrderBy(x => x.Nome).ToList();
            else
            {
                _bairros = db.Bairros.Where(x =>  x.Nome.ToUpper().Contains(filtro.ToUpper())).ToList();
            }

            List<Cidade> listCidade = db.Cidades.OrderBy(x => x.Nome).ToList();

            _bairros.ToList().ForEach(item => {
                item.Composto = new CompostoEndereco();
                item.Composto.TextBairro = listCidade.Find(x => x.Id == item.CidadeId).Nome;
            });

            IPagedList<Bairro> listaBairros = new PagedList<Bairro>(_bairros.AsEnumerable(), _page, 20);
            return View(listaBairros);
        }

        // GET: Bairros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bairro bairro = db.Bairros.Find(id);
            if (bairro == null)
            {
                return HttpNotFound();
            }
            return View(bairro);
        }

        // GET: Bairros/Create
        public ActionResult Create(string id)
        {
            ViewBag.lstCidade = new Bairro().ListItemCidades(db.Cidades.OrderBy(x => x.Nome).ToList());

            if (!string.IsNullOrWhiteSpace(id))
            {
                int _id = Convert.ToInt32(id);
                Bairro bairro = db.Bairros.SingleOrDefault(x => x.Id == _id);

                if (bairro == null)
                {
                    return HttpNotFound();
                }
                return View(bairro);
            }

            return View();
        }

        // POST: Bairros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                if (bairro.Id > 0)
                {
                    db.Entry(bairro).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                db.Bairros.Add(bairro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bairro);
        }

        // GET: Bairros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bairro bairro = db.Bairros.Find(id);
            if (bairro == null)
            {
                return HttpNotFound();
            }
            return View(bairro);
        }

        // POST: Bairros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Ativo,CidadeId")] Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bairro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bairro);
        }


        // POST: Bairros/Delete/5
        [HttpPost]
        public JsonResult Excluir(int id)
        {
            Bairro bairro = db.Bairros.Find(id);
            var item = db.Bairros.Remove(bairro);
            db.SaveChanges();
            return Json(new { item }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
