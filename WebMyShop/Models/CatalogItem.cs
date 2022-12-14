namespace MyShop.Models
{
    public sealed class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public CatalogType? CatalogType { get; set; }   
        public CatalogBrand? CatalogBrand { get; set; }

        public CatalogItem(int id, string name, string discription, decimal price, string pictireUrl)
        {
            Id = id;    
            Name = name;
            Description = discription;
            Price = price;
            PictureUrl = pictireUrl;           
        }

        public void UpdateDetails(CatalogItemDetails details) 
        {
            Name = details.Name;
            Description = details.Description;
            Price = details.Price;
        }

        public readonly record struct CatalogItemDetails
        {
            public string? Name { get; }
            public string? Description { get; }
            public decimal Price { get; }

            public CatalogItemDetails(string? name, decimal price)
            {
                Name = name;
                Price = price;
            }
        }       
    }
}
