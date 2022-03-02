using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appCaminhao.Models;
using DomainApp.Interfaces;
using DomainApp.Entities;
using Repository.Context;
using Microsoft.AspNetCore.Authorization;

namespace appCaminhao.Controllers
{
    [Authorize]
    public class CaminhoesController : Controller
    {
        private readonly ICaminhaoBusinessService _caminhaoBusinessService;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CaminhoesController(AppMainDbContext context, ICaminhaoBusinessService caminhaoBusinessService)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this._caminhaoBusinessService = caminhaoBusinessService;
        }

        private CaminhaoViewModel ModelToView(Caminhao model)
        {

            CaminhaoViewModel caminhaoViewModel = new CaminhaoViewModel
            {
                Id = model.Id,
                AnoFabricacao = model.AnoFabricacao,
                AnoModelo = model.AnoModelo,
                Fabricante = model.Fabricante,
                Modelo = model.Modelo
            };

            return caminhaoViewModel;
        }

        private Caminhao ViewToModel(CaminhaoViewModel viewModel)
        {
            Caminhao caminhao = new Caminhao
            {
                Id = viewModel.Id,
                AnoFabricacao = viewModel.AnoFabricacao,
                AnoModelo = viewModel.AnoModelo,
                Fabricante = viewModel.Fabricante,
                Modelo = viewModel.Modelo
            };

            return caminhao;
        }

        private SelectList ListaFabricantes()
        {
            return new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Volvo", Value = "Volvo" },
                new SelectListItem { Text = "Scania", Value = "Scania" },
                new SelectListItem { Text = "Scania", Value = "Ford" },
            }, "Value", "Text");
        }

        private SelectList ListaModelos()
        {
            return new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "FH", Value = "FH" },
                new SelectListItem { Text = "FM", Value = "FM" },
            }, "Value", "Text");
        }

        // GET: Caminhoes
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CaminhaoViewModel> lst = new();
            var lista = await _caminhaoBusinessService.GetAll();

            foreach (var item in lista)
            {
                lst.Add(ModelToView(item));
            }
            
            return View(lst);
        }

        // GET: Caminhoes/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _caminhaoBusinessService.GetById(id);
            if (caminhao == null)
            {
                return NotFound();
            }

            return View(ModelToView(caminhao));
        }

        // GET: Caminhoes/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.listaFabircantes = ListaFabricantes();
            ViewBag.listaModelos = ListaModelos();

            return View();
        }

        // POST: Caminhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fabricante,Modelo,AnoFabricacao,AnoModelo")] CaminhaoViewModel caminhaoViewModel)
        {
            ViewBag.listaFabircantes = ListaFabricantes();
            ViewBag.listaModelos = ListaModelos();

            if (!_caminhaoBusinessService.ValidarDifAnos(caminhaoViewModel.AnoModelo, caminhaoViewModel.AnoFabricacao))
            {
                ModelState.AddModelError("AnoModelo", "Ano de fabricação e do modelo, devem ser iguais, ou no máximo ter uma diferença de 1 ano.");
            }

            if (caminhaoViewModel != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await _caminhaoBusinessService.Add(ViewToModel(caminhaoViewModel));
                        await _caminhaoBusinessService.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(Guid.NewGuid().ToString(), ex.Message);
                    }
                }
            }

            return View(caminhaoViewModel);
        }

        // GET: Caminhoes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _caminhaoBusinessService.GetById(id);
            if (caminhao == null)
            {
                return NotFound();
            }
            return View(ModelToView(caminhao));
        }

        // POST: Caminhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Fabricante,Modelo,AnoFabricacao,AnoModelo")] CaminhaoViewModel caminhaoViewModel)
        {
            if (id != caminhaoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _caminhaoBusinessService.Update(ViewToModel(caminhaoViewModel));
                    await _caminhaoBusinessService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaminhoesExists(caminhaoViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(caminhaoViewModel);
        }

        // GET: Caminhoes/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _caminhaoBusinessService.GetById(id);
            if (caminhao == null)
            {
                return NotFound();
            }

            return View(ModelToView(caminhao));
        }

        // POST: Caminhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var caminhao = await _caminhaoBusinessService.GetById(id);
            if (caminhao != null)
            {
                await _caminhaoBusinessService.Remove(caminhao);
                await _caminhaoBusinessService.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        private bool CaminhoesExists(long id)
        {
            return _caminhaoBusinessService.GetById(id).Result != null;
        }
    }
}
