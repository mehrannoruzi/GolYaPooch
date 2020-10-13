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
    [Table(nameof(User), Schema = "Base")]
    public class User : IEntity, IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Display(Name = nameof(DisplayNames.MobileNumber), ResourceType = typeof(DisplayNames))]
        public long MobileNumber { get; set; }

        [Display(Name = nameof(DisplayNames.Gender), ResourceType = typeof(DisplayNames))]
        public Gender Gender { get; set; }

        [Display(Name = nameof(DisplayNames.Region), ResourceType = typeof(DisplayNames))]
        public RegionNames Region { get; set; }

        [Display(Name = nameof(DisplayNames.RefreshTokenExpireTime), ResourceType = typeof(DisplayNames))]
        public DateTime RefreshTokenExpireTime { get; set; }

        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.InsertDate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Display(Name = nameof(DisplayNames.Birthdate), ResourceType = typeof(DisplayNames))]
        public DateTime BirthdateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(DisplayNames.Birthdate), ResourceType = typeof(DisplayNames))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string BirthdateSh { get; set; }

        [Column(TypeName = "char(32)")]
        [Display(Name = nameof(DisplayNames.RefreshToken), ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string RefreshToken { get; set; }

        [Display(Name = nameof(DisplayNames.FirstName), ResourceType = typeof(DisplayNames))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FirstName { get; set; }

        [Display(Name = nameof(DisplayNames.LastName), ResourceType = typeof(DisplayNames))]
        [MaxLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Display(Name = nameof(DisplayNames.Email), ResourceType = typeof(DisplayNames))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Email { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Display(Name = nameof(DisplayNames.PushId), ResourceType = typeof(DisplayNames))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string PushId { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Display(Name = nameof(DisplayNames.ProfileImgUrl), ResourceType = typeof(DisplayNames))]
        [MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ProfileImgUrl { get; set; }



        [JsonIgnore]
        public ICollection<PushEndpoint> PushEndpoints { get; set; }

        [JsonIgnore]
        public ICollection<DiscountCode> DiscountCode { get; set; }

        [JsonIgnore]
        public ICollection<Ticket> Tickets { get; set; }

        [JsonIgnore]
        public ICollection<DrawChance> DrawChances { get; set; }

        [JsonIgnore]
        public ICollection<RoundWinner> RoundWinners { get; set; }

        [JsonIgnore]
        public ICollection<Purchase> Purchases { get; set; }

        [JsonIgnore]
        public ICollection<Notification> Notifications { get; set; }

        [JsonIgnore]
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
    }
}