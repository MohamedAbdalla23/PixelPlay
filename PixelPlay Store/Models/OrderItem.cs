namespace PixelPlay.Models
{
    public class OrderItem
    {      
            public int Id { get; set; }

            public int OrderId { get; set; }
            public Orders Order { get; set; }

            public int GameId { get; set; }
            public Games Game { get; set; }

            public int Quantity { get; set; }

            [Precision(5,2)]
            public decimal GamePrice { get; set; }
    }    
}
