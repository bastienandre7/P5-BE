using ExpressVoitureApp.Data;
using ExpressVoitureApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using System.Drawing.Drawing2D;

namespace ExpressVoitureApp.Controllers
{
    public class CarController : Controller
    {
        ApplicationDbContext _MyContext;
        IWebHostEnvironment _WebHostEnvironment;
        public CarController(ApplicationDbContext myContext, IWebHostEnvironment webHostEnvironment)
        {
            _MyContext = myContext;
            _WebHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var cars = _MyContext.Cars.Include(c => c.Finance);
            return View(cars.ToList());
        }
        public List<Car> GetCar()
        {
            var cars = _MyContext.Cars.ToList();
            return cars;
        }
        public List<Finance> GetFinance()
        {
            var finances = _MyContext.Finances.ToList();
            return finances;
        }
        public List<Repair> GetRepair()
        {
            var repair = _MyContext.Repairs.ToList();
            return repair;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddCar()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCar(CarViewModel carvm)
        {
            string fileName = "";
            if (carvm.Photo != null)
            {
                string uploadfolder = Path.Combine(_WebHostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + carvm.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, fileName);
                carvm.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            Car car = new Car
            {
                VINCode = carvm.VINCode,
                Year = carvm.Year,
                Brand = carvm.Brand,
                Model = carvm.Model,
                Finishing = carvm.Finishing,
                Availability = carvm.Availability,
                AvailabilityDate = carvm.AvailabilityDate,
                Image = fileName,
                Description = carvm.Description,
            };
            _MyContext.Cars.Add(car);
            await _MyContext.SaveChangesAsync();
            Finance finance = new Finance
            {
                PurchaseDate = carvm.PurchaseDate,
                PurchaseAmount = carvm.PurchaseAmount,
                Price = carvm.Price,
                SaleDate = carvm.SaleDate,
                CarId = car.Id
            };
            Repair repair = new Repair
            {
                RepairsName = carvm.RepairsName,
                RepairsCost = carvm.RepairsCost,
                CarId = car.Id
            };
            
            _MyContext.Finances.Add(finance);
            _MyContext.Repairs.Add(repair);
            _MyContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _MyContext.Cars == null)
            {
                return NotFound();
            }

            var cars = await _MyContext.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cars == null)
            {
                return NotFound();
            }
            
            return View(cars);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_MyContext.Cars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }
            var cars = await _MyContext.Cars.FindAsync(id);
            var finance = await _MyContext.Finances.FindAsync(id);
            var repair = await _MyContext.Repairs.FindAsync(id);
            if (cars != null)
            {
                _MyContext.Cars.Remove(cars);
            }

            await _MyContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, CarViewModel carvm)
        {
            if (id == null || _MyContext.Cars == null)
            {
                return NotFound();
            }

            var cars = await _MyContext.Cars.FindAsync(id);
            if (cars == null)
            {
                return NotFound();
            }
            return View("Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CarViewModel carvm)
        {
            if (carvm.Photo == null || carvm.Photo.Length == 0)
            {
                return Content("File not selected");
            }
            //Save The Picture In folder
            string fileName = "";
            if (carvm.Photo != null)
            {
                string uploadfolder = Path.Combine(_WebHostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + carvm.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, fileName);
                carvm.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            //Finding the member by its Id which we would update
            var objMember = _MyContext.Cars.Where(mId => mId.Id == carvm.Id).FirstOrDefault();

            if (objMember != null)
            {
                //Update the existing member with new value
                objMember!.Availability =carvm.Availability;
                objMember!.Image = fileName;
                objMember!.Description = carvm.Description;

                await _MyContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
