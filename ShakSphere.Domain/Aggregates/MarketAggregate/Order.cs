namespace ShakSphere.Domain.Aggregates.MarketAggregate
{
    public class Order
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        private Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public static Order CreateOrder(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("ID kupca nije validan.", nameof(customerId));

            return new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };
        }

        public void AddOrderItem(Product product, int quantity)
        {
            //TO DO provjere
            var orderItem = OrderItem.CreateOrderItem(product, quantity);
            OrderItems.Add(orderItem);

            product.AdjustStock(-quantity);
        }
        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
