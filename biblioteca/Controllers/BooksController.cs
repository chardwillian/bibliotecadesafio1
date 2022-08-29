using biblioteca.Data;
using biblioteca.Models;
using biblioteca.Services;
using biblioteca.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;
        private readonly LoanService _loanService;

        public BooksController(BookService bookService, LoanService loanService)
        {
            _bookService = bookService;
            _loanService = loanService;
        }
        public IActionResult Index()
        {
            var books = _bookService.FindForLoan();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            _bookService.Insert(book);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var book = _bookService.FindById(id.Value);
            if(book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _bookService.RemoveBook(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.FindById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.FindById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
             try
            {
                _bookService.UpdateBook(book);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
