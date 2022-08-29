using biblioteca.Data;
using biblioteca.Models;
using biblioteca.Models.Enums;
using biblioteca.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace biblioteca.Services
{
    public class BookService
    {
        private readonly BibliotecaContext _context;
        private readonly LoanService _loanService;

        public BookService(BibliotecaContext context, LoanService loanService)
        {
            _loanService = loanService;
            _context = context;
        }

        public void Insert(Book obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public List<Book> FindAllBooks()
        {
            return _context.Book.ToList();
        }

        public List<Book> FindForLoan()
        {
           // var teste = _context.Book.ToList();
           // var teste22 = _context.Book.Include(x => x.Loans).ToList();
            var hasAvailableLoan = _context.Book.Include(x => x.Loans)
                .Where(x => x.Amount - x.Loans.Where(s => s.Status == LoanStatus.Activated).Count() > 0)
                .ToList();
            return hasAvailableLoan;
        
        }

        //public void NovosExemplares()
        //{
            
        //    if (!teste && _context.Book.Where(x => x.Amount.Count() < 10)
        //    {

        //    }
        //}
        public Book FindById(int id)
        {
          // return _context.Book.FirstOrDefault(obj => obj.Id == id);
            return _context.Book.Find(id);
        }

        public void RemoveBook(int id)
        {
            var hasActivatedLoans = _loanService.FindAllLoan().Any(s => s.BookId == id && s.Status == Models.Enums.LoanStatus.Activated);
            if (hasActivatedLoans)
            {
                throw new NotFoundException("Não é possível deletar um livro que tenha um empréstimo ativo");
            }
            var book = FindById(id);
            _context.Book.Remove(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book obj)
        {
            if (!_context.Book.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
