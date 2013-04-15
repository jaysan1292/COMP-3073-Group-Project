using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;

namespace TheGateService.Types {
    [Route("/register", "GET,POST")]
    public class UserRegister {
        public User User;

        public string FirstName { get { return User.FirstName; } set { User.FirstName = value; } }
        public string LastName { get { return User.LastName; } set { User.LastName = value; } }
        public string Email { get { return User.Email; } set { User.Email = value; } }
        public string Password { get { return User.Password; } set { User.Password = value; } }

        public UserRegister()
            : this(new User()) { }

        public UserRegister(User user) {
            User = user;
        }
    }
}
