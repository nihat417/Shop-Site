using Newtonsoft.Json;
using Shop_Site.Models;

namespace Shop_Site.Helpers
{
    public static class ShoppingCartExtensions
    {
        public static void AddToCart(this ISession session, Products product, int quantity)
        {
            var cart = session.GetCart() ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(item => item.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem { Product = product, Quantity = quantity });
            }

            session.SetCart(cart);
        }

        public static void RemoveFromCart(this ISession session, string productId)
        {
            var cart = session.GetCart() ?? new List<CartItem>();
            var itemToRemove = cart.FirstOrDefault(item => item.Product.Id == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                session.SetCart(cart);
            }
        }

        public static List<CartItem> GetCart(this ISession session)
        {
            var json = session.GetString("cart");
            return json == null ? null : JsonConvert.DeserializeObject<List<CartItem>>(json);
        }

        public static void SetCart(this ISession session, List<CartItem> cart)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(cart, jsonSettings);
            session.SetString("cart", json);
        }
    }
}
