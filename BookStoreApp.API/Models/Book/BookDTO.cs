using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book
{
    public class BookCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [Range(1800, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Summary { get; set; }

        public string Image { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }

    public class BookUpdateDTO: BaseDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [Range(1800, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Summary { get; set; }

        public string Image { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

    }

    public class BookReadOnlyDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

    }

    public class BookDetailDTO : BaseDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

    }
}
