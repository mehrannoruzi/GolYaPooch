using System;
using Elk.Core;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table("Transaction", Schema = "Payment")]
    public class PaymentTransaction : IEntity, IInsertDateProperties, IModifyDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentTransactionId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(PaymentGatewayId))]
        public PaymentGateway PaymentGateway { get; set; }
        public int PaymentGatewayId { get; set; }

        [ForeignKey(nameof(ProductOfferId))]
        public ProductOffer ProductOffer { get; set; }
        public int ProductOfferId { get; set; }

        [Display(Name = nameof(DisplayNames.Type), ResourceType = typeof(DisplayNames))]
        public TransactionType Type { get; set; }

        [Display(Name = nameof(DisplayNames.Price), ResourceType = typeof(DisplayNames))]
        public int Price { get; set; }

        [Display(Name = nameof(DisplayNames.IsSuccess), ResourceType = typeof(DisplayNames))]
        public bool IsSuccess { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.ModifyDate), ResourceType = typeof(DisplayNames))]
        public DateTime ModifyDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.ModifyDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ModifyDateSh { get; set; }

        [Column(TypeName = "varchar(21)")]
        [Display(Name = nameof(DisplayNames.UserSheba), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(21, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(21, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UserSheba { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Display(Name = nameof(DisplayNames.Status), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Status { get; set; }

        [Column(TypeName = "varchar(36)")]
        [Display(Name = nameof(DisplayNames.TrackingId), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(36, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(36, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string TrackingId { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Display(Name = nameof(DisplayNames.Description), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }



        public ICollection<Purchase> Purchases { get; set; }
    }
}