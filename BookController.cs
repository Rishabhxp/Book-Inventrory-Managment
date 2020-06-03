using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        kartikEntities2 dbobj = new kartikEntities2();

        //public JsonResult Search( string tags)
        //{
        //    List<tbl_book> li = dbobj.tbl_book.OrderBy(x => x.ID).ToList();
        //    return Json(li, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Book(tbl_book obj)
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult GetData(tbl_book model)
        {
            tbl_book obj = new tbl_book();
            ModelState.Remove("ID");
            if (ModelState.IsValid)
            {
                obj.ID=model.ID;
                obj.Title = model.Title;
                obj.Author = model.Author;
                obj.ISBN = model.ISBN;
                obj.Publisher = model.Publisher;
                obj.Year = model.Year;

                if(model.ID == 0)
                {
                    dbobj.tbl_book.Add(obj);
                    dbobj.SaveChanges();

                }
                else
                {
                    dbobj.Entry(obj).State = EntityState.Modified;
                    dbobj.SaveChanges();
                }
                
            }
            ModelState.Clear();

            return View("Book");
        }
        public ActionResult Booklist( )
        {
            //var res = dbobj.tbl_book.ToList();
            //return View(res);
            return View(dbobj.tbl_book.ToList());

            
        }
        [HttpPost]
        public JsonResult GetDataList(string SearchBy, string SearchValue )
        {
            List<tbl_book> objlist = new List<tbl_book>();
            if (SearchBy == "Title")
            {
                try
                {

                    objlist = dbobj.tbl_book.Where(x => x.Title.StartsWith(SearchValue) || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0},is not a ID", SearchValue);
                }
                return Json(objlist, JsonRequestBehavior.AllowGet);

            }

            else if (SearchBy == "Author")
            {
                objlist = dbobj.tbl_book.Where(x => x.Author.StartsWith(SearchValue) || SearchValue == null).ToList();
                return Json(objlist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                objlist = dbobj.tbl_book.Where(x => x.ISBN.StartsWith(SearchValue) || SearchValue == null).ToList();
                return Json(objlist, JsonRequestBehavior.AllowGet);
            }
           
        }


        public ActionResult Delete(int id)
        {
            
            var res = dbobj.tbl_book.Where(x => x.ID == id).First();
            dbobj.tbl_book.Remove(res);
            dbobj.SaveChanges();
            var list = dbobj.tbl_book.ToList();
            return View("Booklist",list);    
        }
        
       
    }
}