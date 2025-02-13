namespace ShakSphere.Domain.Aggregates.MarketAggregate
{
    public class Product
    {
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockCap { get; private set; }

        private Product() { }

        public static Product CreateProduct(string name, string description, decimal price, int stockQuantity)
        {
            ///TO DO: provjere
            return new Product
            {
                ProductId = Guid.NewGuid(),
                Name = name,
                Description = description,
                Price = price,
                StockCap = stockQuantity
            };
        }

        public void UpdateStock(int newStock)
        {
            if (newStock < 0)
                throw new ArgumentException("Količina na zalihi ne može biti negativna.", nameof(newStock));
            StockCap = newStock;
        }

        public void AdjustStock(int quantityChange)
        {
            var newStock = StockCap + quantityChange;
            if (newStock < 0)
                throw new InvalidOperationException("Nema dovoljno zaliha za ovaj proizvod.");
            StockCap = newStock;
        }
    }
}
