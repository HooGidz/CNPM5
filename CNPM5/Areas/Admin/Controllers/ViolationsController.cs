using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CNPM5.Models;

namespace CNPM5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ViolationsController : Controller
    {
        private readonly Cnpm5Context _context;

        public ViolationsController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/Violations
        public async Task<IActionResult> Index()
        {
            //var cnpm5Context = _context.Violations.Include(v => v.Rule).Include(v => v.Student);
            //return View(await cnpm5Context.ToListAsync());
            return View(await _context.Violations
                .Include(v => v.Rule)
                .Include(v => v.Student)
                .ToListAsync());
        }

        // GET: Admin/Violations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations
                .Include(v => v.Rule)
                .Include(v => v.Student)
                .FirstOrDefaultAsync(m => m.ViolationId == id);
            if (violation == null)
            {
                return NotFound();
            }

            return View(violation);
        }

        // GET: Admin/Violations/Create
        public IActionResult Create()
        {
            // load dropdowns
            ViewData["StudentId"] = new SelectList(_context.TblStudents.OrderBy(s => s.FullName).ToList(), "StudentId", "FullName");
            ViewData["RuleId"] = new SelectList(_context.Rules.OrderBy(r => r.RuleName).ToList(), "RuleId", "RuleName");

            // trả View với model rỗng
            return View(new Violation());
            //ViewData["RuleId"] = new SelectList(_context.Rules, "RuleId", "RuleName");
            //ViewData["StudentId"] = new SelectList(_context.TblStudents, "StudentId", "FullName");
            //return View();
        }

        // POST: Admin/Violations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ViolationId,StudentId,RuleId,Note")] Violation violation)
        {
            if (ModelState.IsValid)
            {
                //trả lại view khi dữ liệu lỗi
                ViewData["StudentId"] = new SelectList(_context.TblStudents.OrderBy(s => s.FullName).ToList(), "StudentId", "FullName", violation.StudentId);
                ViewData["RuleId"] = new SelectList(_context.Rules.OrderBy(r => r.RuleName).ToList(), "RuleId", "RuleName", violation.RuleId);
                return View(violation);
                //violation.ViolationDate = DateTime.Now;
                //_context.Add(violation);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            violation.ViolationDate = DateTime.Now;
            _context.Add(violation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //ViewData["RuleId"] = new SelectList(_context.Rules, "RuleId", "RuleName", violation.RuleId);
            //ViewData["StudentId"] = new SelectList(_context.TblStudents, "StudentId", "FullName", violation.StudentId);
            //return View(violation);
        }

        // GET: Admin/Violations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations.FindAsync(id);
            if (violation == null)
            {
                return NotFound();
            }
            // Load dropdowns
            ViewData["StudentId"] = new SelectList(_context.TblStudents.OrderBy(s => s.FullName).ToList(), "StudentId", "FullName", violation.StudentId);
            ViewData["RuleId"] = new SelectList(_context.Rules.OrderBy(r => r.RuleName).ToList(), "RuleId", "RuleName", violation.RuleId);

            return View(violation);
        }

        // POST: Admin/Violations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ViolationId,StudentId,RuleId,ViolationDate,Note")] Violation violation)
        {
            if (id != violation.ViolationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Trả lại view nếu dữ liệu nhập sai
                ViewData["StudentId"] = new SelectList(_context.TblStudents.OrderBy(s => s.FullName).ToList(), "StudentId", "FullName", violation.StudentId);
                ViewData["RuleId"] = new SelectList(_context.Rules.OrderBy(r => r.RuleName).ToList(), "RuleId", "RuleName", violation.RuleId);
                return View(violation);

                
            }
            try
                {
                    _context.Update(violation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Violations.Any(e => e.ViolationId == violation.ViolationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //ViewData["RuleId"] = new SelectList(_context.Rules, "RuleId", "RuleId", violation.RuleId);
            //ViewData["StudentId"] = new SelectList(_context.TblStudents, "StudentId", "StudentId", violation.StudentId);
            //return View(violation);
        }

        // GET: Admin/Violations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations
                .Include(v => v.Rule)
                .Include(v => v.Student)
                .FirstOrDefaultAsync(m => m.ViolationId == id);
            if (violation == null)
            {
                return NotFound();
            }

            return View(violation);
        }

        // POST: Admin/Violations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var violation = await _context.Violations.FindAsync(id);
            if (violation != null)
            {
                _context.Violations.Remove(violation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViolationExists(int id)
        {
            return _context.Violations.Any(e => e.ViolationId == id);
        }
    }
}
