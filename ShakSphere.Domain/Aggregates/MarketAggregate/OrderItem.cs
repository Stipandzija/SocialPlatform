

namespace ShakSphere.Domain.Aggregates.MarketAggregate
{
    public class OrderItem
    {
        public Guid OrderItemId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        private OrderItem() { }

        public static OrderItem CreateOrderItem(Product product, int quantity)
        {
            //TO Do provjere
            return new OrderItem
            {
                OrderItemId = Guid.NewGuid(),
                ProductId = product.ProductId,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity
            };
        }
    }
}
