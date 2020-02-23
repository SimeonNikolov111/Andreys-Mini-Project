using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Andreys.Services
{
    public interface IUsersService
    {
        void Register(string username, string email, string password);

        bool UserNameExist(string username);

        bool EmailExist(string email);

        string GetUsername(string id);

        string GetUserId(string username, string password);
    }
}
