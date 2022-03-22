using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcEntityframe.Models;

namespace MvcEntityframe.Controllers
{
    public class HomeController : Controller
    {
        testEntities3 ts = new testEntities3();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var db=ts.userstates.ToList();
            
            List<person> uf = new List<person>();
            foreach (var i in db) 
            {
                person f=new person();
                f.id = i.id;
                f.firstname = i.firstname;
                f.lastname = i.lastname;
                f.email = i.email;
                f.address = i.address;
                f.stateid = ts.states.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
                f.cityid = ts.city1.Where(x => x.cityid == i.cityid).SingleOrDefault().cityname;
                f.number = i.number.ToString();
                uf.Add(f);
            }
            return View(uf);
        }
       
        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            List<ddl> ddl = new List<Models.ddl>()
            {
                new ddl
                {
                    id=0,
                    name="Please select"
                }
            };

            person f = new person();
            var citylist = ts.city1.ToList();
            var statelist = ts.states.ToList();

            f.states = new SelectList(statelist, "stateid", "statename");
            f.cities = new SelectList(ddl, "id", "name");
            return View(f);
           
        } 

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(person uf)
        {
            person p = new person();
            try
            {
                List<ddl> ddl = new List<Models.ddl>()
            {
                new ddl
                {
                    id=0,
                    name="Please select"
                }
            };
                // TODO: Add insert logic here
                var statelist = ts.states.ToList();
                p.states = new SelectList(statelist, "stateid", "statename");
                var citylist = ts.city1.ToList();
                p.cities = new SelectList(ddl, "id", "name");
                var task = ts.userstates.Where(x => x.email==uf.email).SingleOrDefault();
                if (task == null)
                {
                    userstate f = new userstate();
                    f.firstname = uf.firstname;
                    f.lastname = uf.lastname;
                    f.email = uf.email;
                    f.address = uf.address;
                    f.number = uf.number;
                    f.stateid = Convert.ToInt32(uf.stateid);
                    f.cityid =Convert.ToInt32(uf.cityid);
                    ts.userstates.AddObject(f);
                    ts.SaveChanges();
                    return RedirectToAction("Index");
                }
                else 
                {
                    ViewBag.Message = "Already email Exist";
                    return View(p);
                }
            }
            catch
            {
                return View(p);
            }
        }
        
        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {

            person f = new person();
            var statelist = ts.states.ToList();
            f.states = new SelectList(statelist, "stateid", "statename");
         
               
            var t = ts.userstates.Where(x => x.id == id).SingleOrDefault();
            var citylist = ts.city1.Where(x => x.stateid==t.stateid).ToList();
            f.cities = new SelectList(citylist, "cityid", "cityname");
            f.id = t.id;
            f.firstname = t.firstname;
            f.lastname = t.lastname;
            f.email = t.email;
            f.number = t.number;
            f.stateid = t.stateid.ToString();
            f.cityid = t.cityid.ToString();
            return View(f);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, person f)
        {
            var citylist = ts.city1.ToList();
            var statelist = ts.states.ToList();
            f.states = new SelectList(statelist, "stateid", "statename");
            f.cities = new SelectList(citylist, "cityid", "cityname");
            try
            {
                // TODO: Add update logic here
                
                var t = ts.userstates.Where(x => x.id == id).SingleOrDefault();
                t.id = f.id;
                t.firstname = f.firstname;
                t.lastname = f.lastname;
                t.email = f.email;
                t.number = f.number;
                t.stateid = Convert.ToInt32(f.stateid);
                t.cityid =Convert.ToInt32(f.cityid);
                ts.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public JsonResult GetcityByStateId(int stateid)
        {
            using (testEntities3 db = new testEntities3())
            {
                List<city1> lstStates = new List<city1>();
                lstStates = db.city1.Where(x => x.stateid == stateid).ToList();
                person f = new person();
                f.cities = new SelectList(lstStates, "cityid", "cityname");
                return Json(f.cities, JsonRequestBehavior.AllowGet);
            }
        }


        //
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
                var pv = ts.userstates.Where(x => x.id == id).SingleOrDefault();
                ts.DeleteObject(pv);
                ts.SaveChanges();
                return RedirectToAction("Index");
        }

        //
        // POST: /Home/Delete/5

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
