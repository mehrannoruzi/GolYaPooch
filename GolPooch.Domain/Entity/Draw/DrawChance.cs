using System;
using Elk.Core;
using GolPooch.Domain.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(DrawChance), Schema = "Draw")]
    public class DrawChance : IEntity, IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DrawChanceId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int? UserId { get; set; }

        [ForeignKey(nameof(PurchaseId))]
        public Purchase Purchase { get; set; }
        public int PurchaseId { get; set; }

        [ForeignKey(nameof(RoundId))]
        public Round Round { get; set; }
        public int RoundId { get; set; }

        [Display(Name = nameof(DisplayNames.Counter), ResourceType = typeof(DisplayNames))]
        public int Counter { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "varchar(16)")]
        [Display(Name = nameof(DisplayNames.Code), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(16, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(16, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Code { get; set; }
    }
}