namespace BuildingDrainageConsultant.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public ICollection<Drain> Drains { get; set; } = new List<Drain>();

        public ICollection<SafeDrain> SafeDrains { get; set; } = new List<SafeDrain>();

        public ICollection<AtticaDrain> AtticaDrains { get; set; } = new List<AtticaDrain>();
    }
}
