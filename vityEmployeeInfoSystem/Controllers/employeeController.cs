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
    public class employeeController : Controller
    {
        private readonly AgroEmployeeDbContext _context;

        public employeeController(AgroEmployeeDbContext context)
        {
            _context = context;
        }

        // GET: employee
        public async Task<IActionResult> Index()
        {
            var agroEmployeeDbContext = _context.EmployeeTables.Include(e => e.Department).Include(e => e.Designation);
            return View(await agroEmployeeDbContext.ToListAsync());
        }

        // GET: employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTable = await _context.EmployeeTables
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeTable == null)
            {
                return NotFound();
            }

            return View(employeeTable);
        }

        // GET: employee/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId");
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId");
            return View();
        }

        // POST: employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,DesignationId,DepartmentId,Address,Contact,Email,Dob,Salary")] EmployeeTable employeeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", employeeTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", employeeTable.DesignationId);
            return View(employeeTable);
        }

        // GET: employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTable = await _context.EmployeeTables.FindAsync(id);
            if (employeeTable == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", employeeTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", employeeTable.DesignationId);
            return View(employeeTable);
        }

        // POST: employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,DesignationId,DepartmentId,Address,Contact,Email,Dob,Salary")] EmployeeTable employeeTable)
        {
            if (id != employeeTable.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeTableExists(employeeTable.EmployeeId))
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
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", employeeTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", employeeTable.DesignationId);
            return View(employeeTable);
        }

        // GET: employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTable = await _context.EmployeeTables
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeTable == null)
            {
                return NotFound();
            }

            return View(employeeTable);
        }

        // POST: employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeTable = await _context.EmployeeTables.FindAsync(id);
            if (employeeTable != null)
            {
                _context.EmployeeTables.Remove(employeeTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeTableExists(int id)
        {
            return _context.EmployeeTables.Any(e => e.EmployeeId == id);
        }
    }
}
