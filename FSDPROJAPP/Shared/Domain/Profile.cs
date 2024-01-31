using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDPROJAPP.Shared.Domain
{
    public class Profile : BaseDomainModel
    {
        public string? Gender { get; set; }
        public string? Name { get; set; }
        public int Phonenumber {  get; set; }

        public int? DetailId { get; set; }
        public virtual Detail? Detail { get; set; }
        public int? DislikeId { get; set; }
        public virtual Dislike? Dislike { get; set; }
        public int? HobbyId { get; set; }
        public virtual Hobby? Hobby { get; set; }
        public int? LikeId { get; set; }
        public virtual Like? Like { get; set; }

        public virtual List<Subscription>? Subscriptions { get; set; }
    }
}
