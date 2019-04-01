using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NomenProgram.Models;

namespace NomenProgram.Controllers
{
    public class NomDatasController : Controller
    {
        private NomSPEntities db = new NomSPEntities();

        // GET: NomDatas
        public ActionResult Index()
        {
            var nomData = db.NomData.Include(n => n.nomcount_types);
            return View(nomData.ToList());
        }

        // GET: NomDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomData nomData = db.NomData.Find(id);
            if (nomData == null)
            {
                return HttpNotFound();
            }
            return View(nomData);
        }

        // GET: NomDatas/Create
        public ActionResult Create()
        {
            ViewBag.CNT_TYPE = new SelectList(db.nomcount_types, "ID", "TYPE");
            return View();
        }

        // POST: NomDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SNAME,FNAME,ITEM_CNT,CNT_TYPE")] NomData nomData)
        {
            if (ModelState.IsValid)
            {
                db.NomData.Add(nomData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CNT_TYPE = new SelectList(db.nomcount_types, "ID", "TYPE", nomData.CNT_TYPE);
            return View(nomData);
        }

        // GET: NomDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomData nomData = db.NomData.Find(id);
            if (nomData == null)
            {
                return HttpNotFound();
            }
            ViewBag.CNT_TYPE = new SelectList(db.nomcount_types, "ID", "TYPE", nomData.CNT_TYPE);
            return View(nomData);
        }

        // POST: NomDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SNAME,FNAME,ITEM_CNT,CNT_TYPE")] NomData nomData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CNT_TYPE = new SelectList(db.nomcount_types, "ID", "TYPE", nomData.CNT_TYPE);
            return View(nomData);
        }

        // GET: NomDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomData nomData = db.NomData.Find(id);
            if (nomData == null)
            {
                return HttpNotFound();
            }
            return View(nomData);
        }

        // POST: NomDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NomData nomData = db.NomData.Find(id);
            db.NomData.Remove(nomData);
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
