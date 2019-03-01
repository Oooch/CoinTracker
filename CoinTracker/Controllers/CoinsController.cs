using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoinTracker.Data;
using CoinTracker.Models;

namespace CoinTracker.Controllers
{
    public class CoinsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoinsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coins
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["PairSortParam"] = String.IsNullOrEmpty(sortOrder) ? "pair_desc" : "";
            ViewData["ChangeSortParam"] = sortOrder == "Change" ? "change_desc" : "Change";
            ViewData["VolumeSortParam"] = sortOrder == "Volume" ? "volume_desc" : "Volume";
            ViewData["CurrentFilter"] = searchString;
            var result = from s in _context.Coin
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(o => o.Pair.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "pair_desc":
                    result = result.OrderByDescending(o => o.Pair);
                    break;
                case "Change":
                    result = result.OrderBy(o => o.PriceChange);
                    break;
                case "change_desc":
                    result = result.OrderByDescending(o => o.PriceChange);
                    break;
                case "Volume":
                    result = result.OrderBy(o => o.Volume);
                    break;
                case "volume_desc":
                    result = result.OrderByDescending(o => o.Volume);
                    break;
                default:
                    result = result.OrderBy(o => o.Pair);
                    break;
            }
            
            return View(await result.AsNoTracking().ToListAsync());
        }

        // GET: Coins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coin = await _context.Coin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coin == null)
            {
                return NotFound();
            }

            return View(coin);
        }

        private bool CoinExists(int id)
        {
            return _context.Coin.Any(e => e.Id == id);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Coins/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Coins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Exchange,Pair,CurrentPrice,PriceChange,Volume")] Coin coin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(coin);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(coin);
        //}

        // GET: Coins/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var coin = await _context.Coin.FindAsync(id);
        //    if (coin == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(coin);
        //}

        // POST: Coins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Exchange,Pair,CurrentPrice,PriceChange,Volume")] Coin coin)
        //{
        //    if (id != coin.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(coin);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CoinExists(coin.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(coin);
        //}

        // GET: Coins/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var coin = await _context.Coin
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (coin == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(coin);
        //}

        // POST: Coins/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var coin = await _context.Coin.FindAsync(id);
        //    _context.Coin.Remove(coin);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
