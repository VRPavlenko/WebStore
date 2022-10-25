using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        

        [Required]
        public string Name { get; set; }
        public string ShortDesc{ get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
        public string ImageUrl { get; set; }


        [Display(Name="Category Type")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        [Display(Name = "Used Type")]
        public int UsedTypeId { get; set; }
        [ForeignKey("UsedTypeId")]
        public UsedType UsedType { get; set; }
    }
}