﻿// Project: TheGateService
// Filename: Product.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;

using TheGateService.Responses;

namespace TheGateService.Types {
    [Route("/products")]
    public class Products {
        public List<Product> Results { get; set; }
    }

    [Route("/products/{Id}")]
    public class Product : Entity<Product>, IReturn<ProductResponse> {
        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Featured { get; set; }
        public bool Showcase { get; set; }

        #endregion

        #region Constructors

        public Product()
            : this(-1, "", "", 0, false, false) { }

        public Product(long id, string name, string description, decimal price, bool featured, bool showcase)
            : base(id) {
            Name = name;
            Description = description;
            Price = price;
            Featured = featured;
            Showcase = showcase;
        }

        public Product(Product other)
            : this(other.Id,
                   other.Name,
                   other.Description,
                   other.Price,
                   other.Featured,
                   other.Showcase) { }

        #endregion

        #region Equality Members

        protected override bool _Equals(Product other) {
            return Name == other.Name &&
                   Description == other.Description &&
                   Price == other.Price;
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
