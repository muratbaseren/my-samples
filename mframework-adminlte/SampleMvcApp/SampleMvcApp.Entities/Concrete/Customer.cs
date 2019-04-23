using SampleMvcApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace SampleMvcApp.Entities.Concrete
{
    public class Customer : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
