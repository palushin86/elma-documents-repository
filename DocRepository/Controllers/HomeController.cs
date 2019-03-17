using DocRepository.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocRepository.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var documents = session.Query<Documents>();

                switch (sortOrder)
                {
                    case "name_desc":
                        documents = documents.OrderByDescending(s => s.DocName);
                        break;
                    case "Date":
                        documents = documents.OrderBy(s => s.DocDate);
                        break;
                    case "date_desc":
                        documents = documents.OrderByDescending(s => s.DocDate);
                        break;
                    case "Author":
                        documents = documents.OrderBy(s => s.DocAuthor);
                        break;
                    case "author_desc":
                        documents = documents.OrderByDescending(s => s.DocAuthor);
                        break;
                    default:
                        documents = documents.OrderBy(s => s.DocName);
                        break;
                }

                var document = documents.ToList();

                return View(document);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string sortOrder, string filter)
        {
            ViewBag.filter = filter;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var documents = session.Query<Documents>();

                if (!String.IsNullOrEmpty(filter))
                {
                    documents = documents.Where(s => s.DocName.Contains(filter)
                                           || s.DocAuthor.Contains(filter));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        documents = documents.OrderByDescending(s => s.DocName);
                        break;
                    case "Date":
                        documents = documents.OrderBy(s => s.DocDate);
                        break;
                    case "date_desc":
                        documents = documents.OrderByDescending(s => s.DocDate);
                        break;
                    case "Author":
                        documents = documents.OrderBy(s => s.DocAuthor);
                        break;
                    case "author_desc":
                        documents = documents.OrderByDescending(s => s.DocAuthor);
                        break;
                    default:
                        documents = documents.OrderBy(s => s.DocName);
                        break;
                }

                var document = documents.ToList();


                return View(document);

            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Documents document)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        document.DocDate = DateTime.Now;
                        document.DocAuthor = User.Identity.Name;
                        HttpPostedFileBase file = document.file;
                        var fileName = Path.GetFileName(file.FileName);

                        if (file.ContentLength > 0)
                            {
                            var path = Path.Combine(Server.MapPath("/Files/"), fileName);

                            
                            if (System.IO.File.Exists(path))
                            {
                                
                                fileName = "_" + fileName;
                                path = Path.Combine(Server.MapPath("/Files/"), fileName);
                            }
                           
                            file.SaveAs(path);

                        }

                        document.DocFileName = fileName;

                        session.Save(document);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }


        //EDIT
        [Authorize]
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var document = session.Get<Documents>(id);
                return View(document);
            }

        }

        [HttpPost]
        public ActionResult Edit(int id, Documents document)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var documenttoUpdate = session.Get<Documents>(id);

                    documenttoUpdate.DocName = document.DocName;
                    documenttoUpdate.DocDate = document.DocDate;
                    documenttoUpdate.DocAuthor = document.DocAuthor;
                    documenttoUpdate.DocFileName = document.DocFileName;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(documenttoUpdate);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //DELETE
        [Authorize]
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var document = session.Get<Documents>(id);
                return View(document);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, Documents document)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(document);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }









    }
}