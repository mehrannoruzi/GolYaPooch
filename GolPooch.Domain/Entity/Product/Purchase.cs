using System;
using Elk.Core;
using GolPooch.Domain.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(Purchase), Schema = "Product")]
    public class Purchase : IEntity, IInsertDateProperties, IModifyDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(ProductOfferId))]
        public ProductOffer ProductOffer { get; set; }
        public int ProductOfferId { get; set; }

        [ForeignKey(nameof(PaymentTransactionId))]
        public PaymentTransaction PaymentTransaction { get; set; }
        public int PaymentTransactionId { get; set; }

        [Display(Name = nameof(DisplayNames.Chance), ResourceType = typeof(DisplayNames))]
        public byte Chance { get; set; }

        [Display(Name = nameof(DisplayNames.UsedChance), ResourceType = typeof(DisplayNames))]
        public byte UsedChance { get; set; }

        [Display(Name = nameof(DisplayNames.IsReFoundable), ResourceType = typeof(DisplayNames))]
        public bool IsReFoundable { get; set; }

        [Display(Name = nameof(DisplayNames.IsFinished), ResourceType = typeof(DisplayNames))]
        public bool IsFinished { get; set; }

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



        public ICollection<DrawChance> DrawChances { get; set; }
    }
}