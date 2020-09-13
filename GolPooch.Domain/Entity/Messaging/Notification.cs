using System;
using Elk.Core;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(Notification), Schema = "Messaging")]
    public class Notification : IEntity, IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        /// <summary>
        /// if this field set with null, that means this notification is for all users
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int? UserId { get; set; }

        [Display(Name = nameof(DisplayNames.Type), ResourceType = typeof(DisplayNames))]
        public NotificationType Type { get; set; }

        [Display(Name = nameof(DisplayNames.Action), ResourceType = typeof(DisplayNames))]
        public NotificationAction Action { get; set; }

        [Display(Name = nameof(DisplayNames.IsActive), ResourceType = typeof(DisplayNames))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(DisplayNames.IsSent), ResourceType = typeof(DisplayNames))]
        public bool IsSent { get; set; }

        [Display(Name = nameof(DisplayNames.IsSuccess), ResourceType = typeof(DisplayNames))]
        public bool? IsSuccess { get; set; }

        [Display(Name = nameof(DisplayNames.IsRead), ResourceType = typeof(DisplayNames))]
        public bool IsRead { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.SentDate), ResourceType = typeof(DisplayNames))]
        public DateTime SentDateMi { get; set; }

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
        public string ActionUrl { get; set; }

        [Column(TypeName = "varchar(500)")]
        [Display(Name = nameof(DisplayNames.Text), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(500, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(500, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Text { get; set; }


        //public ICollection<NotificationDelivery> NotificationDeliveries { get; set; }
    }
}