using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TheGateService.Types {
    [Flags]
    public enum UserType {
        User = 1 << 0,
        BasicEmployee = 1 << 1,
        Shipping = 1 << 2,
        Administrator = 1 << 3
    }

    public class User : Entity<User> {
        #region Properties

        public UserType Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Will be null 99% of the time, except when creating a new user

        #endregion

        #region Constructors

        [DebuggerHidden]
        public User()
            : this(-1, "", "", "", "", UserType.User) { }

        [DebuggerHidden]
        public User(long id, string first, string last, string address, string email, UserType type)
            : base(id) {
            FirstName = first;
            LastName = last;
            Address = address;
            Email = email;
            Type = type;
        }

        [DebuggerHidden]
        public User(User other)
            : this(other.Id,
                   other.FirstName,
                   other.LastName,
                   other.Address,
                   other.Email,
                   other.Type) { }

        #endregion

        #region Equality Members

        protected override bool _Equals(User other) {
            return Type == other.Type &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Address == other.Address &&
                   Email == other.Email;
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ Type.GetHashCode();
                hashCode = (hashCode * 397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Address != null ? Address.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
