using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {
        private readonly ILesseeRepository _lesseesRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public LesseesController(
            ILesseeRepository lesseeRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _lesseesRepository = lesseeRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: lessee
        public IActionResult Index()
        {
            return View(_lesseesRepository.GetAll().OrderBy(o => o.Document).ToList());
        }

        // GET: lessee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseesRepository.GetByIdAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // GET: Lessee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LesseeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "lessees");
                }

                var lessee = _converterHelper.ToLessee(model, path, true);
                lessee.User = await _userHelper.GetUserByEmailAsync("luispatricio.info@gmail.com");
                await _lesseesRepository.CreateAsync(lessee);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Lessee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseesRepository.GetByIdAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }
            var model = _converterHelper.ToLesseeViewModel(lessee);
            return View(model);
        }

        // POST: Lessee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LesseeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "lessees");
                    }

                    var lessee = _converterHelper.ToLessee(model, path, false);

                    lessee.User = await _userHelper.GetUserByEmailAsync("luispatricio.info@gmail.com");
                    await _lesseesRepository.UpdateAsync(lessee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _lesseesRepository.ExistAsync(model.Id))
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
            return View(model);
        }

        // GET: Lessees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseesRepository.GetByIdAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // POST: lessee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessee = await _lesseesRepository.GetByIdAsync(id);
            await _lesseesRepository.DeleteAsync(lessee);
            return RedirectToAction(nameof(Index));
        }
    }
}
