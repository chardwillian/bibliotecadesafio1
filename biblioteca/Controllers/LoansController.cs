using biblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using biblioteca.Models.ViewModels;
using biblioteca.Models;
using System.Collections.Generic;
using biblioteca.Services.Exceptions;

namespace biblioteca.Controllers
{
    public class LoansController : Controller
    {
        private readonly LoanService _loanService;
        private readonly BookService _bookService;
        private readonly UserService _userService;

        public LoansController(LoanService loanService, BookService bookService, UserService userService)
        {
            _loanService = loanService;
            _bookService = bookService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            var listLoan = _loanService.FindAllLoan();
            return View(listLoan);
        }

        public IActionResult Create()
        {
            var books = _bookService.FindAllBooks();
            var users = _userService.FindAllUser();
            var viewModel = new LoanFormViewModel { Books = books, Users = users };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Loan loan) //, Book book
        {
            //_bookService.DecrementAmountBook(book);
            _loanService.Insert(loan);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _loanService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _loanService.RemoveLoan(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = _loanService.FindById(id.Value);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var obj = _loanService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<User> users = _userService.FindAllUser();
            List<Book> books = _bookService.FindAllBooks();
            LoanFormViewModel viewModel = new LoanFormViewModel { Loan = obj, Users = users, Books = books };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Loan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest();
            }
            try
            {
                _loanService.UpdateLoan(loan);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException)
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
