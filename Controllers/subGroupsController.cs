using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using frushgah.Data;
using frushgah.Models;

namespace frushgah.Controllers
{
    public class subGroupsController : Controller
    {
        private readonly frushgahContext _context;

        public subGroupsController(frushgahContext context)
        {
            _context = context;
        }

        // GET: subGroups
        public async Task<IActionResult> Index(Int64 idGroup)
        {
            HttpContext.Session.SetString("idGroup", idGroup.ToString());
            return View(await _context.subGroup.Where(s => s.idGroup == idGroup).ToListAsync());
        }

        // GET: subGroups/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.subGroup == null)
            {
                return NotFound();
            }

            var subGroup = await _context.subGroup
                .FirstOrDefaultAsync(m => m.idSupGroup == id);
            if (subGroup == null)
            {
                return NotFound();
            }

            return View(subGroup);
        }

        // GET: subGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: subGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idSupGroup,idGroup,nameSupGroup,picSupGroup")] subGroup subGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subGroup);
        }

        // GET: subGroups/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.subGroup == null)
            {
                return NotFound();
            }

            var subGroup = await _context.subGroup.FindAsync(id);
            if (subGroup == null)
            {
                return NotFound();
            }
            return View(subGroup);
        }

        // POST: subGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("idSupGroup,idGroup,nameSupGroup,picSupGroup")] subGroup subGroup)
        {
            if (id != subGroup.idSupGroup)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!subGroupExists(subGroup.idSupGroup))
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
            return View(subGroup);
        }

        // GET: subGroups/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.subGroup == null)
            {
                return NotFound();
            }

            var subGroup = await _context.subGroup
                .FirstOrDefaultAsync(m => m.idSupGroup == id);
            if (subGroup == null)
            {
                return NotFound();
            }

            return View(subGroup);
        }

        // POST: subGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.subGroup == null)
            {
                return Problem("Entity set 'frushgahContext.subGroup'  is null.");
            }
            var subGroup = await _context.subGroup.FindAsync(id);
            if (subGroup != null)
            {
                _context.subGroup.Remove(subGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool subGroupExists(long id)
        {
          return _context.subGroup.Any(e => e.idSupGroup == id);
        }
    }
}
