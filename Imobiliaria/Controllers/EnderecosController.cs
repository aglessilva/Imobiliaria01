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
    public class EnderecosController : Controller
    {
        private DBConn db = new DBConn();

        // GET: Endereco
        public ActionResult Index(string filtro, int? _page = null )
        {
            int p = _page ?? 1;
            //db.Enderecos.Include(e => e.AnuncioEndereco);
            List<Endereco> enderecos = new List<Endereco>();
            List<Estado> lstEstado = db.Estados.ToList();
            List<Cidade> lstCidade = db.Cidades.ToList();
            List<Bairro> lstBairro = db.Bairros.ToList();

            if (!string.IsNullOrWhiteSpace(filtro))
                enderecos = db.Enderecos.Where(x => x.Cep.ToUpper().Contains(filtro.ToUpper()) ||
                                            x.Logradouro.ToUpper().Contains(filtro.ToUpper())).ToList();
            else
            {
               enderecos = db.Enderecos.ToList();
            }

            enderecos.ToList().ForEach(item => {
                item.Composto = new CompostoEndereco();
                item.Composto.TextEstado = lstEstado.Find(x => x.Id == item.EstadoId).Nome;
                item.Composto.TextCidade = lstCidade.Find(x => x.Id == item.CidadeId).Nome;
                item.Composto.TextBairro = lstBairro.Find(x => x.Id == item.BairroId).Nome;
            });

            ViewBag.CountAnuncio = db.Anuncios.Count();
            IPagedList<Endereco> lstEndereco = new PagedList<Endereco>(enderecos.AsEnumerable(), p, 20);
            return View("Index",lstEndereco);
        }

        // GET: Endereco/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Endereco endereco = db.Enderecos.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }

            endereco.Composto = new CompostoEndereco();
            endereco.Composto.TextBairro = db.Bairros.Find(endereco.BairroId).Nome;
            endereco.Composto.TextCidade = db.Cidades.Find(endereco.CidadeId).Nome;
            endereco.Composto.TextEstado = db.Bairros.Find(endereco.EstadoId).Nome;

            return View(endereco);
        }

        // GET: Endereco/Create
        public ActionResult Create(string id)
        {
            ViewBag.lstAnuncio = string.IsNullOrEmpty(id) ? ListItensAnuncio() : ListItensAnuncio().Where(x => x.Value.Equals(id)) ;
            ViewBag.lstEstado = ListItemEstado();
            return View();
        }

        // POST: Endereco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Endereco endereco)
        {
            Cidade _cidade = null;
            if (!db.Cidades.Any(x => x.Nome.Contains(endereco.Composto.TextCidade)))
            {
                _cidade = new Cidade() { Nome = endereco.Composto.TextCidade, Ativo = true,  EstadoId = endereco.EstadoId };
                _cidade = db.Cidades.Add(_cidade);
                db.SaveChanges();
            }
            else
            {
                _cidade = db.Cidades.SingleOrDefault(x => x.Nome.Contains(endereco.Composto.TextCidade));
            }

            endereco.CidadeId = _cidade.Id;

            Bairro _bairro = null;
           if (!db.Bairros.Any(x => x.Nome.Contains(endereco.Composto.TextBairro)))
            {
              _bairro = new Bairro() { Nome = endereco.Composto.TextBairro, Ativo = true, CidadeId = endereco.CidadeId };
                _bairro = db.Bairros.Add(_bairro);
                db.SaveChanges();
            }
           else
            {
                _bairro = db.Bairros.SingleOrDefault(x => x.Nome.Contains(endereco.Composto.TextBairro));
            }

            endereco.BairroId = _bairro.Id;

            if (ModelState.IsValid)
            {
                db.Enderecos.Add(endereco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnundioId = new SelectList(db.Anuncios, "Id", "TipoAnuncio", endereco.AnundioId);
            return View(endereco);
        }

        // GET: Endereco/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.lstEstado = ListItemEstado();
            ViewBag.lstBairro = ListItemBairros();
            ViewBag.lstCidade = ListItemCidades();

            Endereco endereco = db.Enderecos.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }


        // POST: Endereco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endereco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnundioId = new SelectList(db.Anuncios, "Id", "TipoAnuncio", endereco.AnundioId);
            return View(endereco);
        }

        // GET: Endereco/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.Enderecos.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            endereco.Composto = new CompostoEndereco();
            endereco.Composto.TextBairro = db.Bairros.Find(endereco.BairroId).Nome;
            endereco.Composto.TextCidade = db.Cidades.Find(endereco.CidadeId).Nome;
            endereco.Composto.TextEstado = db.Bairros.Find(endereco.EstadoId).Nome;
            return View(endereco);
        }

        // POST: Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Endereco endereco = db.Enderecos.Find(id);
            db.Enderecos.Remove(endereco);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [NonAction]
        private List<SelectListItem> ListItensAnuncio()
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            var lst = db.Anuncios.ToList();
            lst.ForEach(item => {
                lstSelectItem.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Titulo });
            });

            if (lstSelectItem.Count == 0)
                return new List<SelectListItem>();
            return lstSelectItem;
        }


        [NonAction]
        private List<SelectListItem> ListItemBairros()
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            new Bairro().FindAll().ForEach(item => {
                lstSelectItem.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Nome });
            });

            if (lstSelectItem.Count == 0)
                return new List<SelectListItem>();

            return lstSelectItem;
        }


        [NonAction]
        private List<SelectListItem> ListItemCidades()
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            new Cidade().FindAll().ForEach(item => {
                lstSelectItem.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Nome });
            });

            if (lstSelectItem.Count == 0)
                return new List<SelectListItem>();

            return lstSelectItem;
        }


        [NonAction]
        private List<SelectListItem> ListItemEstado()
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            new Estado().FindAll().ForEach(item => {
                lstSelectItem.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Sigla });
            });

            if (lstSelectItem.Count == 0)
                return new List<SelectListItem>();

            return lstSelectItem;
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
