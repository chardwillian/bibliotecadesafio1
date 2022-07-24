using biblioteca.Data;
using biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using biblioteca.Services.Exceptions;
using biblioteca.Models.Enums;

namespace biblioteca.Services
{
    public class LoanService
    {
        private readonly BibliotecaContext _context;
        //private readonly BookService _bookService;

        public LoanService(BibliotecaContext context) //, BookService bookService
        {
       //     _bookService = bookService;
            _context = context;
        }

        public List<Loan> FindAllLoan()
        {
            return _context.Loan.ToList();
        }

        public void Insert(Loan obj)
        {
            obj.StartLoan = DateTime.Now;
            obj.Status = LoanStatus.Activated;
            _context.Add(obj);
            _context.SaveChanges();
        }

        //public void InsertAmounBorrowed()
        //{
        //    _bookService.IncrementAmountBorrowedBook();
        //}
        public Loan FindById(int id)
        {
            return _context.Loan.Include(obj => obj.Book).Include(obj => obj.User).FirstOrDefault(obj => obj.Id == id);
        }

        public void RemoveLoan(int id)
        {
            var loanRemove = _context.Loan.Find(id);
            DateTime dateNow = DateTime.Now;
            var dateLoan = loanRemove.StartLoan;
            var tempoCalculado = dateNow - dateLoan;
          if (tempoCalculado.TotalMinutes > 10)
            { 
                throw new NotFoundException ("Não é possível remover depois de 10 minutos"); 
            }
            
            //if (DateTime.Now.Subtract(DateTime.StartLoan) > 00:10:00 ) {
            //    throw new NotFoundException ("Não é possível remover depois de 10 minutos");
            //} 
                _context.Loan.Remove(loanRemove);
                _context.SaveChanges();
            } 

        public void UpdateLoan(Loan obj)
        {
            if (!_context.Loan.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id não existe");
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
