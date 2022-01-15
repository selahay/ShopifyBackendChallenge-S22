using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopifyBackendChallenge.Models;

/// <summary>
/// Controller responsible for the CRUD actions and the delete/undo delete
/// </summary>
namespace ShopifyBackendChallenge.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly InventoryContext _context;

        public InventoriesController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index() //rendering Index (all items) page
        {

            return View(await _context.Inventory.Where(m => m.IsDeleted == false).ToListAsync()); //only displaying items that are not marked as deleted
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id) //page displays selected inventory item and the details
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create() //executes when user clicks add new item
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,InStock")] Inventory inventory) //post method for creating new item
        {
            if (ModelState.IsValid) //checking validations
            {
                _context.Add(inventory); //adding item to database
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            return View(inventory); //returning to index page and displaying updated list
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id) //when edit is pressed
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FindAsync(id); //finding id in database
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,InStock")] Inventory inventory) //post method for edit
        {
            if (id != inventory.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid) //if passes validations
            {
                try
                {
                    _context.Update(inventory); //updating item in database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); //returning to main page
            }
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id) //when delete clicked
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.ItemId == id); //finds item in database

            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) //post method for delete - using a "soft" delete so that the action can be reversed
        {
            var inventory = await _context.Inventory.FindAsync(id); //finds id
            inventory.IsDeleted = true; //marks item as deleted
            TempData["itemName"] = inventory.ItemName; //storing id and name so a temp data message can be shown, allowing user to temporarily undo their delete
            TempData["itemId"] = inventory.ItemId;
            _context.Inventory.Update(inventory);
            await _context.SaveChangesAsync(); //saving changes
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Undelete(int id) //executes when user clicks "undo"
        {
            Inventory inventory = _context.Inventory.Find(id); //finds database item
            inventory.IsDeleted = false; //marks is deleted as false
            _context.Inventory.Update(inventory); //updating database
            _context.SaveChanges();
            return RedirectToAction("Index", "Inventories"); //redirecting back to main page
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.ItemId == id);
        }
    }
}
