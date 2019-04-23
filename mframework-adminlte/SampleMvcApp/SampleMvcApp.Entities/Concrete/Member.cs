using SampleMvcApp.Entities.Abstract;
using SampleMvcApp.Web.Core.Mvc.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleMvcApp.Entities.Concrete
{
    // CQRS : Command and Query Responsible Segregation

    public class Member : EntityBase<int>, IEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public class MemberCommand
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Adı")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Soyad")]
        public string Surname { get; set; }

        [Required, EmailAddress()]
        [DisplayName("E-Posta")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [DisplayName("Re-Şifre"), Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required]
        [DisplayName("Aktif mi?")]
        [UICheckbox(nameof(IsActive))]
        public string IsActive { get; set; } = "off";
    }

    public class MemberQuery
    {
        public int Id { get; set; }

        [DisplayName("Ad Soyad")]
        public string NameSurname { get; set; }
        [DisplayName("E-Posta")]
        public string Email { get; set; }
        [DisplayName("Aktif mi?")]
        public string IsActive { get; set; }
    }
}
