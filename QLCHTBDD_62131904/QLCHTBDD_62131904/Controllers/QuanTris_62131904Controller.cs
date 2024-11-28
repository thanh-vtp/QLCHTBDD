using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers
{
    public class QuanTris_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        public bool CheckUser(string username, string password)
        {
            var kq = db.QuanTris.Where(x => x.Email == username && x.Password == password).ToList();
            //string hoTen = kq.First().HoTen;
            if (kq.Count() > 0)
            {
                Session["HoTen"] = kq.First().HoTen;
                return true;
            }
            else
            {
                Session["HoTen"] = null;
                return false;
            }
        }
        public ActionResult DangNhap_62131904()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap_62131904(QuanTri qt)
        {
            if (ModelState.IsValid)
            {
                if (CheckUser(qt.Email, qt.Password))
                {
                    FormsAuthentication.SetAuthCookie(qt.Email, true);
                    return RedirectToAction("Index", "NhanViens_62131904");
                }
                else
                    ModelState.AddModelError("", "Tên đăng nhập hoặc tài khoản không đúng.");
            }
            return View(qt);
        }

        // GET: QuanTris_62131904
        public ActionResult Index()
        {
            return View(db.QuanTris.ToList());
        }

        // GET: QuanTris_62131904/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // GET: QuanTris_62131904/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanTris_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,Admin,HoTen,Password")] QuanTri quanTri)
        {
            if (ModelState.IsValid)
            {
                db.QuanTris.Add(quanTri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quanTri);
        }

        // GET: QuanTris_62131904/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // POST: QuanTris_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,Admin,HoTen,Password")] QuanTri quanTri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanTri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quanTri);
        }

        // GET: QuanTris_62131904/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // POST: QuanTris_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuanTri quanTri = db.QuanTris.Find(id);
            db.QuanTris.Remove(quanTri);
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
