using System.Linq;
using ApiRestful.Data;
using ApiRestful.Models;
using ApiRestful.Business.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;

namespace ApiRestful.Business.Services
{
    public class UserService : IUserService
    {
        private ApiContext _context;
        public UserService(ApiContext context)
        {
            _context = context;
        }

        public User Handle(SaveUser input)
        {
            var newUser = new User
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Password = input.Password,
                Email = input.Email,
                CreatedAt = input.CreatedAt,
                Phones = input.Phones

            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public User Handle(FindUser input)
        {
            var query = _context.Users.Include(f => f.Phones).AsQueryable();
            if (input.Id.HasValue)
            {
                return query.FirstOrDefault(w => w.Id == input.Id);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(input.Email))
                {
                    query = query.Where(w => w.Email == input.Email);
                }

                if (!string.IsNullOrWhiteSpace(input.Password))
                {
                    query = query.Where(w => w.Password == input.Password);
                }
            }

            return query.FirstOrDefault();
        }

        public bool Handle(CheckHasEmail input)
        {
            return _context.Users.Count(w => w.Email == input.Email) > 0;
        }

        public void Handle(UpdateLastLoginUser input)
        {
            var user = _context.Users.First(w => w.Id == input.Id);

            user.LastLogin = DateTime.Now;
            _context.SaveChanges();

        }
    }
}
