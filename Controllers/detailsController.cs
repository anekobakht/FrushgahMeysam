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
                    HttpContext.Session.SetString("idSubGroup", idSubGroup.ToString());
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

        public async Task<IActionResult> user1(Int64? idSubGroup)
        {
            try
            {
                if (idSubGroup != null)
                {
                    HttpContext.Session.SetString("idSubGroup", idSubGroup.ToString());
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
            catch(Exception ex)
            {
                return RedirectToAction("Index", "groups");
            }
        }

        public async Task<IActionResult> user2(Int64? idDetail)
        {
            try
            {
                if (idDetail != null)
                {
                    HttpContext.Session.SetString("idDetail", idDetail.ToString());
                }
                else
                {
                    idDetail = Int64.Parse(HttpContext.Session.GetString("idDetail"));
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                var q = _context.detail.AsNoTracking().Where(s => s.idDetail == idDetail).FirstOrDefault();
                ViewBag.nameGroup = q.nameDetail;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                return View(await _context.detail.Where(s => s.idDetail == idDetail).ToListAsync());
            }
            catch
            {
                return RedirectToAction("user1", "detail");
            }
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile pic1,IFormFile pic2,IFormFile pic3,[Bind("tozihat,idDetail,nameDetail,price")] detail detail)
        {

            try
            {
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% pic1
                if (pic1 != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await pic1.CopyToAsync(memoryStream);
                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 10097152)
                        {
                            detail.pic1 = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("peyvast1_never", "فایل پیوست اول بزرگتر از 2 مگ میباشد");
                        }
                    }
                }
                else
                {
                    detail.pic1 = null;
                };
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% pic2
                if (pic2 != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await pic2.CopyToAsync(memoryStream);
                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 10097152)
                        {
                            detail.pic2 = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("peyvast1_never", "فایل پیوست اول بزرگتر از 2 مگ میباشد");
                        }
                    }
                }
                else
                {
                    detail.pic2 = null;
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% pic3
                if (pic3 != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await pic3.CopyToAsync(memoryStream);
                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 10097152)
                        {
                            detail.pic3 = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("peyvast1_never", "فایل پیوست اول بزرگتر از 2 مگ میباشد");
                        }
                    }
                }
                else
                {
                    detail.pic3 = null;
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


                detail.idSubGroup = Int64.Parse(HttpContext.Session.GetString("idSubGroup"));

                _context.Add(detail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.er = "1";
                return View();

            }

        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("tozihat,idDetail,idSubGroup,nameDetail,price,pic1,pic2,pic3")] detail detail)
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
