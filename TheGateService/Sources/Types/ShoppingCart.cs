using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;

namespace TheGateService.Types {
    [Route("/cart")]
    public class ShoppingCart : Entity<ShoppingCart> {
        public List<ShoppingCartItem> Items { get; set; }
        public long UserId { get; set; }

        public ShoppingCart()
            : this(-1, new List<ShoppingCartItem>()) { }

        public ShoppingCart(long userId, List<ShoppingCartItem> items) {
            UserId = userId;
            Items = items;
        }

        public int TotalQuantity { get { return Items.Aggregate(0, (amount, item) => amount + item.Quantity); } }

        public decimal TotalPrice {
            get {
                return Items.Aggregate(new decimal(0), (amount, item) => {
                    var itemPrice = item.Product.Price * item.Quantity;
                    return amount + itemPrice;
                });
            }
        }

        protected override bool _Equals(ShoppingCart other) {
            return UserId == other.UserId &&
                   Items.Equals(other.Items);
        }

        public override int GetHashCode() {
            return (Items != null ? Items.GetHashCode() : 0) ^ UserId.GetHashCode();
        }

        [Route("/cart/item", "POST,DELETE")]
        public class ShoppingCartItem {
            public Product Product { get; set; }
            public int Quantity { get; set; }

            public ShoppingCartItem() {
                Product = new Product();
                Quantity = 0;
            }

            public decimal TotalPrice { get { return Product.Price * Quantity; } }

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
