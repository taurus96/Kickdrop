using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kickdrop.Api.Models
{
    public enum ShoeColor
    {
        Blue,
        Green,
        Black,
        Pink,
        Yellow
    }

    public class Shoe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(24)")]
        public ShoeColor Color { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Size { get; set; }
    }
}