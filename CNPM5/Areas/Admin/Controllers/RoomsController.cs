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
    public class RoomsController : Controller
    {
        private readonly Cnpm5Context _context;

        public RoomsController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/Rooms
        public async Task<IActionResult> Index()
        {
            var cnpm5Context = _context.TblRooms.Include(t => t.Floor);
            return View(await cnpm5Context.ToListAsync());
        }

        // GET: Admin/Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoom = await _context.TblRooms
                .Include(t => t.Floor)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (tblRoom == null)
            {
                return NotFound();
            }

            return View(tblRoom);
        }

        // GET: Admin/Rooms/Create
        public IActionResult Create()
        {
            ViewData["FloorId"] = new SelectList(_context.TblFloors, "FloorId", "FloorId");
            return View();
        }

        // POST: Admin/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,FloorId,RoomName,Capacity,CurrentOccupants,Gender,RoomType,Price,Status")] TblRoom tblRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FloorId"] = new SelectList(_context.TblFloors, "FloorId", "FloorId", tblRoom.FloorId);
            return View(tblRoom);
        }

        // GET: Admin/Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoom = await _context.TblRooms.FindAsync(id);
            if (tblRoom == null)
            {
                return NotFound();
            }
            ViewData["FloorId"] = new SelectList(_context.TblFloors, "FloorId", "FloorId", tblRoom.FloorId);
            return View(tblRoom);
        }

        // POST: Admin/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,FloorId,RoomName,Capacity,CurrentOccupants,Gender,RoomType,Price,Status")] TblRoom tblRoom)
        {
            if (id != tblRoom.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblRoomExists(tblRoom.RoomId))
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
            ViewData["FloorId"] = new SelectList(_context.TblFloors, "FloorId", "FloorId", tblRoom.FloorId);
            return View(tblRoom);
        }

        // GET: Admin/Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoom = await _context.TblRooms
                .Include(t => t.Floor)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (tblRoom == null)
            {
                return NotFound();
            }

            return View(tblRoom);
        }

        // POST: Admin/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblRoom = await _context.TblRooms.FindAsync(id);
            if (tblRoom != null)
            {
                _context.TblRooms.Remove(tblRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblRoomExists(int id)
        {
            return _context.TblRooms.Any(e => e.RoomId == id);
        }
    }
}
