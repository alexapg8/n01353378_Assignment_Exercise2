using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List()
        {
            //what data do we need?
            List<Species> myspecies = db.Species.SqlQuery("Select * from species").ToList();

            return View(myspecies);
        }

     
     
        // Add
        // [HttpPost] Add
        // URL: /Species/Add
        // GO TO Views -> Species -> Add
        public ActionResult Add()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult Add(string SpeciesName)
        {
            // 1: User input 
            string query = "insert into species (Name) values (@SpeciesName)";
            // 2: Create Query
            SqlParameter sqlparam = new SqlParameter("@SpeciesName", SpeciesName);
            // 3: Run Query
            db.Database.ExecuteSqlCommand(query, sqlparam);
            // 4: Go to list of Species
            return RedirectToAction("List");
        }
        //Delete
        //show confirmation messsage with species name to delete
        public ActionResult Delete(int id)
        {

            Species species = db.Species.SqlQuery("select * from species where speciesid=@SpeciesID", new SqlParameter("@SpeciesID", id)).FirstOrDefault();

            return View(species);
        }
        //Confirmation of delete
        public ActionResult DeleteF(int id)
        {
            string query = "delete from species where speciesid=@id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparam);
            return RedirectToAction("List");
        }
        //Show
        public ActionResult Show(int id)
        {
            string query = "select * from species where speciesid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();
            return View(selectedspecies);
        }

        //Update view
        public ActionResult Update(int id)
        {
            string query = "select * from species where speciesid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();
            return View(selectedspecies);
        }
        //Update post
        [HttpPost]
        public ActionResult Update(int id, string SpeciesName)
        {
            string query = "update species set Name=@SpeciesName where speciesid = @id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@SpeciesName", SpeciesName);
            sqlparams[1] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        
    }
}