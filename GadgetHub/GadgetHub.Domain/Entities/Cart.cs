using GadgetHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    // CART OPERATIONS
    public class Cart
    {
        // Create a List for cart
        private List<CartLine> lineCollection = new List<CartLine>();

        // Access the content of the cart
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        // Add an item (product and quantity) to the cart
        public void AddItem(Product myproduct, int myquantity)
        {
            CartLine line = lineCollection
                            .Where(p => p.Product.ProductID == myproduct.ProductID)
                            .FirstOrDefault();

            // Adding a new item to the cart
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = myproduct,
                    Quantity = myquantity
                });
            }
            // For item that already exists in the cart, just increase the quantity.
            else
            {
                line.Quantity += myquantity;
            }
        }

        // Remove a product
        public void RemoveLine(Product myproduct)
        {
            lineCollection.RemoveAll(i => i.Product.ProductID == myproduct.ProductID);
        }

        // Compute the total cost of the cart
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        // Reset (emptying the cart)
        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}
