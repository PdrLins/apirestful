using DesafioPitang.Models;
using System;
using System.Collections.Generic;


namespace DesafioPitang.Business
{
    public class SaveUser
    {
        private string _password;
        public string Password
        {
            get => _password;
            set { _password = Utils.Utils.Instance.GenerateMd5(value?.Trim()); }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value?.ToUpper().Trim(); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value?.ToUpper().Trim(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value?.ToUpper().Trim(); }
        }

        public List<UserContact> Phones { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
