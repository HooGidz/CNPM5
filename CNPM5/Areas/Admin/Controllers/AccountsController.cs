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
    public class AccountsController : Controller
    {
        private readonly Cnpm5Context _context;

        public AccountsController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblAccounts.ToListAsync());
        }

        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.TblAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (tblAccount == null)
            {
                return NotFound();
            }

            return View(tblAccount);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Username,Password,FullName,Phone,Email,RoleId,LastLogin,CreatedDate,Status")] TblAccount tblAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblAccount);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.TblAccounts.FindAsync(id);
            if (tblAccount == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleName", tblAccount.RoleId);
            return View(tblAccount);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Username,Password,FullName,Phone,Email,RoleId,LastLogin,CreatedDate,Status")] TblAccount tblAccount)
        {
            if (id != tblAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblAccountExists(tblAccount.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleName", tblAccount.RoleId);
            return View(tblAccount);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.TblAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (tblAccount == null)
            {
                return NotFound();
            }

            return View(tblAccount);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblAccount = await _context.TblAccounts.FindAsync(id);
            if (tblAccount != null)
            {
                _context.TblAccounts.Remove(tblAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblAccountExists(int id)
        {
            return _context.TblAccounts.Any(e => e.AccountId == id);
        }
    }
}
