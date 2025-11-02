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
    public class BuildingsController : Controller
    {
        private readonly Cnpm5Context _context;

        public BuildingsController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/Buildings
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblBuildings.ToListAsync());
        }

        // GET: Admin/Buildings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBuilding = await _context.TblBuildings
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            if (tblBuilding == null)
            {
                return NotFound();
            }

            return View(tblBuilding);
        }

        // GET: Admin/Buildings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TotalFloors,TotalRooms")] TblBuilding tblBuilding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblBuilding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblBuilding);
        }

        // GET: Admin/Buildings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBuilding = await _context.TblBuildings.FindAsync(id);
            if (tblBuilding == null)
            {
                return NotFound();
            }
            return View(tblBuilding);
        }

        // POST: Admin/Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TotalFloors,TotalRooms")] TblBuilding tblBuilding)
        {
            if (id != tblBuilding.BuildingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblBuilding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBuildingExists(tblBuilding.BuildingId))
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
            return View(tblBuilding);
        }

        // GET: Admin/Buildings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBuilding = await _context.TblBuildings
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            if (tblBuilding == null)
            {
                return NotFound();
            }

            return View(tblBuilding);
        }

        // POST: Admin/Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblBuilding = await _context.TblBuildings.FindAsync(id);
            if (tblBuilding != null)
            {
                _context.TblBuildings.Remove(tblBuilding);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblBuildingExists(int id)
        {
            return _context.TblBuildings.Any(e => e.BuildingId == id);
        }
    }
}
