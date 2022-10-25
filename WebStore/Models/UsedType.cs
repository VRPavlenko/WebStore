using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class UsedType
    {
        [Key]
        public int UsedTypeId { get; set; }
        [Required]
        public string UsedTypeName { get; set; }
    }
}