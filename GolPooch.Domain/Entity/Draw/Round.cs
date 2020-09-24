﻿using System;
using Elk.Core;
using Newtonsoft.Json;
using GolPooch.Domain.Enum;
using GolPooch.Domain.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(Round), Schema = "Draw")]
    public class Round : IEntity, IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoundId { get; set; }

        [ForeignKey(nameof(ChestId))]
        public Chest Chest { get; set; }
        public int ChestId { get; set; }

        [ForeignKey(nameof(WinnerUserId))]
        public User WinnerUser { get; set; }
        public int? WinnerUserId { get; set; }

        [Display(Name = nameof(DisplayNames.ParticipantCount), ResourceType = typeof(DisplayNames))]
        public int ParticipantCount { get; set; }

        [Display(Name = nameof(DisplayNames.State), ResourceType = typeof(DisplayNames))]
        public RoundState State { get; set; }

        [Display(Name = nameof(DisplayNames.StartDate), ResourceType = typeof(DisplayNames))]
        public DateTime? OpenDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.StartDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string OpenDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.CloseDate), ResourceType = typeof(DisplayNames))]
        public DateTime? CloseDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.CloseDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string CloseDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.DrawDate), ResourceType = typeof(DisplayNames))]
        public DateTime DrawDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.DrawDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string DrawDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.Description), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }


        [JsonIgnore]
        public ICollection<DrawChance> DrawChances { get; set; }
    }
}