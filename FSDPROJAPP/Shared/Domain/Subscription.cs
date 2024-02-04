using System.ComponentModel.DataAnnotations;

namespace FSDPROJAPP.Shared.Domain
{
    public class Subscription : BaseDomainModel, IValidatableObject
    {
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public int profileId { get; set; }
        public virtual Profile? Profile { get; set; }
        //public int PreferenceId { get; set; }
        //public virtual Preference? Preference { get; set; }

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