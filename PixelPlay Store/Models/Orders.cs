namespace PixelPlay.Models
{
    public class Orders 
    {
        public int Id { get; set; }

        [Display(Name = "Order Number")]
        public string? OrderNo { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        [Required,EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Address { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        public List<OrderItem> Items { get; set; } = new();

        // Optional extras
        public decimal TotalAmount => Items.Sum(i => i.Quantity * i.GamePrice);        


    }
}
