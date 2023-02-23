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
    public class detailsController : Controller
    {
        private readonly frushgahContext _context;

        public detailsController(frushgahContext context)
        {
            _context = context;
        }

        // GET: details
        public async Task<IActionResult> Index(Int64? idSubGroup)
        {
            try
            {
                if (idSubGroup != null)
                {
                    HttpContext.Session.SetString("idGroup", idSubGroup.ToString());
                }
                else
                {
                    idSubGroup = Int64.Parse(HttpContext.Session.GetString("idSubGroup"));
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                var q = _context.subGroup.AsNoTracking().Where(s => s.idSubGroup == idSubGroup).FirstOrDefault();
                ViewBag.nameGroup = q.nameSubGroup;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                return View(await _context.detail.Where(s => s.idSubGroup == idSubGroup).ToListAsync());
            }
            catch
            {
                return RedirectToAction("Index", "groups");
            }
        }

        // GET: details/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.detail == null)
            {
                return NotFound();
            }

            var detail = await _context.detail
                .FirstOrDefaultAsync(m => m.idDetail == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // GET: details/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idDetail,idSubGroup,nameDetail,price,pic1,pic2,pic3")] detail detail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detail);
        }

        // GET: details/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.detail == null)
            {
                return NotFound();
            }

            var detail = await _context.detail.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            return View(detail);
        }

        // POST: details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("idDetail,idSubGroup,nameDetail,price,pic1,pic2,pic3")] detail detail)
        {
            if (id != detail.idDetail)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!detailExists(detail.idDetail))
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
            return View(detail);
        }

        // GET: details/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.detail == null)
            {
                return NotFound();
            }

            var detail = await _context.detail
                .FirstOrDefaultAsync(m => m.idDetail == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // POST: details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.detail == null)
            {
                return Problem("Entity set 'frushgahContext.detail'  is null.");
            }
            var detail = await _context.detail.FindAsync(id);
            if (detail != null)
            {
                _context.detail.Remove(detail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool detailExists(long id)
        {
          return _context.detail.Any(e => e.idDetail == id);
        }
    }
}
