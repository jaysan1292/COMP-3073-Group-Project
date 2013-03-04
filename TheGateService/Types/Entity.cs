﻿// Project: TheGateService
// Filename: Entity.cs
// 
// Author: Jason Recillo

using System;
using System.Runtime.Serialization;

using ServiceStack.Text;

namespace TheGateService.Types {
    public abstract class Entity {
        #region Properties

        public long Id { get; set; }

        #endregion

        #region Constructors

        public Entity()
            : this(-1) { }

        public Entity(long id) {
            Id = id;
        }

        public Entity(Entity other)
            : this(other.Id) { }

        #endregion

        #region Equality Members

        internal abstract bool Equals(Entity other);

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity) obj);
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        #endregion

        public override string ToString() {
            return JsonSerializer.SerializeToString(this);
        }
    }

    public abstract class Entity<T> : Entity where T : Entity {
        // Instance of type using no-arg constructor used for checking valid objects
        private static readonly T InitialValue = (T) Activator.CreateInstance(typeof(T));

        #region Constructors

        public Entity()
            : this(-1) { }

        public Entity(long id)
            : base(id) { }

        public Entity(T other)
            : this((Entity) other) { }

        public Entity(Entity other)
            : this(other.Id) { }

        #endregion

        [IgnoreDataMember]
        public bool IsInitialized {
            get {
                // This object's id may already have been set
                InitialValue.Id = Id;

                // Return false if, after making the ids the same, this object's
                // other properties match that of the properties of initialValue
                var equals = !Equals(InitialValue);

                // Reset the initial value's ID back to what it was before
                InitialValue.Id = -1;

                return equals;
            }
        }

        #region Equality Members

        internal override bool Equals(Entity other) {
            return Id == other.Id && _Equals((T) other);
        }

        // ReSharper disable InconsistentNaming
        protected abstract bool _Equals(T other);
        // ReSharper restore InconsistentNaming

        #endregion
    }
}