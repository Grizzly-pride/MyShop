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

        public CatalogItem(string name, string discription, decimal price, string pictireUrl)
        {
            Name = name;
            Description = discription;
            Price = price;
            PictureUrl = pictireUrl;           
        }  
    }
}
