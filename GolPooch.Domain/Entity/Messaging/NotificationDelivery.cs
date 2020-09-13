using System;
using Elk.Core;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(NotificationDelivery), Schema = "Messaging")]
    public class NotificationDelivery : IEntity, IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationDeliveryId { get; set; }

        [ForeignKey(nameof(NotificationId))]
        public Notification Notification { get; set; }
        public int NotificationId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int UserId { get; set; }

        [Display(Name = nameof(DisplayNames.Type), ResourceType = typeof(DisplayNames))]
        public NotificationDeliveryType Type { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }
    }
}