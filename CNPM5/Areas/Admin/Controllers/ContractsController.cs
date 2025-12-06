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
    public class ContractsController : Controller
    {
        private readonly Cnpm5Context _context;

        public ContractsController(Cnpm5Context context)
        {
            _context = context;
        }

        // GET: Admin/Contracts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contracts
                .Include(v=>v.Student)
                .Include(v=>v.Room)
                .Include(v=>v.Service)
                
                .ToListAsync());
        }

        // GET: Admin/Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.Include(v => v.Student)
                .Include(v => v.Room).Include(v => v.Service)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Admin/Contracts/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.TblStudentss.OrderBy(s => s.FullName).ToList(), "StudentId", "FullName");
            ViewData["RoomId"] = new SelectList(_context.TblRooms.OrderBy(r => r.RoomName).ToList(), "RoomId", "RoomName");
            ViewData["ServiceId"] = new SelectList(_context.TblServices.OrderBy(r => r.Name).ToList(), "ServiceId", "Name");
            
            return View(new Contract());
        }

        // POST: Admin/Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,StudentId,RoomId,ServiceId,StartDate,EndDate,Deposit,MonthlyFee,Cycle,Harvestday,Status,CreatedDate,CreatedBy")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                ViewData["StudentId"] = new SelectList(_context.TblStudentss.OrderBy(s => s.FullName).ToList(), "StudentId", "FullName", contract.StudentId);
                ViewData["RoomId"] = new SelectList(_context.TblRooms.OrderBy(r => r.RoomName).ToList(), "RoomId", "RoomName", contract.RoomId);
                ViewData["ServiceId"] = new SelectList(_context.TblServices.OrderBy(r => r.Name).ToList(), "ServiceId", "Name", contract.ServiceId);
                
                return View(contract);
                
            }
                contract.CreatedDate = DateTime.Now;
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.TblStudentss.OrderBy(s => s.FullName).ToList(), "StudentId", "FullName", contract.StudentId);
            ViewData["RoomId"] = new SelectList(_context.TblRooms.OrderBy(r => r.RoomName).ToList(), "RoomId", "RoomName", contract.RoomId);
            ViewData["ServiceId"] = new SelectList(_context.TblServices.OrderBy(r => r.Name).ToList(), "ServiceId", "Name", contract.ServiceId);
            
            return View(contract);
        }

        // POST: Admin/Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,StudentId,RoomId,ServiceId,StartDate,EndDate,Deposit,MonthlyFee,Cycle,Harvestday,Status,CreatedDate,CreatedBy")] Contract contract)
        {
            if (id != contract.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ViewData["StudentId"] = new SelectList(_context.TblStudentss.OrderBy(s => s.FullName).ToList(), "StudentId", "FullName", contract.StudentId);
                ViewData["RoomId"] = new SelectList(_context.TblRooms.OrderBy(r => r.RoomName).ToList(), "RoomId", "RoomName", contract.RoomId);
                ViewData["ServiceId"] = new SelectList(_context.TblServices.OrderBy(r => r.Name).ToList(), "ServiceId", "Name", contract.ServiceId);
                
                return View(contract); 
            }
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ContractId))
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
            //return View(contract);
        

        // GET: Admin/Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(v => v.Student)
                .Include(v => v.Room)
                .Include(v => v.Service)
                
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Admin/Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractId == id);
        }
    }
}
