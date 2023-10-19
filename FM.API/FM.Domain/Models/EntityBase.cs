using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Models
{
    /// <summary>
    /// Base para entidades de Base de Datos
    /// </summary>
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
