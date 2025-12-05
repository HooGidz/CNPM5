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
    public class RulesController : Controller
    {
        private readonly Cnpm5Context _context;

        public RulesController(Cnpm5Context context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rules.Include(r => r.Account).ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rule = await _context.Rules.Include(r => r.Account)
                .FirstOrDefaultAsync(m => m.RuleId == id);
            if (rule == null)
            {
                return NotFound();
            }

            return View(rule);
        }

        
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.TblAccounts.OrderBy(s => s.FullName).ToList(), "AccountId", "FullName");
            return View(new Rule());
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RuleId,RuleName,Description,Penalty,EffectiveDate,Status,AccountId,CreateDate")] Rule rule)
        {
            if (ModelState.IsValid)
            {
               ViewData["AccountId"] = new SelectList(_context.TblAccounts.OrderBy(s => s.FullName).ToList(), "AccountId", "FullName", rule.AccountId);
               return View(rule);
                
            }
            _context.Add(rule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rule = await _context.Rules.FindAsync(id);
            if (rule == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.TblAccounts.OrderBy(s => s.FullName).ToList(), "AccountId", "FullName", rule.AccountId);
            return View(rule);
        }

        // POST: Admin/Rules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RuleId,RuleName,Description,Penalty,EffectiveDate,Status,AccountId,CreatedDate")] Rule rule)
        {
            if (id != rule.RuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ViewData["AccountId"] = new SelectList(_context.TblAccounts.OrderBy(s => s.FullName).ToList(), "AccountId", "FullName", rule.AccountId);
            }
                try
                {
                    _context.Update(rule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RuleExists(rule.RuleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
        
            //return View(rule);
        }

        // GET: Admin/Rules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rule = await _context.Rules
                .FirstOrDefaultAsync(m => m.RuleId == id);
            if (rule == null)
            {
                return NotFound();
            }

            return View(rule);
        }

        // POST: Admin/Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rule = await _context.Rules.FindAsync(id);
            if (rule != null)
            {
                _context.Rules.Remove(rule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RuleExists(int id)
        {
            return _context.Rules.Any(e => e.RuleId == id);
        }
    }
}
