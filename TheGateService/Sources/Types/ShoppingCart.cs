using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;

using ServiceStack.ServiceHost;

using TheGateService.Types;

namespace TheGateService.Endpoints {
    [Route("/cart")]
    public class ShoppingCart:Entity<ShoppingCart> {
        public readonly long UserId;
        public readonly List<ShoppingCartItem> Items;

        public ShoppingCart()
            : this(-1, new List<ShoppingCartItem>()) { }

        public ShoppingCart(long userId, List<ShoppingCartItem> items) {
            UserId = userId;
            Items = items;
        }

        protected override bool _Equals(ShoppingCart other) {
            return UserId == other.UserId &&
                   Items.Equals(other.Items);
        }

        public override int GetHashCode() {
            return (Items != null ? Items.GetHashCode() : 0) ^ UserId.GetHashCode();
        }

        public class ShoppingCartItem {
            public Product Product;
            public int Quantity;
            public decimal TotalPrice { get { return Product.Price * Quantity; } }

            public ShoppingCartItem() {
                Product = new Product();
                Quantity = 0;
            }

            protected bool Equals(ShoppingCartItem other) {
                return Product.Equals(other.Product) &&
                       Quantity == other.Quantity;
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((ShoppingCartItem) obj);
            }

            public override int GetHashCode() {
                unchecked {
                    return ((Product != null ? Product.GetHashCode() : 0) * 397) ^ Quantity.GetHashCode();
                }
            }
        }
    }
}