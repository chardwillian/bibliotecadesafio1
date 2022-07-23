using biblioteca.Data;
using biblioteca.Models;
using biblioteca.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Book FindById(int id)
        {
            return _context.Book.FirstOrDefault(obj => obj.Id == id);
        }

        public void RemoveBook(int id)
        {
            var loans = _loanService.FindById(id);
            if (loans != null)
            {
                throw new NotFoundException("Não é possível deletar um livro que tenha um empréstimo ativo");
            }
            var book = _context.Book.Find(id);
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

        public void DecrementAmountBorrowedBook(int id)
        {
            var book = _context.Book.Find(id);
            //book.Amount = book.Amount - 1
            book.Amount--;
            _context.Update(book);
            _context.SaveChanges();
        }

        public void IncrementAmountBorrowedBook(int id)
        {
            var book = _context.Book.Find(id);
            book.Amount++;
            _context.Update(book);
            _context.SaveChanges();
        }
    }
}
