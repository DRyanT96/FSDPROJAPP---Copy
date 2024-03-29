﻿using FSDPROJAPP.Shared.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FSDPROJAPP.Shared.Domain
{
    public class Subscription : BaseDomainModel, IValidatableObject
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }
        public DateTime? DateIn { get; set; }
        [Required]
        public int? ProfileId { get; set; }
        public virtual Profile? Profile { get; set; }
        
        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //throw new NotImplementedException();

            if (DateIn != null)
            {
                if (DateIn <= DateOut)
                {
                    yield return new ValidationResult("DateIn must be greater than DateOut", new[] { "DateIn" });
                }
            }
        }
    }
}
