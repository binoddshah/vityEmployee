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
    public class departmentController : Controller
    {
        private readonly AgroEmployeeDbContext _context;

        public departmentController(AgroEmployeeDbContext context)
        {
            _context = context;
        }

        // GET: department
        public async Task<IActionResult> Index()
        {
            return View(await _context.DepartmentTables.ToListAsync());
        }

        // GET: department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentTable = await _context.DepartmentTables
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (departmentTable == null)
            {
                return NotFound();
            }

            return View(departmentTable);
        }

        // GET: department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName,Description")] DepartmentTable departmentTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentTable);
        }

        // GET: department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentTable = await _context.DepartmentTables.FindAsync(id);
            if (departmentTable == null)
            {
                return NotFound();
            }
            return View(departmentTable);
        }

        // POST: department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentName,Description")] DepartmentTable departmentTable)
        {
            if (id != departmentTable.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentTableExists(departmentTable.DepartmentId))
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
            return View(departmentTable);
        }

        // GET: department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentTable = await _context.DepartmentTables
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (departmentTable == null)
            {
                return NotFound();
            }

            return View(departmentTable);
        }

        // POST: department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentTable = await _context.DepartmentTables.FindAsync(id);
            if (departmentTable != null)
            {
                _context.DepartmentTables.Remove(departmentTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentTableExists(int id)
        {
            return _context.DepartmentTables.Any(e => e.DepartmentId == id);
        }
    }
}
