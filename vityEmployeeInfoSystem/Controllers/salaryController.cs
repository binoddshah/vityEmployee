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
    public class salaryController : Controller
    {
        private readonly AgroEmployeeDbContext _context;

        public salaryController(AgroEmployeeDbContext context)
        {
            _context = context;
        }

        // GET: salary
        public async Task<IActionResult> Index()
        {
            var agroEmployeeDbContext = _context.SalaryTables.Include(s => s.Employee);
            return View(await agroEmployeeDbContext.ToListAsync());
        }

        // GET: salary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaryTable = await _context.SalaryTables
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SalaryId == id);
            if (salaryTable == null)
            {
                return NotFound();
            }

            return View(salaryTable);
        }

        // GET: salary/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTables, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: salary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaryId,EmployeeId,ReleasedAmount,ReleasedDate,ReleasedForMonth")] SalaryTable salaryTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salaryTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTables, "EmployeeId", "EmployeeId", salaryTable.EmployeeId);
            return View(salaryTable);
        }

        // GET: salary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaryTable = await _context.SalaryTables.FindAsync(id);
            if (salaryTable == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTables, "EmployeeId", "EmployeeId", salaryTable.EmployeeId);
            return View(salaryTable);
        }

        // POST: salary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalaryId,EmployeeId,ReleasedAmount,ReleasedDate,ReleasedForMonth")] SalaryTable salaryTable)
        {
            if (id != salaryTable.SalaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salaryTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryTableExists(salaryTable.SalaryId))
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
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTables, "EmployeeId", "EmployeeId", salaryTable.EmployeeId);
            return View(salaryTable);
        }

        // GET: salary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaryTable = await _context.SalaryTables
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SalaryId == id);
            if (salaryTable == null)
            {
                return NotFound();
            }

            return View(salaryTable);
        }

        // POST: salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salaryTable = await _context.SalaryTables.FindAsync(id);
            if (salaryTable != null)
            {
                _context.SalaryTables.Remove(salaryTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryTableExists(int id)
        {
            return _context.SalaryTables.Any(e => e.SalaryId == id);
        }
    }
}
