using System.ComponentModel.DataAnnotations;

namespace POSBackend.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string Category { get; set; }  // Contoh: "Makanan", "Minuman", "Lainnya"

        public string ImageUrl { get; set; }  // URL gambar yang disimpan di Cloudinary
    }
}
