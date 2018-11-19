using System.Linq;
using DesafioPitang.Data;
using DesafioPitang.Models;
using DesafioPitang.Business.Interfaces;
using System;

namespace DesafioPitang.Business.Services
{
    public class UserService : IUserService
    {
        private ApiContext _context;
        public UserService(ApiContext context)
        {
            _context = context;
        }

        public void Handle(SaveUser input)
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
        }

        public User Handle(FindUser input)
        {
            var query = _context.Users.AsQueryable();
            if (input.Id.HasValue)
            {
               return  query.FirstOrDefault(w => w.Id == input.Id);
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
