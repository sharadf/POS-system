namespace POS_system.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public float Price { get; set; }
    }
}