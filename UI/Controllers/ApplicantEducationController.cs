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
    public class ApplicantEducationController : Controller
    {

         readonly CareerCloudContext _context;
        public ApplicantEducationController(CareerCloudContext context)
        {
            _context = context;
        }
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationController()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }

        // GET: ApplicantEducation
        public async Task<IActionResult> Index(Guid Id)
        {
            //var poco = _logic.GetAll().Where(a => a.Applicant == Id);
            //return View(await poco.ToListAsync());

            var careerCloudContext = _context.ApplicationEducations.Where(a => a.Applicant == Id);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: ApplicantEducation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantEducationPoco = await _context.ApplicationEducations
                .Include(a => a.ApplicantProfilePoco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantEducationPoco == null)
            {
                return NotFound();
            }

            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id");
            return View();
        }

        // POST: ApplicantEducation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent,TimeStamp")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                applicantEducationPoco.Id = Guid.NewGuid();
                _context.Add(applicantEducationPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantEducationPoco = await _context.ApplicationEducations.FindAsync(id);
            if (applicantEducationPoco == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // POST: ApplicantEducation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent,TimeStamp")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (id != applicantEducationPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantEducationPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantEducationPocoExists(applicantEducationPoco.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantEducationPoco = await _context.ApplicationEducations
                .Include(a => a.ApplicantProfilePoco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantEducationPoco == null)
            {
                return NotFound();
            }

            return View(applicantEducationPoco);
        }

        // POST: ApplicantEducation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantEducationPoco = await _context.ApplicationEducations.FindAsync(id);
            _context.ApplicationEducations.Remove(applicantEducationPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantEducationPocoExists(Guid id)
        {
            return _context.ApplicationEducations.Any(e => e.Id == id);
        }
    }
}
