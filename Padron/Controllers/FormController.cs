using Padron.Models;
using Padron.Models.PadronEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padron.Controllers
{
    public class FormController : Controller
    {

        private ApplicationDbContext _context = new ApplicationDbContext();
        private PadronDbcontext _padronContext = new PadronDbcontext();
        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        // GET: Form/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Register(Guid id)
        {
            Session["UserFormId"] =  ViewBag.UserFormId = id;

            if (!_context.Users.Any(x => x.UserCode == id))
                throw new HttpException(404, "File Not Found");
            var model = new ContactForm();
            model.CascadingModel.Provincias = _context.Provincias.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ModelState.Clear();

            return View(model);
        }

        [HttpGet]
        public ActionResult GetMunicipios(int provinciaId)
        {

            IEnumerable<SelectListItem> regions = _context.Municipios.Where(x => x.ProvinciaId == provinciaId)
            .Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            return Json(regions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetByCedula(string cedula)
        {
            cedula = cedula.Replace("-", "").Trim();
            var persona = _padronContext.Personas.FirstOrDefault(x => x.Cedula == cedula);
            return Json(persona  ?? new Persona(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Success(Guid id)
        {
            Session["UserFormId"] = ViewBag.UserFormId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Cedula,TelefonoMovil,FullName,ProvinciaId,MunicipioId,Sector,Email,IsInstagram,IsFacebook,IsTwitter,IsOther,ColaboradorDigitalRedes,Comentario,CoordinadorGuid,CascadingModel")] ContactForm model)
        {
            model.CascadingModel.Provincias = _context.Provincias.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            ViewBag.IsSuccess = false;
            model.ConcatRedes();
            //var sd = Guid.Parse(Session["UserFormId"].ToString());
            model.CoordinadorGuid = (model.CoordinadorGuid == Guid.Empty || model.CoordinadorGuid == null ? Guid.Parse(Session["UserFormId"].ToString()) : model.CoordinadorGuid);

            ViewBag.UserFormId = model.CoordinadorGuid;
            var coordinador  = _context.Users.FirstOrDefault(x => x.UserCode == (model.CoordinadorGuid));
            if (coordinador == null)
            {
                ViewBag.Message = "Se debe especificar un coordinador";

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Message = String.Join(",",ModelState.Values.SelectMany(v => v.Errors).Select(x=>x.ErrorMessage));

                return View(model);
            }

            model.CoordinadorId = coordinador.Id;


            _context.ContactForms.Add(model);
            _context.SaveChanges();
            ViewBag.IsSuccess = true;
            ViewBag.Message = "Registrado correctamente!";
            //model = new ContactForm();
            ModelState.Clear();

            return RedirectToAction("Success");
        }

            // GET: Form/Create
            public ActionResult Create()
        {
            return View();
        }

        // POST: Form/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Form/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: Form/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Form/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
