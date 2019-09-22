using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Padron.Models;
using PagedList;

namespace Padron.Controllers
{
    [Authorize(Roles ="Administrador")]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index(string sortOrder, string CurrentSort, int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;
            IPagedList<User> users = null;

            var rol = db.Roles.FirstOrDefault(x => x.Name == "Administrador");
            var userList = db.Users
                .Include(x => x.Rol)
                .Include(x => x.Roles)
                .Where(x => x.Roles.Any(y => y.RoleId != rol.Id));
            switch (sortOrder)
            {
                case "Name":
                    if (sortOrder.Equals(CurrentSort))
                        users = userList.OrderByDescending
                                (m => m.Nombres).ToPagedList(pageIndex, pageSize);
                    else
                        users = userList.OrderBy
                                (m => m.Nombres).ToPagedList(pageIndex, pageSize);
                    break;
                case "Cedula":
                    if (sortOrder.Equals(CurrentSort))
                        users = userList.OrderByDescending
                                (m => m.Cedula).ToPagedList(pageIndex, pageSize);
                    else
                        users = userList.OrderBy
                                (m => m.Cedula).ToPagedList(pageIndex, pageSize);
                    break;
                case "Telefono":
                    if (sortOrder.Equals(CurrentSort))
                        users = userList.OrderByDescending
                                (m => m.Telefono).ToPagedList(pageIndex, pageSize);
                    else
                        users = userList.OrderBy
                                (m => m.Telefono).ToPagedList(pageIndex, pageSize);
                    break;
                case "Celular":
                    if (sortOrder.Equals(CurrentSort))
                        users = userList.OrderByDescending
                                (m => m.Celular).ToPagedList(pageIndex, pageSize);
                    else
                        users = db.Users.OrderBy
                                (m => m.Celular).ToPagedList(pageIndex, pageSize);
                    break;
                case "Activo":
                    if (sortOrder.Equals(CurrentSort))
                        users = userList.OrderByDescending
                                (m => m.Activo).ToPagedList(pageIndex, pageSize);
                    else
                        users = userList.OrderBy
                                (m => m.Activo).ToPagedList(pageIndex, pageSize);
                    break;
                case "Default":
                    users = userList.OrderBy
                        (m => m.Nombres).ToPagedList(pageIndex, pageSize);
                    break;
            }

            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserCode,Nombres,Apellidos,Cedula,Telefono,Celular,Activo,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserCode,Nombres,Apellidos,Cedula,Telefono,Celular,Activo,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpGet]
        public bool ActiveUser(int Id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == Id);
            user.Activo = true;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
        [HttpGet]
        public bool DesactiveUser(int Id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == Id);
            user.Activo = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
    }
}
