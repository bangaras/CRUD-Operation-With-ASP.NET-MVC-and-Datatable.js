using Datatable.js.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Datatable.js.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (datatable_practiceEntities db = new datatable_practiceEntities())
            {
                List<Employee> employees = db.Employee.ToList<Employee>();
                return Json(new {data=employees},JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpGet]
        public ActionResult AddorEdit(int id=0)
        {
            if(id==0)
            {
                return View(new Employee());
            }
            else
            {
                using (datatable_practiceEntities db = new datatable_practiceEntities())
                {
                    return View(db.Employee.Where(x=>x.Id==id).FirstOrDefault<Employee>());

                }
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(Employee emp)
        {

            if(ModelState.IsValid)
            {
                using (datatable_practiceEntities db = new datatable_practiceEntities())
                {
                    if(emp.Id==0)
                    {
                        db.Employee.Add(emp);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        db.Entry(emp).State=EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (datatable_practiceEntities db = new datatable_practiceEntities())
            {
                Employee emp=db.Employee.Where(x=>x.Id==id).FirstOrDefault<Employee>();
                db.Employee.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" },JsonRequestBehavior.AllowGet);
            }
        }
    }
}