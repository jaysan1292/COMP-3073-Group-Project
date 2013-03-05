using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;

using TheGateService.Types;

namespace TheGateService.Database {
    public interface IDbProvider<T> where T : Entity<T> {
        /// <summary>
        /// Retrieves an object from the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the object to get.</param>
        /// <returns>The item from the database.</returns>
        T Get(long id);

        /// <summary>
        /// Saves the given object into the database.
        /// </summary>
        /// <param name="obj">The object to save.</param>
        /// <returns>The database ID of the new object.</returns>
        long Create(T obj);

        /// <summary>
        /// Updates the given object, provided its ID is valid.
        /// </summary>
        /// <param name="obj">The object to update.</param>
        void Update(T obj);

        /// <summary>
        /// Deletes the given object from the database.
        /// </summary>
        /// <param name="id">The ID of the object to delete.</param>
        void Delete(long id);
    }
}