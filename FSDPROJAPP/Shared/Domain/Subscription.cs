namespace FSDPROJAPP.Shared.Domain
{
    public class Subscription : BaseDomainModel
    {
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public int profileId { get; set; }
        public virtual Profile? Profile { get; set; }
        public int PreferenceId { get; set; }
        public virtual Preference? Preference { get; set; }

    }
}