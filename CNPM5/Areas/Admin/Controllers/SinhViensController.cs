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
    public class SinhViensController : Controller
    {
        private readonly Cnpm5Context _context;

        public SinhViensController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/SinhViens
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblSinhViens.ToListAsync());
        }

        // GET: Admin/SinhViens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSinhVien = await _context.TblSinhViens
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (tblSinhVien == null)
            {
                return NotFound();
            }

            return View(tblSinhVien);
        }

        // GET: Admin/SinhViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SinhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSV,HoTen,GioiTinh,NgaySinh,Khoa,Lop,SDT,Email,DiaChi,TrangThai")] TblSinhVien tblSinhVien)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(tblSinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblSinhVien);
        }

        // GET: Admin/SinhViens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSinhVien = await _context.TblSinhViens.FindAsync(id);
            if (tblSinhVien == null)
            {
                return NotFound();
            }
            return View(tblSinhVien);
        }

        // POST: Admin/SinhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSV,HoTen,GioiTinh,NgaySinh,Khoa,Lop,SDT,Email,DiaChi,TrangThai")] TblSinhVien tblSinhVien)
        {
            if (id != tblSinhVien.MaSV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblSinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSinhVienExists(tblSinhVien.MaSV))
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
            return View(tblSinhVien);
        }

        // GET: Admin/SinhViens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSinhVien = await _context.TblSinhViens
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (tblSinhVien == null)
            {
                return NotFound();
            }

            return View(tblSinhVien);
        }

        // POST: Admin/SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tblSinhVien = await _context.TblSinhViens.FindAsync(id);
            if (tblSinhVien != null)
            {
                _context.TblSinhViens.Remove(tblSinhVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSinhVienExists(string id)
        {
            return _context.TblSinhViens.Any(e => e.MaSV == id);
        }
    }
}
