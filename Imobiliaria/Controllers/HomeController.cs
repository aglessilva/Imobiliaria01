using Imobiliaria.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Imobiliaria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GetInitiaalize();
            IEnumerable<Anuncio> _anuncio = new Anuncio().FindAll();
            return View(_anuncio);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [NonAction]
        private void GetInitiaalize()
        {
            ViewBag.TipoImoveis = new TipoImovel().FindAll();
            ViewBag.EstadoBr = new Estado().FindAll();
        }


        [HttpGet]
        public PartialViewResult Detalhes(string id)
        {
            Anuncio _anuncio = new Anuncio().FindAll(id)[0];
            return PartialView("Detalhes", _anuncio);
        }

        [HttpGet]
        public PartialViewResult Lista(string id)
        {
            IPagedList<Anuncio> _anuncio = new PagedList<Anuncio>(new Anuncio().FindAll(id).AsEnumerable(), 1, 20);
            return PartialView("Lista",_anuncio);
        }

        [HttpGet]
        public JsonResult BindCidade(string id)
        {
            List<Cidade> t = new Cidade().FindAllById(Convert.ToInt32(id));
            return Json(new { t }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BindBairro(string id)
        {
            List<Bairro> bairro = new Bairro().FindAllById(Convert.ToInt32(id));
            return Json(new { bairro}, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public PartialViewResult Search(string _anuncioTipo, string _tipo, string _estado, string _bairro, string _cidade, string _valor, string _page)
        {
            var tipoAnuncio = Convert.ToInt32(_anuncioTipo);

            int[] parans = {
                Convert.ToInt32(_tipo), Convert.ToInt32(_estado), Convert.ToInt32(_bairro), Convert.ToInt32(_cidade),
                Convert.ToInt32(_valor.Split('-')[0]), Convert.ToInt32(_valor.Split('-')[1]), tipoAnuncio };

            IPagedList<Anuncio> _anuncio = new PagedList<Anuncio>(new Anuncio().Filter(parans).AsEnumerable(), Convert.ToInt32( _page), 20);
            return PartialView("Lista",_anuncio);
        }


        [HttpGet]
        public ActionResult EnviarContato()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EnviarContato(Email _objModelMail)
        {
            if (ModelState.IsValid)
            {
                //Instância classe email
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body + "<br><br><p>Telefone do Cliente"+ _objModelMail.Telefone + "</p>";
                mail.Body = Body;
                mail.IsBodyHtml = true;

                //Instância smtp do servidor, neste caso o gmail.
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("agles.developer@gmail.com", "290482hbt");// Login e senha do e-mail.
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return Json(new { _objModelMail }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return  Json(new { ModelState.IsValid }, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: Usuarios
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string login, string pwd)
        {
            DBConn db = new DBConn();
            Usuario ret = db.Usuarios.SingleOrDefault(x => x.UserName.Equals(login) && x.Ativo == true);
            if (ret == null)
            {
                db.Dispose();
                return Json(new { status = "NOK" }, JsonRequestBehavior.AllowGet);
            }

            if (Crypto.VerifyHashedPassword(ret.Senha, pwd) && ret.UserName.Equals(login))
            {
                db.Dispose();
                SessionContext.Login = ret.Nome;
                SessionContext.IsAutenticado = "true";
                return Json(new { ret }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Dispose();
                return Json(new { status = "NOK" }, JsonRequestBehavior.AllowGet);
            }
            

        }

        public JsonResult Logout()
        {
            SessionContext.Login = string.Empty;
            SessionContext.IsAutenticado = "false";
            return Json(new { status = "NOK" }, JsonRequestBehavior.AllowGet);
        }


    }
}