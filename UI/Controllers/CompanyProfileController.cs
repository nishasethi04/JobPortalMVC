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
    public class CompanyProfileController : Controller
    {
        private readonly CareerCloudContext _context;


        private readonly CompanyProfileLogic _logic;

        public CompanyProfileController(CareerCloudContext context)
        {
            _context = context;
            var repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }
        // GET: CompanyProfile
        public IActionResult Index(string SearchString)
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                var pocos = _context.CompanyProfile.Where(c => c.ContactName.Contains(SearchString)
                || c.CompanyWebsite.Contains(SearchString));
                return View(pocos);
            }
            else
            {
                var poco = _logic.GetAll();
                if (poco == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(poco);
                }
            }
          
        }

        // GET: CompanyProfile/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfilePoco = await _context.CompanyProfile
              
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }

            return View(companyProfilePoco);
        }

        // GET: CompanyProfile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,RegistrationDate,CompanyWebsite,ContactPhone,ContactName,CompanyLogo,TimeStamp")] CompanyProfilePoco companyProfilePoco)
        {
            if (ModelState.IsValid)
            {
                //companyProfilePoco.Id = Guid.NewGuid();
                //_context.Add(companyProfilePoco);
                //await _context.SaveChangesAsync();

                companyProfilePoco.Id = Guid.NewGuid();
                _logic.Add(new CompanyProfilePoco[] { companyProfilePoco });
                return RedirectToAction(nameof(Index));
            }
            return View(companyProfilePoco);
        }

        // GET: CompanyProfile/Edit/5
        public IActionResult Edit(Guid id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            CompanyProfilePoco companyProfilePoco = _logic.Get(id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }
            return View(companyProfilePoco);
        }

        // POST: CompanyProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,RegistrationDate,CompanyWebsite,ContactPhone,ContactName,CompanyLogo,TimeStamp")] CompanyProfilePoco companyProfilePoco)
        {
           
            if (ModelState.IsValid)
            {
                CompanyProfilePoco[] companyProfilePocos = new CompanyProfilePoco[1];
                companyProfilePocos[0] = companyProfilePoco;
                _logic.Update(companyProfilePocos);

                return RedirectToAction("Index");
            }
            return View(companyProfilePoco);
        }

        // GET: CompanyProfile/Delete/5
        public IActionResult Delete(Guid id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            CompanyProfilePoco companyProfilePoco = _logic.Get(id);  // db.CompanyProfile.Find(id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }
            return View(companyProfilePoco);
        }

        // POST: CompanyProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
           
            CompanyProfilePoco companyProfilePoco = _logic.Get(id);  
            _logic.Delete(new CompanyProfilePoco[] { companyProfilePoco });
            return RedirectToAction("Index");
        }

        private bool CompanyProfilePocoExists(Guid id)
        {
            return _context.CompanyProfile.Any(e => e.Id == id);
        }
    }
}
