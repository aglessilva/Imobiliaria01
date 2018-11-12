using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Imobiliaria.Models;
using System.IO;
using System.Threading;

namespace Imobiliaria.Controllers
{
    public class ImagensAnunciosController : Controller
    {
        private DBConn db = new DBConn();

        // GET: ImagensAnuncios
        public ActionResult Index()
        {
            var _anuncio = db.Anuncios.Include(i => i.Imagens);
            return View(_anuncio.ToList());
        }

        // GET: ImagensAnuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagensAnuncio imagensAnuncio = db.Imagens.Find(id);
            if (imagensAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(imagensAnuncio);
        }

        // GET: ImagensAnuncios/Create
        public ActionResult Create(string id = null)
        {
            ViewBag.lstAnuncios = ListItensAnuncio(id);

            ImagensAnuncio _imagemAnuncio = null;
            if (!string.IsNullOrWhiteSpace(id))
                _imagemAnuncio = db.Imagens.FirstOrDefault(x => x.AnuncioId.ToString().Equals(id));

            return View(_imagemAnuncio);
        }

        // POST: ImagensAnuncios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImagensAnuncio imagensAnuncio)
        {
            if (ModelState.IsValid)
            {
                db.Imagens.Add(imagensAnuncio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnuncioId = new SelectList(db.Anuncios, "Id", "TipoAnuncio", imagensAnuncio.AnuncioId);
            return View(imagensAnuncio);
        }

        // GET: ImagensAnuncios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagensAnuncio imagensAnuncio = db.Imagens.Find(id);
            if (imagensAnuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.lstAnuncios = ListItensAnuncio();
            return View(imagensAnuncio);
        }

        // POST: ImagensAnuncios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(ImagensAnuncio imagensAnuncio)
        {
            var itemImg = db.Imagens.Find(imagensAnuncio.Id);
            itemImg.Descricao = imagensAnuncio.Descricao;
            itemImg.Ativo = imagensAnuncio.Ativo;
            itemImg.AnuncioId = imagensAnuncio.AnuncioId;

            db.Entry(itemImg).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { imagensAnuncio }, JsonRequestBehavior.AllowGet);
        }

        // GET: ImagensAnuncios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagensAnuncio imagensAnuncio = db.Imagens.Find(id);
            if (imagensAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(imagensAnuncio);
        }

        // POST: ImagensAnuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImagensAnuncio imagensAnuncio = db.Imagens.Find(id);
            db.Imagens.Remove(imagensAnuncio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult DeleteAll(List<ItemId> _itemId)
        {
            List<int> item = new List<int>();
            _itemId.ForEach(j => { item.Add(Convert.ToInt32(j.Id)); });

            List<ImagensAnuncio> imagensAnuncio = db.Imagens.Where(x => item.Contains(x.Id)).ToList();
            db.Imagens.RemoveRange(imagensAnuncio);
            int ret = db.SaveChanges();

            if (ret > 0)
                return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "ok"  }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private List<SelectListItem> ListItensAnuncio(string id = null)
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            var lst = db.Anuncios.ToList();
            lst.ForEach(item => {
                if (string.IsNullOrWhiteSpace(id))
                    lstSelectItem.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Titulo });
                else
                {
                    if(id == item.Id.ToString())
                        lstSelectItem.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Titulo });
                }
            });

            if (lstSelectItem.Count == 0)
                return new List<SelectListItem>();
            return lstSelectItem;
        }


        [HttpPost]
        public JsonResult UploadFiles()
        {
            var test = db.Anuncios.FirstOrDefault(x => x.Id > 0);

            if (test == null)
            {
                return  null;
            }

            if (test.Enderecos.Count == 0)
            {
                return null;
            }

            HttpFileCollectionBase files = Request.Files;
            ImagensAnuncio img = new ImagensAnuncio();

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
               
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    img.Path = ms.GetBuffer();
                }

                img.Descricao = "-------------";
                img.AnuncioId = db.Anuncios.FirstOrDefault(x => x.Id > 0).Id;
                img = db.Imagens.Add(img);
                db.SaveChanges();
                db.Dispose();

            }
            JsonResult jsonResult = Json(new {id=img.Id}, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public class ItemId {
            public string Id { get; set; }
        }
    }
}
