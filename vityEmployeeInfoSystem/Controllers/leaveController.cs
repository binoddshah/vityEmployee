﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vityEmployeeInfoSystem.Models;

namespace vityEmployeeInfoSystem.Controllers
{
    public class leaveController : Controller
    {
        private readonly AgroEmployeeDbContext _context;

        public leaveController(AgroEmployeeDbContext context)
        {
            _context = context;
        }

        // GET: leave
        public async Task<IActionResult> Index()
        {
            var agroEmployeeDbContext = _context.LeaveTables.Include(l => l.Employee);
            return View(await agroEmployeeDbContext.ToListAsync());
        }

        // GET: leave/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveTable = await _context.LeaveTables
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leaveTable == null)
            {
                return NotFound();
            }

            return View(leaveTable);
        }

        // GET: leave/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTables, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: leave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveId,EmployeeId,LeaveDate,Reason")] LeaveTable leaveTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTables, "EmployeeId", "EmployeeId", leaveTable.EmployeeId);
            return View(leaveTable);
        }

        // GET: leave/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveTable = await _context.LeaveTables.FindAsync(id);
            if (leaveTable == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTables, "EmployeeId", "EmployeeId", leaveTable.EmployeeId);
            return View(leaveTable);
        }

        // POST: leave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveId,EmployeeId,LeaveDate,Reason")] LeaveTable leaveTable)
        {
            if (id != leaveTable.LeaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTableExists(leaveTable.LeaveId))
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
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTables, "EmployeeId", "EmployeeId", leaveTable.EmployeeId);
            return View(leaveTable);
        }

        // GET: leave/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveTable = await _context.LeaveTables
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leaveTable == null)
            {
                return NotFound();
            }

            return View(leaveTable);
        }

        // POST: leave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveTable = await _context.LeaveTables.FindAsync(id);
            if (leaveTable != null)
            {
                _context.LeaveTables.Remove(leaveTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTableExists(int id)
        {
            return _context.LeaveTables.Any(e => e.LeaveId == id);
        }
    }
}
