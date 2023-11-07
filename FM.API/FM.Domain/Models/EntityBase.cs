using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Models
{
    /// <summary>
    /// Base for Database entities
    /// </summary>
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
