using System;
using Elk.Core;
using Newtonsoft.Json;
using GolPooch.Domain.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(ProductOffer), Schema = "Product")]
    public class ProductOffer : IEntity, IInsertDateProperties, IIsActiveProperty
    {
        [Key]
        public int ProductOfferId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public int ProductId { get; set; }

        [Display(Name = nameof(DisplayNames.Chance), ResourceType = typeof(DisplayNames))]
        public byte Chance { get; set; }

        [Display(Name = nameof(DisplayNames.Price), ResourceType = typeof(DisplayNames))]
        public int Price { get; set; }

        [Display(Name = nameof(DisplayNames.ReFoundsPercent), ResourceType = typeof(DisplayNames))]
        public float ReFoundsPercent { get; set; }

        [Display(Name = nameof(DisplayNames.Discount), ResourceType = typeof(DisplayNames))]
        public int Discount { get; set; }

        [Display(Name = nameof(DisplayNames.TotalPrice), ResourceType = typeof(DisplayNames))]
        public int TotalPrice { get; set; }

        [Display(Name = nameof(DisplayNames.Profit), ResourceType = typeof(DisplayNames))]
        public int Profit { get; set; }

        [Display(Name = nameof(DisplayNames.UnUseDay), ResourceType = typeof(DisplayNames))]
        public int UnUseDay { get; set; }

        [Display(Name = nameof(DisplayNames.IsActive), ResourceType = typeof(DisplayNames))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Display(Name = nameof(DisplayNames.ImageUrl), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ImageUrl { get; set; }



        [JsonIgnore]
        public ICollection<Purchase> Purchases { get; set; }
        [JsonIgnore]
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
    }
}