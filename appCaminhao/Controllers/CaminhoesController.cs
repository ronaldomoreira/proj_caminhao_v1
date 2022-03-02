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

namespace appCaminhao.Controllers
{
    public class CaminhoesController : Controller
    {
        private readonly ICaminhaoBusinessService _caminhaoBusinessService;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CaminhoesController(AppMainDbContext context, ICaminhaoBusinessService _caminhaoBusinessService)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _caminhaoBusinessService = _caminhaoBusinessService;
        }

        // GET: Caminhoes
        public async Task<IActionResult> Index()
        {
            return View( await _caminhaoBusinessService.GetAll());
        }

        // GET: Caminhoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Caminhao = await _caminhaoBusinessService.GetById(id);
            if (Caminhao == null)
            {
                return NotFound();
            }

            return View(Caminhao);
        }

        // GET: Caminhoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caminhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fabricante,Modelo,AnoFabricacao,AnoModelo")] Caminhao caminhao)
        {
            if (ModelState.IsValid)
            {
                await _caminhaoBusinessService.Add(caminhao);
                await _caminhaoBusinessService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caminhao);
        }

        // GET: Caminhoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Caminhoes = await _caminhaoBusinessService.GetById(id);
            if (Caminhoes == null)
            {
                return NotFound();
            }
            return View(Caminhoes);
        }

        // POST: Caminhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Fabricante,Modelo,AnoFabricacao,AnoModelo")] Caminhao caminhao)
        {
            if (id != caminhao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _caminhaoBusinessService.Update(caminhao);
                    await _caminhaoBusinessService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaminhoesExists(caminhao.Id))
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
            return View(caminhao);
        }

        // GET: Caminhoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Caminhoes = await _caminhaoBusinessService.GetById(id);
            if (Caminhoes == null)
            {
                return NotFound();
            }

            return View(Caminhoes);
        }

        // POST: Caminhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var caminhaoTmp = await _caminhaoBusinessService.GetById(id);
            if (caminhaoTmp != null)
            {
                await _caminhaoBusinessService.Remove(caminhaoTmp);
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
