using System;
using Elk.Core;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Dto
{
    public class UserDto
    {
        [Display(Name = nameof(DisplayNames.Gender), ResourceType = typeof(DisplayNames))]
        public Gender Gender { get; set; }

        [Display(Name = nameof(DisplayNames.Region), ResourceType = typeof(DisplayNames))]
        public RegionNames Region { get; set; }

        [Display(Name = nameof(DisplayNames.Birthdate), ResourceType = typeof(DisplayNames))]
        public DateTime BirthdateMi { get { return PersianDateTime.Parse(BirthdateSh).ToDateTime(); } }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.Birthdate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string BirthdateSh { get; set; }

        [Display(Name = nameof(DisplayNames.FirstName), ResourceType = typeof(DisplayNames))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FirstName { get; set; }

        [Display(Name = nameof(DisplayNames.LastName), ResourceType = typeof(DisplayNames))]
        [MaxLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Display(Name = nameof(DisplayNames.Email), ResourceType = typeof(DisplayNames))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Email { get; set; }

    }
}