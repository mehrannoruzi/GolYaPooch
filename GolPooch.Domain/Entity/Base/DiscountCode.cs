using System;
using Elk.Core;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(DiscountCode), Schema = "Base")]
    public class DiscountCode : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountCodeId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int? UserId { get; set; }

        [ForeignKey(nameof(PurchaseId))]
        public Purchase Purchase { get; set; }
        public int? PurchaseId { get; set; }

        [Display(Name = nameof(DisplayNames.DiscountCodeType), ResourceType = typeof(DisplayNames))]
        public DiscountCodeType Type { get; set; }

        [Display(Name = nameof(DisplayNames.DiscountCodeType), ResourceType = typeof(DisplayNames))]
        public bool IsUsed { get; set; } = false;

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime UsedDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UsedDateSh { get; set; }

        [Column(TypeName = "varchar(16)")]
        [Display(Name = nameof(DisplayNames.Code), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(16, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(16, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Code { get; set; }
    }
}