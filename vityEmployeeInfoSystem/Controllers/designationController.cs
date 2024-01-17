using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vityEmployeeInfoSystem.Models;

namespace vityEmployeeInfoSystem.Controllers
{
    public class designationController : Controller
    {
        private readonly AgroEmployeeDbContext _context;

        public designationController(AgroEmployeeDbContext context)
        {
            _context = context;
        }

        // GET: designation
        public async Task<IActionResult> Index()
        {
            return View(await _context.DesignationTables.ToListAsync());
        }

        // GET: designation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designationTable = await _context.DesignationTables
                .FirstOrDefaultAsync(m => m.DesignationId == id);
            if (designationTable == null)
            {
                return NotFound();
            }

            return View(designationTable);
        }

        // GET: designation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: designation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DesignationId,DesignationName,Description")] DesignationTable designationTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designationTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(designationTable);
        }

        // GET: designation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designationTable = await _context.DesignationTables.FindAsync(id);
            if (designationTable == null)
            {
                return NotFound();
            }
            return View(designationTable);
        }

        // POST: designation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DesignationId,DesignationName,Description")] DesignationTable designationTable)
        {
            if (id != designationTable.DesignationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designationTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignationTableExists(designationTable.DesignationId))
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
            return View(designationTable);
        }

        // GET: designation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designationTable = await _context.DesignationTables
                .FirstOrDefaultAsync(m => m.DesignationId == id);
            if (designationTable == null)
            {
                return NotFound();
            }

            return View(designationTable);
        }

        // POST: designation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designationTable = await _context.DesignationTables.FindAsync(id);
            if (designationTable != null)
            {
                _context.DesignationTables.Remove(designationTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignationTableExists(int id)
        {
            return _context.DesignationTables.Any(e => e.DesignationId == id);
        }
    }
}
