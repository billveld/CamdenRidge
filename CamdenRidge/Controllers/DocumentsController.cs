using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CamdenRidge.DAL;
using CamdenRidge.Models;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace CamdenRidge.Controllers
{
    
    public class DocumentsController : Controller
    {



        private CamdenRidgeContext db = new CamdenRidgeContext();
        
        private ApplicationDbContext appdb = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        public DocumentsController()
        {
        }

        public DocumentsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: Documents
        public ActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        // GET: Documents/Details/5
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Category,Display,Sequence,Public")] Document document, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid && upload.ContentLength != 0)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container;
                if (document.Public == true)
                {
                    container = blobClient.GetContainerReference("public");

                }
                else
                {
                    container = blobClient.GetContainerReference("private");
                }

                container.CreateIfNotExists();
                container.SetPermissions( new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(upload.FileName);
                blockBlob.Properties.ContentType = upload.ContentType;

                blockBlob.UploadFromStream(upload.InputStream);
                document.Path = blockBlob.Uri.ToString();
                
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        // GET: Documents/Edit/5
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Edit([Bind(Include = "ID,Title,Path,Category,Display,Sequence,Public")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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


        public ActionResult PublicDocuments()
        {
            return View(db.Documents.Where(x => x.Public == true && x.Display == true));
        }

        [Authorize]
        public ActionResult AllDocuments()
        {
            AllDocumentsViewModel model = new AllDocumentsViewModel();
            model.Documents = db.Documents.Where(x => x.Display == true).ToList();

            var userId = User.Identity.GetUserId();
            
            model.AllowCreate = UserManager.IsInRole(userId, "Admin") || UserManager.IsInRole(userId, "Board Member") || UserManager.IsInRole(userId, "AECC Member");
            return View(model);
        }
    }
}
