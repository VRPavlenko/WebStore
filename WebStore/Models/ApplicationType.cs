using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class ApplicationType
    {
        [Key]
        public int TypeId { get; set; }
        [Required]
        public string TypeName { get; set; }
    }
}