using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Models;

namespace Library.Controllers
{
    public class LoansController : Controller
    {
        private readonly LibraryContext _context;

        public LoansController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LoanDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["ReturnDateSortParm"] = sortOrder == "ReturnDate" ? "return_desc" : "ReturnDate";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var loans = from l in _context.Loans.Include(l => l.Book).Include(l => l.Reader)
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                loans = loans.Where(l => l.Book.Title.Contains(searchString)
                                       || l.Reader.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    loans = loans.OrderByDescending(l => l.LoanDate);
                    break;
                case "ReturnDate":
                    loans = loans.OrderBy(l => l.ReturnDate);
                    break;
                case "return_desc":
                    loans = loans.OrderByDescending(l => l.ReturnDate);
                    break;
                default:
                    loans = loans.OrderBy(l => l.LoanDate);
                    break;
            }

            return View(await loans.AsNoTracking().ToListAsync());
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name");
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title");
            return View();
        }

        // POST: Loans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoanDate,ReturnDate,BookId,ReaderId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name", loan.ReaderId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loan.BookId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loan.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name", loan.ReaderId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoanDate,ReturnDate,BookId,ReaderId")] Loan loan)
        {
            if (id != loan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loan.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name", loan.ReaderId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }
    }
}
