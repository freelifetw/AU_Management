using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AU_Management.Models;
using System.IO;

namespace AU_Management.Controllers
{

    [Authorize]
    public class Paper_manageController : Controller
    {

        ManagementDBContext db = new ManagementDBContext();
        //
        // GET: /Paper_manage/
        public ActionResult Index(String searchTitle,String searchSource,String searchyear,String searchKey)
        {
            var result = (from m in db.Papers where m.Creater == User.Identity.Name select m).ToList();
            if(!String.IsNullOrEmpty(searchKey))
                result = result.Where(a => !String.IsNullOrEmpty(a.Key)).Where(b => b.Key.ToLower().Split(',').Any(p => p.Contains(searchKey.ToLower()))).ToList();
            if (!String.IsNullOrEmpty(searchSource))
                result = result.Where(a => a.Source.ToLower().Contains(searchSource.ToLower())).ToList();
            if (!String.IsNullOrEmpty(searchTitle))
                result = result.Where(a => a.Title.ToLower().Contains(searchTitle.ToLower())).ToList();
            if(!String.IsNullOrEmpty(searchyear))
            {
                String[] sYear = searchyear.Trim().Split('~');
                if (sYear.Length > 1)
                    result = result.Where(a => a.Year >= int.Parse(sYear[0]) && a.Year <= int.Parse(sYear[1])).ToList();
                else
                    result = result.Where(a => a.Year == int.Parse(sYear[0])).ToList();
            }

            return View(result);
        }

        #region CreatePaperData
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaperMain paper)
        {
            var errors = ModelState.SelectMany(m => m.Value.Errors);
            if (ModelState.IsValid)
            {
                bool result = db.Papers.Any(m => m.Title == paper.Title);
                if (!result)
                {
                    db.Papers.Add(paper);
                    db.SaveChanges();
                    return RedirectToAction("UploadFile", new { title = paper.Title});
                }
                else
                    ModelState.AddModelError("", "This Paper Already Exists");
            }
            return View();
        }
        #endregion

        #region EditPaper
        public ActionResult Edit(int id)
        {
            PaperMain paper = db.Papers.Find(id);
            return View(paper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaperMain paper)
        {
            if(ModelState.IsValid)
            {
                paper.Creater = User.Identity.Name;
                db.Entry(paper).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UploadFile", new { title = paper.Title});
            }
            return View(paper);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            PaperMain paper = db.Papers.Find(id);
            return View(paper);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            PaperMain paper = db.Papers.Find(id);

            if(!String.IsNullOrEmpty(paper.PDF))
                System.IO.File.Delete(paper.PDF);

            db.Papers.Remove(paper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region UploadFile
        public ActionResult UploadFile(String title)
        {
            ViewBag.temp = title;
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFile(HttpPostedFileBase upfile,PaperMain pp)
        {
            if(upfile!=null)
            {
                if(upfile.ContentLength >0)
                {
                    //String ss = "~/UploadFile/" + User.Identity.Name + "/";
                    String ss = Server.MapPath("~/Upload/" + User.Identity.Name + "/");
                    if (!Directory.Exists(ss))
                        Directory.CreateDirectory(ss);
                    String saveName = Path.Combine(Server.MapPath("~/Upload/" + User.Identity.Name + "/"), pp.Title + ".txt");
                    upfile.SaveAs(saveName);

                    var result = from m in db.Papers
                                 where m.Title == pp.Title
                                 select m;
                    foreach (PaperMain rr in result)
                    {
                        rr.PDF =  Server.MapPath("~/Upload/" + User.Identity.Name + "/") + pp.Title + ".txt";
                        db.Entry(rr).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region DownLoadFile
        public ActionResult DownLoadPDF(int? id)
        {
            String filepath = db.Papers.Find(id).PDF;
            String filename = Path.GetFileName(filepath);
            Stream iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(iStream, "application/unknow", filename);
        }
        #endregion

    }
}