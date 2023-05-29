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
    public class patientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        public patientsController(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: patients
        public async Task<IActionResult> Index()
        {
            try
            {
                return _context.Patients != null ?
                          View(await _context.Patients.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Patients'  is null.");
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception Occured: " + ex.Message);
            }
            return null;
        }

        // GET: patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null || _context.Patients == null)
                {
                    return NotFound();
                }

                var patient = await _context.Patients
                    .FirstOrDefaultAsync(m => m.patientId == id);
                if (patient == null)
                {
                    return NotFound();
                }

                return View(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured: " + ex.Message);
            }
            return null;
        }

        // GET: patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("patientId,patientName,age,gender,bloodGroup,contactNo,emergencyContact")] patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch(Exception ex) 
            {
                _logger.LogError("Exception Occured: " + ex.Message);
            }
            return View(patient);
        }

        // GET: patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || _context.Patients == null)
                {
                    return NotFound();
                }

                var patients = await _context.Patients.FindAsync(id);
                if (patients == null)
                {
                    return NotFound();
                }
                return View(patients);
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception Occured: " + ex.Message);
            }
            return null;
        }

        // POST: patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("patientId,patientName,age,gender,bloodGroup,contactNo,emergencyContact")] patient patient)
        {
            try
            {
                if (id != patient.patientId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(patient);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!patientExists(patient.patientId))
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
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception Occured: " + ex.Message);
            }
            
            return View(patient);
        }

        // GET: patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.patientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'AppDbContext.Patients'  is null.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool patientExists(int id)
        {
            try
            {
                return (_context.Patients?.Any(e => e.patientId == id)).GetValueOrDefault();
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception Occured: " + ex.Message);
                return false;
            }

        }
    }
}
