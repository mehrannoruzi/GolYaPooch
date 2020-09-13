using System;
using Elk.Core;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(Round), Schema = "Draw")]
    public class Round : IEntity, IInsertDateProperties, IModifyDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoundId { get; set; }

        [ForeignKey(nameof(ChestId))]
        public Chest Chest { get; set; }
        public int ChestId { get; set; }

        [ForeignKey(nameof(WinnerUserId))]
        public User WinnerUser { get; set; }
        public int WinnerUserId { get; set; }

        [Display(Name = nameof(DisplayNames.ParticipantCount), ResourceType = typeof(DisplayNames))]
        public int ParticipantCount { get; set; }

        [Display(Name = nameof(DisplayNames.State), ResourceType = typeof(DisplayNames))]
        public RoundState State { get; set; }

        [Display(Name = nameof(DisplayNames.StartDate), ResourceType = typeof(DisplayNames))]
        public DateTime StartDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.StartDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string StartDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.EndDate), ResourceType = typeof(DisplayNames))]
        public DateTime EndDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.EndDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string EndDateSh { get; set; }

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

        [Display(Name = nameof(DisplayNames.Description), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }



        public ICollection<DrawChance> DrawChances { get; set; }
    }
}