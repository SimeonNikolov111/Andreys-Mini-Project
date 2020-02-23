﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Andreys.Data;
using Andreys.Models;

namespace Andreys.Services
{
    public class UsersService : IUsersService
    {
        private readonly AndreysDbContext db;

        public UsersService(AndreysDbContext db)
        {
            this.db = db;
        }

        public bool UserNameExist(string username)
        {
            return this.db.Users.Any(u => u.Username == username);
        }

        public bool EmailExist(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }

        public string GetUsername(string id)
        {
            var username = this.db.Users.
                Where(u => u.Id == id).
                Select(u => u.Username).
                FirstOrDefault();

            return username;
        }

        public string GetUserId(string username, string password)
        {
            var hashPassword = this.Hash(password);
            var user = this.db.Users.FirstOrDefault(u => u.Username == username && u.Password == hashPassword);

            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public void Register(string username, string email, string password)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Email = email,
                Password = this.Hash(password)
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
