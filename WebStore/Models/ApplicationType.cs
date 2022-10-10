using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class ApplicationType
    {
        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}