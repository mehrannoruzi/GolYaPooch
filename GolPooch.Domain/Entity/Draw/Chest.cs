using System;
using Elk.Core;
using GolPooch.Domain.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolPooch.Domain.Entity
{
    [Table(nameof(Chest), Schema = "Draw")]
    public class Chest : IEntity, IInsertDateProperties, IIsActiveProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChestId { get; set; }

        [Display(Name = nameof(DisplayNames.RoundCount), ResourceType = typeof(DisplayNames))]
        public int RoundCount { get; set; }

        [Display(Name = nameof(DisplayNames.ParticipantCount), ResourceType = typeof(DisplayNames))]
        public int ParticipantCount { get; set; }

        [Display(Name = nameof(DisplayNames.WinnerCount), ResourceType = typeof(DisplayNames))]
        public int WinnerCount { get; set; }

        [Display(Name = nameof(DisplayNames.IsActive), ResourceType = typeof(DisplayNames))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.Title), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Title { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Display(Name = nameof(DisplayNames.ImageUrl), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ImageUrl { get; set; }



        public ICollection<Round> Rounds { get; set; }
    }
}