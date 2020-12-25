using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db) 
        {
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
           // var data = _db.ProductTypes.ToList();
            return View(_db.ProductTypes.ToList());
        }

        // Get Create action Method
     
        public ActionResult Create() 
        {
            return View();
        }

        // Create Post Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }


        //Get Edit action Method
      
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Post Edit  Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }


        //Get Edit action Method

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Post Edit  Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        
        }

        //Get Delete action Method

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Post Delete  Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (id!=productTypes.Id)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }

    }
}
