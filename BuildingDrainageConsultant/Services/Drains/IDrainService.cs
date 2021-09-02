﻿namespace BuildingDrainageConsultant.Services.Drains
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using BuildingDrainageConsultant.Services.Images.Models;
    using System.Collections.Generic;

    public interface IDrainService
    {
        public DrainQueryServiceModel All(
             string searchTerm,
             DrainDirectionEnum direction,
             DrainDiameterEnum diameter,
             DrainVisiblePartEnum visiblePart,
             DrainWaterproofingEnum waterproofing,
             DrainHeatingEnum heating,
             DrainRenovationEnum renovation,
             DrainFlapSealEnum flapSeal,
             DrainSortingEnum sorting,
             int currentPage,
             int drainsPerPage);

        public DrainServiceModel Details(int id);

        public int Create(
            string name,
            double flowRate,
            int drainageArea,
            int depth,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            int? imageId,
            string description);

        public bool Edit(
            int id,
            string name,
            double flowRate,
            int drainageArea,
            int depth,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            int? imageId,
            string description);

        public bool Delete(int id);

        public IEnumerable<DrainDetailsServiceModel> ByUser(string userId);

        public bool AddToMine(string userId, int drainId);

        public bool RemoveFromMine(string userId, int drainId);

        public bool IsMyDrain(int drainId, string userId);

        public void CreateAll(Drain[] drains);
    }
}