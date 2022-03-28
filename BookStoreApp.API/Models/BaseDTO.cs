using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models
{
    public abstract class BaseDTO
    {
        [Required]
        public int Id { get; set; }     
    }
}
