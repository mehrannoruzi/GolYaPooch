using System;
using Elk.Core;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table("Gateway", Schema = "Payment")]
    public class PaymentGateway : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentGatewayId { get; set; }

        [Display(Name = nameof(DisplayNames.BankName), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public BankNames BankName { get; set; }

        [Display(Name = nameof(DisplayNames.IsActive), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.Name), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Name { get; set; }

        [Column(TypeName = "varchar(25)")]
        [Display(Name = nameof(DisplayNames.Username), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Username { get; set; }

        [Column(TypeName = "varchar(25)")]
        [Display(Name = nameof(DisplayNames.Password), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Password { get; set; }

        [Column(TypeName = "varchar(36)")]
        [Display(Name = nameof(DisplayNames.MerchantId), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(36, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(36, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string MerchantId { get; set; }



        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
    }
}