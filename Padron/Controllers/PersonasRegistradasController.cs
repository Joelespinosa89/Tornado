﻿using Padron.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Padron.Helpers;
using PagedList;

namespace Padron.Controllers
{
    [Authorize]
    public class PersonasRegistradasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonasRegistradas
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string sortOrder, string CurrentSort, int? page)
        {

            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "FullName" : sortOrder;
            IPagedList<ContactForm> contactForms = null;

            var forms = db.ContactForms.Include(u => u.Coordinador).Include(u=>u.Provincia);

            switch (sortOrder)
            {
                case "Cedula":
                    if (sortOrder.Equals(CurrentSort))
                        contactForms = forms.OrderByDescending
                                (m => m.Cedula).ToPagedList(pageIndex, pageSize);
                    else
                        contactForms = forms.OrderBy
                                (m => m.Cedula).ToPagedList(pageIndex, pageSize);
                    break;
                case "FullName":
                    if (sortOrder.Equals(CurrentSort))
                        contactForms = forms.OrderByDescending
                                (m => m.FullName).ToPagedList(pageIndex, pageSize);
                    else
                        contactForms = forms.OrderBy
                                (m => m.FullName).ToPagedList(pageIndex, pageSize);
                    break;
                case "TelefonoMovil":
                    if (sortOrder.Equals(CurrentSort))
                        contactForms = forms.OrderByDescending
                                (m => m.TelefonoMovil).ToPagedList(pageIndex, pageSize);
                    else
                        contactForms = forms.OrderBy
                                (m => m.TelefonoMovil).ToPagedList(pageIndex, pageSize);
                    break;
                case "Default":
                    contactForms = forms.OrderBy
                        (m => m.FullName).ToPagedList(pageIndex, pageSize);
                    break;
            }
            return View(contactForms);
        }
        //[Authorize(Roles = "Director,Cordinador,Sub-Cordinador,Multiplicador")]

        public ActionResult MisPersonas(string sortOrder, string CurrentSort, int? page)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = int.Parse(claimsIdentity.Claims
                       .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "FullName" : sortOrder;
            IPagedList<ContactForm> contactForms = null;

            // var forms = db.ContactForms.Include(u => u.Coordinador).Include(u => u.Provincia).Where(x => x.CoordinadorId == userIdClaim);
            var forms = db.ContactForms.Include(u => u.Coordinador).Include(u => u.Provincia);

            switch (sortOrder)
            {
                case "Cedula":
                    if (sortOrder.Equals(CurrentSort))
                        contactForms = forms.OrderByDescending
                                (m => m.Cedula).ToPagedList(pageIndex, pageSize);
                    else
                        contactForms = forms.OrderBy
                                (m => m.Cedula).ToPagedList(pageIndex, pageSize);
                    break;
                case "FullName":
                    if (sortOrder.Equals(CurrentSort))
                        contactForms = forms.OrderByDescending
                                (m => m.FullName).ToPagedList(pageIndex, pageSize);
                    else
                        contactForms = forms.OrderBy
                                (m => m.FullName).ToPagedList(pageIndex, pageSize);
                    break;
                case "TelefonoMovil":
                    if (sortOrder.Equals(CurrentSort))
                        contactForms = forms.OrderByDescending
                                (m => m.TelefonoMovil).ToPagedList(pageIndex, pageSize);
                    else
                        contactForms = forms.OrderBy
                                (m => m.TelefonoMovil).ToPagedList(pageIndex, pageSize);
                    break;
                case "Default":
                    contactForms = forms.OrderBy
                        (m => m.FullName).ToPagedList(pageIndex, pageSize);
                    break;
            }

            //ViewBag.UserURL = GetBaseUrl() + "Form/" + User.Identity.GetUserCode() + "/Register";
            ViewBag.UserURL = GetBaseUrl() + "Form/Register";

            return View(contactForms);
        }

        public string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

        public bool ValidateRegister(int Id)
        {
         
            try
            {
                var ContactForm = db.ContactForms.FirstOrDefault(x => x.Id == Id);
                ContactForm.Validado = true;
                db.Entry(ContactForm).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            return true;
        }
        
        public bool InValidateRegister(int Id)
        {
            var ContactForm = db.ContactForms.FirstOrDefault(x => x.Id == Id);
            ContactForm.Validado = false;
            db.Entry(ContactForm).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }

    }
}