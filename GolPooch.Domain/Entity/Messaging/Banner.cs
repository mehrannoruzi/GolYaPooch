using System;
using Elk.Core;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(Banner), Schema = "Messaging")]
    public class Banner : IEntity, IInsertDateProperties, IIsActiveProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BannerId { get; set; }

        [ForeignKey(nameof(PageId))]
        public Page Page { get; set; }
        public int PageId { get; set; }

        [Display(Name = nameof(DisplayNames.IsActive), ResourceType = typeof(DisplayNames))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(DisplayNames.Type), ResourceType = typeof(DisplayNames))]
        public BannerType Type { get; set; }

        [Display(Name = nameof(DisplayNames.ActionType), ResourceType = typeof(DisplayNames))]
        public ActionType ActionType { get; set; }

        [Display(Name = nameof(DisplayNames.DisplayType), ResourceType = typeof(DisplayNames))]
        public DisplayType DisplayType { get; set; }

        [Display(Name = nameof(DisplayNames.LocationType), ResourceType = typeof(DisplayNames))]
        public LocationType LocationType { get; set; }

        [Display(Name = nameof(DisplayNames.ExpirationDate), ResourceType = typeof(DisplayNames))]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Display(Name = nameof(DisplayNames.ActionText), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ActionText { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = nameof(DisplayNames.Subject), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Subject { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Display(Name = nameof(DisplayNames.ImageUrl), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ImageUrl { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Display(Name = nameof(DisplayNames.ActionUrl), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Href { get; set; }

        [Column(TypeName = "varchar(500)")]
        [Display(Name = nameof(DisplayNames.Text), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(500, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(500, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Text { get; set; }
    }
}