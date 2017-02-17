using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PessoaController : Controller
    {
        // GET: Pessoa
        public ActionResult Index()
        {
            ViewBag.Pessoa = "Tayna";

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(PessoaModels model)
        {            
            List<PessoaModels> listPessoas = new List<PessoaModels>();
            
            if(Session["listPessoas"] != null)
            {
                listPessoas.AddRange((List<PessoaModels>)Session["listPessoas"]);
            }

            listPessoas.Add(model);
            Session["listPessoas"] = listPessoas;

            return View("Index");
        }

        public ActionResult List()
        {
            if (Session["listPessoas"] != null)
            {
                var model = (List<PessoaModels>)Session["listPessoas"];
                return View(model);
            }

            return View(new List<PessoaModels>());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (((List<PessoaModels>)Session["listPessoas"]).Where(p => p.Codigo == id).Any())
            {
                var model = ((List<PessoaModels>)Session["listPessoas"]).Where(p => p.Codigo == id).FirstOrDefault();

                return View("Create", model);
            }

            return View("Create", new List<PessoaModels>());
        }
        [HttpPost]
        public ActionResult Edit(PessoaModels model)
        {
            if (Session["listPessoas"] != null)
            {
                if (((List<PessoaModels>)Session["listPessoas"]).Where(p => p.Codigo == model.Codigo).Any())
                {
                    var modelBase = ((List<PessoaModels>)Session["listPessoas"]).Where(p => p.Codigo == model.Codigo).FirstOrDefault();

                    ((List<PessoaModels>)Session["listPessoas"])[modelBase.Codigo] = model;
                }

                var list = (List <PessoaModels>)Session["listPessoas"];
                return View("List", list);
            }
           
            return View("Create",new List<PessoaModels>());
        }
    }
}