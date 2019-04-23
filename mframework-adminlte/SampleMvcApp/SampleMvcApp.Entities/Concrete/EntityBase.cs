using System.ComponentModel.DataAnnotations;

namespace SampleMvcApp.Entities.Concrete
{
    public abstract class EntityBase<T>
    {
        [Key]
        public T Id { get; set; }
    }
}