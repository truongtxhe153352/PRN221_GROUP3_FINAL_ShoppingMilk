using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class ShoppingCart
    {
        shoppingMilkPrn221Context context;

        string ShoppingCartId { get; set; }
        //     public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart()
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId();
            return cart;
        }

        public void AddToCart(Milk milk)
        {
            // Get the matching cart and album instances
            var cartItem = context.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.MilkId == milk.MilkId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    MilkId = milk.MilkId,
                    CartId = ShoppingCartId,
                    Quantity = 1,
                    DateCreate = DateTime.Now
                };

                context.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Quantity++;
            }
            // Save changes
            context.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the matching cart and album id
            var cartItem = context.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId && c.RecordId == id);

            int itemCount = 0;
            if (cartItem != null)
            {
                context.Carts.Remove(cartItem);

                // Save changes
                context.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = context.Carts
                .Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                context.Carts.Remove(cartItem);
            }
            // Save changes
            context.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return context.Carts.Where(cart => cart.CartId == ShoppingCartId)
                .Include(cart => cart.Milk)
                .ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in context.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public double GetTotal()
        {
            // Multiply album price by count of that album to get
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total

            double? total = (from cartItems in context.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Milk.Price).Sum();
            return total ?? 0;
        }

        public int CreateOrder(Order order)
        {
            var cartItems = GetCartItems();
            // Save the order
            try
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            int orderID = (int)context.Orders.Select(o => o.OrderId).Max();
            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    MilkId = item.MilkId,
                    OrderId = orderID,
                    Quantity = (int)item.Quantity
                };
                try
                {
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return orderID;
        }


        public string GetCartId()
        {

            if (Settings.CartId == null)
            {
                if (Settings.UserName != null)
                    Settings.CartId = Settings.UserName;
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    Settings.CartId = tempCartId.ToString();
                }
            }
            return Settings.CartId;

            return null;
        }


        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart()
        {
            var shoppingCart = context.Carts.Where(c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = Settings.UserName;
            }
            context.SaveChanges();
            Settings.CartId = null;
        }

    }
}
