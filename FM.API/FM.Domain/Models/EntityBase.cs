using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Models
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
