using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();

        private ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(string search)
        {
            
            ////** Basic **////
            //var data = db.Products.Where(p => p.Price < 10 && p.Price > 9);
            //Product product = db.Products.FirstOrDefault(p => p.Price < 10);


            ////** Plus OrderBy **////
            //var data = db.Products
            //    .Where(p => p.ProductName.Contains("100%"))
            //    .Where(p => p.Price < 10)
            //    .OrderBy(p => p.ProductName);


            ////** Plus some conditions **////
            //var data = db.Products.AsQueryable();
            //data = data.Where(p => p.ProductName.Contains("100%"))

            //if (true)
            //{
            //    data = data.Where(p => p.Active == true);
            //}

            //data = data.OrderBy(p => p.ProductName);


            ////** Plus LINQ **////
            //var data1 = from p in db.Products
            //            where p.ProductName.Contains("100")
            //            orderby p.ProductName
            //            select p;
            
            //var data2 = from p in db.Products
            //            where p.ProductName.Contains("100")
            //            orderby p.ProductName
            //            select new NewProductVM
            //            {
            //                ProductName = p.ProductName,
            //                Price = p.Price
            //            };


            ///** 2015/11/14 Use Repository **///
            //var data = db.Products.Where(p => p.ProductId < 10).AsQueryable();
            //var data = repo.All().Where(p => p.ProductId < 10);
            var data = repo.GetTheFirst10();

            // Add search function
            if (!String.IsNullOrEmpty(search))
            {
                data = data.Where(p => p.ProductName.Contains(search));
            }
            data = data.OrderBy(p => p.ProductName);

            return View(data);
        }

        public ActionResult BatchUpdate()
        {
            //var data = db.Products.Where(p => p.ProductId < 10);
            ///** Use Repository **///
            var data = repo.GetTheFirst10();

            foreach (var item in data)
            {
                item.Price = 3;
            }

            //db.SaveChanges();
            ///** Use Repository **///
            repo.UnitOfWork.Commit();
            
            return RedirectToAction("Index");
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Products.Find(id);
            ///** Use Repository **///
            Product product = repo.GetByID(id);
            if (product == null)
            {
                //return HttpNotFound();
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Products.Add(product);
                ///** Use Repository **///
                repo.Add(product);

                //db.SaveChanges();
                ///** Use Repository **///
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Products.Find(id);
            ///** Use Repository **///
            Product product = repo.GetByID(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                ///** Use Repository **///
                ((FabricsEntities)repo.UnitOfWork.Context).Entry(product).State = EntityState.Modified;

                //db.SaveChanges();
                ///** Use Repository **///
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Products.Find(id);
            ///** Use Repository **///
            Product product = repo.GetByID(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ////** Basic, but if this item has link connected to other items in other table, there will be an exception **////
            //Product product = db.Products.Find(id);


            ////** Solve the above problem**////
            //Product product = db.Products.Find(id);
            //var orderLines = db.OrderLines.Where(p => p.ProductId == product.ProductId);
            //foreach (var item in orderLines)
            //{
            //    db.OrderLines.Remove(item);
            //}


            ////** Better, shorter codes**////
            //Product product = db.Products.Find(id);
            //var orderLines = product.OrderLines.ToList();
            //db.OrderLines.RemoveRange(orderLines);


            ////** The Best, finish this action in just one line **////
            //Product product = db.Products.Find(id);
            ///** Use Repository **///
            Product product = repo.GetByID(id);

            //db.OrderLines.RemoveRange(product.OrderLines);
            ///** Use Repository **///
            ((FabricsEntities)repo.UnitOfWork.Context).OrderLines.RemoveRange(product.OrderLines);

            //db.Products.Remove(product);
            ///** Use Repository **///
            repo.Delete(product);

            //db.SaveChanges();
            ///** Use Repository **///
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");


            
            //db.SaveChanges();
            ///** Use Repository **///
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                ///** Use Repository **///
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(NewProductVM product)
        {
            if (ModelState.IsValid)
            {
                var prod = new Product();
                prod.ProductName = product.ProductName;
                prod.Price = product.Price;
                prod.Stock = 1;
                prod.Active = true;

                //db.Products.Add(prod);
                ///** Use Repository **///
                repo.Add(prod);

                try
                {
                    //db.SaveChanges();
                    ///** Use Repository **///
                    repo.UnitOfWork.Commit();

                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    var allErrors = new List<string>();

                    foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
                    {
                        foreach (DbValidationError err in re.ValidationErrors)
                        {
                            allErrors.Add(err.ErrorMessage);
                        }
                    }

                    ViewBag.Errors = allErrors;
                }
            }

            return View();
        }

    }
}
