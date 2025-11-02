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
    public class FloorsController : Controller
    {
        private readonly Cnpm5Context _context;

        public FloorsController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/Floors
        public async Task<IActionResult> Index()
        {
            var cnpm5Context = _context.TblFloors.Include(t => t.Building);
            return View(await cnpm5Context.ToListAsync());
        }

        // GET: Admin/Floors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblFloor = await _context.TblFloors
                .Include(t => t.Building)
                .FirstOrDefaultAsync(m => m.FloorId == id);
            if (tblFloor == null)
            {
                return NotFound();
            }

            return View(tblFloor);
        }

        // GET: Admin/Floors/Create
        public IActionResult Create()
        {
            ViewData["BuildingId"] = new SelectList(_context.TblBuildings, "BuildingId", "Name");
            return View();
        }

        // POST: Admin/Floors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FloorId,BuildingId,FloorNumber")] TblFloor tblFloor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblFloor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new SelectList(_context.TblBuildings, "BuildingId", "Name", tblFloor.BuildingId);
            return View(tblFloor);
        }

        // GET: Admin/Floors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblFloor = await _context.TblFloors.FindAsync(id);
            if (tblFloor == null)
            {
                return NotFound();
            }
            ViewData["BuildingId"] = new SelectList(_context.TblBuildings, "BuildingId", "Name", tblFloor.BuildingId);
            return View(tblFloor);
        }

        // POST: Admin/Floors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FloorId,BuildingId,FloorNumber")] TblFloor tblFloor)
        {
            if (id != tblFloor.FloorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblFloor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblFloorExists(tblFloor.FloorId))
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
            ViewData["BuildingId"] = new SelectList(_context.TblBuildings, "BuildingId", "Name", tblFloor.BuildingId);
            return View(tblFloor);
        }

        // GET: Admin/Floors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblFloor = await _context.TblFloors
                .Include(t => t.Building)
                .FirstOrDefaultAsync(m => m.FloorId == id);
            if (tblFloor == null)
            {
                return NotFound();
            }

            return View(tblFloor);
        }

        // POST: Admin/Floors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblFloor = await _context.TblFloors.FindAsync(id);
            if (tblFloor != null)
            {
                _context.TblFloors.Remove(tblFloor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblFloorExists(int id)
        {
            return _context.TblFloors.Any(e => e.FloorId == id);
        }
    }
}
