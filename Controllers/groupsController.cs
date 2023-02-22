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
    public class groupsController : Controller
    {
        private readonly frushgahContext _context;

        public groupsController(frushgahContext context)
        {
            _context = context;
        }

        // GET: groups
        public async Task<IActionResult> Index()
        {
            return _context.group != null ?
                        View(await _context.group.ToListAsync()) :
                        Problem("Entity set 'frushgahContext.group'  is null.");
        }

        // GET: groups/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.group == null)
            {
                return NotFound();
            }

            var @group = await _context.group
                .FirstOrDefaultAsync(m => m.id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile pic, [Bind("id,name")] group @group)
        {
            if (pic != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await pic.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 10097152)
                    {
                        group.pic = memoryStream.ToArray();
                        // group.peyvast1_kind_never = Path.GetExtension(peyvast1_never.FileName);
                    }
                    else
                    {
                        ModelState.AddModelError("peyvast1_never", "فایل پیوست اول بزرگتر از 2 مگ میباشد");
                    }
                }
            }
            else
            {
                group.pic = null;
            }


            // if (ModelState.IsValid)
            // {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            // }
            // return View(@group);
        }

        // GET: groups/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.group == null)
            {
                return NotFound();
            }

            var @group = await _context.group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        // POST: groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id,name,pic")] group @group)
        {
            if (id != @group.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!groupExists(@group.id))
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
            return View(@group);
        }

        // GET: groups/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.group == null)
            {
                return NotFound();
            }

            var @group = await _context.group
                .FirstOrDefaultAsync(m => m.id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.group == null)
            {
                return Problem("Entity set 'frushgahContext.group'  is null.");
            }
            var @group = await _context.group.FindAsync(id);
            if (@group != null)
            {
                _context.group.Remove(@group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool groupExists(long id)
        {
            return (_context.group?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
