namespace FSDPROJAPP.Shared.Domain
{
    public class Preference : BaseDomainModel
    {
        public int AgeRange { get; set; }
        public int DistanceRange { get; set; }
        public string? relationshipType { get; set; }
        public string? preferedGender { get; set; }

        public virtual List<Subscription>? Subscriptions { get; set; }
    }
}