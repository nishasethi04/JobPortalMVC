using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;

namespace UI.Controllers
{
    public class CompanyJobController : Controller
    {
        private readonly CareerCloudContext _context;
        private readonly CompanyJobLogic _logic;
        private readonly CompanyProfileLogic _plogic;

        public CompanyJobController(CareerCloudContext context)
        {
            _context = context;
            var repo = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);

            var repos = new EFGenericRepository<CompanyProfilePoco>();
            _plogic = new CompanyProfileLogic(repos);
        }

        // GET: CompanyJob
        public async Task<IActionResult> Index(Guid Id)
        {
            TempData["Id"] = Id;
            var careerCloudContext = _context.CompanyJob.Where(a => a.Company == Id).Include(c => c.CompanyProfile);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyJob/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJob
                .Include(c => c.CompanyProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }

            return View(companyJobPoco);
        }

        // GET: CompanyJob/Create
        public IActionResult Create()
        {
            if (TempData.ContainsKey("Id"))
            {
                Guid id = (Guid)TempData["Id"];
                TempData.Keep();
                ViewData["Companys"] = id.ToString();
                ViewData["Company"] = _context.CompanyProfile.FirstOrDefault().
                    CompanyDescriptions.FirstOrDefault().CompanyName;
            }

            return View();
        }

        // POST: CompanyJob/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco)
        {

            if (ModelState.IsValid)
            {
                if (TempData.ContainsKey("Id"))
                {
                    Guid id = (Guid)TempData["Id"];
                    TempData.Keep();
                    companyJobPoco.Company = id;
                }
                companyJobPoco.Id = Guid.NewGuid();
                _context.Add(companyJobPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = companyJobPoco.Company });
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id", companyJobPoco.Company);
            return View(companyJobPoco);

        }

        // GET: CompanyJob/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJob.FindAsync(id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // POST: CompanyJob/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco)
        {
            if (id != companyJobPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobPocoExists(companyJobPoco.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = companyJobPoco.Company });
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfile, "Id", "Id", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJob
                .Include(c => c.CompanyProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }

            return View(companyJobPoco);
        }

        // POST: CompanyJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobPoco = await _context.CompanyJob.FindAsync(id);
            _context.CompanyJob.Remove(companyJobPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = companyJobPoco.Company });
        }

        private bool CompanyJobPocoExists(Guid id)
        {
            return _context.CompanyJob.Any(e => e.Id == id);
        }
    }
}
