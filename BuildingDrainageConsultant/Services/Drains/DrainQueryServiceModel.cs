namespace BuildingDrainageConsultant.Services.Drains
{
    using System.Collections.Generic;
    public class DrainQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int DrainsPerPage { get; init; }

        public int TotalDrains { get; init; }

        public IEnumerable<DrainServiceModel> Drains { get; init; }
}
}
