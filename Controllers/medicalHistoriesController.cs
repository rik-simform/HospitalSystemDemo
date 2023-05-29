using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Context;
using HospitalManagementSystem.Models;

namespace HospitalManagementDemo.Controllers
{
    public class medicalHistoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        public medicalHistoriesController(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: medicalHistories
        public async Task<IActionResult> Index()
        {
            try
            {
                var appDbContext = _context.MedicalHistories.Include(m => m.patients);
                return View(await appDbContext.ToListAsync());
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception Occured: " +ex.Message);
                throw ex;
            }
            
        }

        // GET: medicalHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null || _context.MedicalHistories == null)
                {
                    return NotFound();
                }

                var medicalHistory = await _context.MedicalHistories
                    .Include(m => m.patients)
                    .FirstOrDefaultAsync(m => m.medicalId == id);
                if (medicalHistory == null)
                {
                    return NotFound();
                }

                return View(medicalHistory);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured: " + ex.Message);
                throw ex;
            }
           
        }

        // GET: medicalHistories/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["patientId"] = new SelectList(_context.Patients, "patientId", "patientName");
                return View();
            }
            catch(Exception e)
            {
                _logger.LogError("Exception Occured: " + e.Message);
                throw e;
            }
            
        }

        // POST: medicalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("medicalId,disease,diseaseDescription,testDetails,patientId")] medicalHistory medicalHistory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(medicalHistory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["patientId"] = new SelectList(_context.Patients, "patientId", "bloodGroup", medicalHistory.patientId);
                return View(medicalHistory);
            }
            catch(Exception e)
            {
                _logger.LogError("Exception Occured: " + e.Message);
                throw e;
            }
            
        }

        // GET: medicalHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || _context.MedicalHistories == null)
                {
                    return NotFound();
                }

                var medicalHistory = await _context.MedicalHistories.FindAsync(id);
                if (medicalHistory == null)
                {
                    return NotFound();
                }
                ViewData["patientId"] = new SelectList(_context.Patients, "patientId", "bloodGroup", medicalHistory.patientId);
                return View(medicalHistory);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured: " + e.Message);
                throw e;
            }
            
        }

        // POST: medicalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("medicalId,disease,diseaseDescription,testDetails,patientId")] medicalHistory medicalHistory)
        {
            try
            {
                if (id != medicalHistory.medicalId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(medicalHistory);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!medicalHistoryExists(medicalHistory.medicalId))
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
                ViewData["patientId"] = new SelectList(_context.Patients, "patientId", "bloodGroup", medicalHistory.patientId);
                return View(medicalHistory);
            }
            catch(Exception e)
            {
                _logger.LogError("Exception Occured: " + e.Message);
                throw e;
            }
            
        }

        // GET: medicalHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || _context.MedicalHistories == null)
                {
                    return NotFound();
                }

                var medicalHistory = await _context.MedicalHistories
                    .Include(m => m.patients)
                    .FirstOrDefaultAsync(m => m.medicalId == id);
                if (medicalHistory == null)
                {
                    return NotFound();
                }

                return View(medicalHistory);
            }
            catch(Exception e)
            {
                _logger.LogError("Exception Occured: " + e.Message);
                throw;
            }
            
        }

        // POST: medicalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.MedicalHistories == null)
                {
                    return Problem("Entity set 'AppDbContext.MedicalHistories'  is null.");
                }
                var medicalHistory = await _context.MedicalHistories.FindAsync(id);
                if (medicalHistory != null)
                {
                    _context.MedicalHistories.Remove(medicalHistory);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured: " + e.Message);
                throw;
            }
            
        }

        private bool medicalHistoryExists(int id)
        {
            try
            {
                return (_context.MedicalHistories?.Any(e => e.medicalId == id)).GetValueOrDefault();
            }
            catch(Exception e)
            {
                _logger.LogError("Exception Occured: " + e.Message);
                return false;
            }

        }
    }
}
