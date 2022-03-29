using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book
{
    public class BookCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }
        [Required]
        public string Isbn { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }

    public class BookUpdateDTO: BaseDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }
        [Required]
        public string Isbn { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

    }

    public class BookReadOnlyDTO : BaseDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }


    }
}
