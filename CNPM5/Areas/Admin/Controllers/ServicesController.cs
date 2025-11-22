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
    public class ServicesController : Controller
    {
        private readonly Cnpm5Context _context;

        public ServicesController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblServices.ToListAsync());
        }

        // GET: Admin/Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblService = await _context.TblServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (tblService == null)
            {
                return NotFound();
            }

            return View(tblService);
        }

        // GET: Admin/Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,Name,Unit,Price,CreatedAt,UpdatedAt")] TblService tblService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblService);
        }

        // GET: Admin/Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblService = await _context.TblServices.FindAsync(id);
            if (tblService == null)
            {
                return NotFound();
            }
            return View(tblService);
        }

        // POST: Admin/Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,Name,Unit,Price,CreatedAt,UpdatedAt")] TblService tblService)
        {
            if (id != tblService.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblService.UpdatedAt = DateTime.Now;
                    _context.Update(tblService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblServiceExists(tblService.ServiceId))
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
            return View(tblService);
        }

        // GET: Admin/Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblService = await _context.TblServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (tblService == null)
            {
                return NotFound();
            }

            return View(tblService);
        }

        // POST: Admin/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblService = await _context.TblServices.FindAsync(id);
            if (tblService != null)
            {
                _context.TblServices.Remove(tblService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblServiceExists(int id)
        {
            return _context.TblServices.Any(e => e.ServiceId == id);
        }
    }
}
