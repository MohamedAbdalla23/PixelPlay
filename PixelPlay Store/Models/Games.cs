﻿
namespace PixelPlay.Models
{
    public class Games : BaseEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;

        public string? Trailer { get; set; }

        [Precision(5, 2)]
        public decimal Price { get; set; }

        public ICollection<GameDevices> GameDevices { get; set; } = new List<GameDevices>();

        public ICollection<GameCategories> GameCategories { get; set; } = new List<GameCategories>();
    }
}
