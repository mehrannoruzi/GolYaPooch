using System;
using Elk.Core;
using GolPooch.Domain.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(UserDeviceLog), Schema = "Base")]
    public class UserDeviceLog : IEntity, IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserDeviceLogId { get; set; }

        [Display(Name = nameof(DisplayNames.MobileNumber), ResourceType = typeof(DisplayNames))]
        public long MobileNumber { get; set; }

        [Display(Name = nameof(DisplayNames.IsMobile), ResourceType = typeof(DisplayNames))]
        public bool IsMobile { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Display(Name = nameof(DisplayNames.IP), ResourceType = typeof(DisplayNames))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string IP { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Display(Name = nameof(DisplayNames.Version), ResourceType = typeof(DisplayNames))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Os { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = nameof(DisplayNames.Version), ResourceType = typeof(DisplayNames))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Device { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = nameof(DisplayNames.Version), ResourceType = typeof(DisplayNames))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Application { get; set; }
    }
}