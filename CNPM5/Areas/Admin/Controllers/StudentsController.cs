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
    public class StudentsController : Controller
    {
        private readonly Cnpm5Context _context;

        public StudentsController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/Students
        public async Task<IActionResult> Index()
        {
            var cnpm5Context = _context.TblStudents.Include(t => t.Account);
            return View(await cnpm5Context.ToListAsync());
        }

        // GET: Admin/Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents
                .Include(t => t.Account)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            return View(tblStudent);
        }

        // GET: Admin/Students/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.TblAccounts, "AccountId", "AccountId");
            return View();
        }

        // POST: Admin/Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentCode,FullName,Gender,BirthDate,Phone,Email,Faculty,Major,CitizenId,PermanentAddress,TemporaryAddress,AvatarUrl,StudentStatus,EmergencyContactName,EmergencyContactPhone,CreatedAt,UpdatedAt,AccountId")] TblStudent tblStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.TblAccounts, "AccountId", "AccountId", tblStudent.AccountId);
            return View(tblStudent);
        }

        // GET: Admin/Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents.FindAsync(id);
            if (tblStudent == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.TblAccounts, "AccountId", "AccountId", tblStudent.AccountId);
            return View(tblStudent);
        }

        // POST: Admin/Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentCode,FullName,Gender,BirthDate,Phone,Email,Faculty,Major,CitizenId,PermanentAddress,TemporaryAddress,AvatarUrl,StudentStatus,EmergencyContactName,EmergencyContactPhone,CreatedAt,UpdatedAt,AccountId")] TblStudent tblStudent)
        {
            if (id != tblStudent.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblStudentExists(tblStudent.StudentId))
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
            ViewData["AccountId"] = new SelectList(_context.TblAccounts, "AccountId", "AccountId", tblStudent.AccountId);
            return View(tblStudent);
        }

        // GET: Admin/Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents
                .Include(t => t.Account)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            return View(tblStudent);
        }

        // POST: Admin/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblStudent = await _context.TblStudents.FindAsync(id);
            if (tblStudent != null)
            {
                _context.TblStudents.Remove(tblStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblStudentExists(int id)
        {
            return _context.TblStudents.Any(e => e.StudentId == id);
        }
    }
}
