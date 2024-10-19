using DataSystemFront.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DataSystemFront.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataSystemFront.Controllers
{
    public class TarefaController : Controller
    {
        private readonly ITarefa _ITarefa;
        public TarefaController(ITarefa tarefa)
        {
            _ITarefa = tarefa;
        }
        public IActionResult Index()
        {
            try
            {
               return View(_ITarefa.GetAllTarefasAsync());
            }
            catch
            {
                return View();
            }
           
        }
       
        public ActionResult Details(int id)
        {
            return View(_ITarefa.GetTarefaByIdAsync(id));
        }
        public List<SelectListItem> TrazStatusList()
        {
            List<SelectListItem> StatusList = Enum.GetValues(typeof(Status))
                         .Cast<Status>()
                         .Select(s => new SelectListItem
                         {
                             Value = s.ToString(),
                             Text = s.ToString()
                         }).ToList();
            return StatusList;
        }
        public ActionResult Create()
        {
            var model = new TarefaModel();
            model.StatusList = TrazStatusList();
            DateTime data = DateTime.Now;
            model.DataConclusao = new DateTime(data.Year, data.Month, data.Day, data.Hour, data.Minute, 0);
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TarefaModel collection)
        {
            var model = new TarefaModel();
            model.StatusList = TrazStatusList();
            model.Titulo = collection.Titulo;
            model.Descricao = collection.Descricao;
            model.DataConclusao = collection.DataConclusao;
            try
            {
                string respota = _ITarefa.AddTarefaAsync(collection);
                ViewBag.MensagemErro = respota;
                if(respota== "Tarefa adicionada com sucesso!")
                {
                    TempData["Mensagem"] = respota;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
                }
            }
            catch(Exception ex)
            {
                ViewBag.MensagemErro = ex;
                return View(model);
            }
        }
        public ActionResult Edit(int id)
        {
            var model = _ITarefa.GetTarefaByIdAsync(id);
            DateTime data = model.DataConclusao;
            model.DataConclusao = new DateTime(data.Year, data.Month, data.Day, data.Hour, data.Minute, 0);
            model.StatusList = TrazStatusList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TarefaModel collection)
        {
            try
            {
                if (collection.Descricao == null) collection.Descricao = "";       
                string respota = _ITarefa.UpdateTarefaAsync(collection, id);
                if (respota == "Tarefa atualizada com sucesso")
                {
                    TempData["Mensagem"] = respota;
                    return RedirectToAction(nameof(Index));
                }        
                ViewBag.MensagemErro = respota;
                var model = _ITarefa.GetTarefaByIdAsync(id);
                model.StatusList = TrazStatusList();
                return View(model);
            }
            catch(Exception ex)
            {
                var model = _ITarefa.GetTarefaByIdAsync(id);
                model.StatusList = TrazStatusList();
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            return View(_ITarefa.GetTarefaByIdAsync(id));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TarefaModel tarefa)
        {
            try
            {
                _ITarefa.DeleteTarefaByIDAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
