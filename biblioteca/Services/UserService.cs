using biblioteca.Data;
using biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using biblioteca.Services.Exceptions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Services
{
    public class UserService
    {
        private readonly BibliotecaContext _context;
        private readonly LoanService _loanService;

        public UserService(BibliotecaContext context, LoanService loanService)
        {
            _loanService = loanService;
            _context = context;
        }

        public void Insert(User obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public List<User> FindAllUser()
        {
            return _context.User.ToList();
        }

        public User FindById(int id)
        {
            return _context.User.FirstOrDefault(obj => obj.Id == id);
        }
        public void RemoveUser(int id)
        {
            var hasActivatedLoans = _loanService.FindAllLoan().Any(s => s.UserId == id && s.Status == Models.Enums.LoanStatus.Activated);
            if (hasActivatedLoans)
            {
                throw new NotFoundException("Não pode remover usuário que tenha um empréstimo ativo");
            }
            var user = _context.User.Find(id);
            _context.User.Remove(user);
            _context.SaveChanges();
        }


        public void UpdateUser(User obj)
        {
            if (!_context.User.Any(x => x.Id == obj.Id))
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
