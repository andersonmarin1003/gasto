using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gasto.web.Data;
using gasto.web.Data.Entities;

namespace gasto.web.Controllers
{
    public class gastosController : Controller
    {
        private readonly DataContext _context;

        public gastosController(DataContext context)
        {
            _context = context;
        }

        // GET: gastos
        public async Task<IActionResult> Index()
        {
            return View(await _context.gastos.ToListAsync());
        }

        // GET: gastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoEntity = await _context.gastos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastoEntity == null)
            {
                return NotFound();
            }

            return View(gastoEntity);
        }

        // GET: gastos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: gastos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Plaque")] gastoEntity gastoEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gastoEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gastoEntity);
        }

        // GET: gastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoEntity = await _context.gastos.FindAsync(id);
            if (gastoEntity == null)
            {
                return NotFound();
            }
            return View(gastoEntity);
        }

        // POST: gastos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Plaque")] gastoEntity gastoEntity)
        {
            if (id != gastoEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gastoEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!gastoEntityExists(gastoEntity.Id))
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
            return View(gastoEntity);
        }

        // GET: gastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastoEntity = await _context.gastos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastoEntity == null)
            {
                return NotFound();
            }

            return View(gastoEntity);
        }

        // POST: gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gastoEntity = await _context.gastos.FindAsync(id);
            _context.gastos.Remove(gastoEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool gastoEntityExists(int id)
        {
            return _context.gastos.Any(e => e.Id == id);
        }
    }
}
