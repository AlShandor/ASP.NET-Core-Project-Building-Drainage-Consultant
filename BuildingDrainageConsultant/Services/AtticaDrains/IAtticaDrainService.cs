﻿namespace BuildingDrainageConsultant.Services.AtticaDrains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;

    public interface IAtticaDrainService
    {
        public IEnumerable<AtticaDrainServiceModel> All(string searchTerm);

        public AtticaDrainServiceModel Details(int id);

        public int Create(
            string name,
            double flowRate,
            int drainageArea,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart);

        public bool Edit(
            int id,
            string name,
            double flowRate,
            int drainageArea,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart);

        public bool Delete(int id);

        public IEnumerable<AtticaDetailServiceModel> GetAtticaDetails();

        public IEnumerable<AtticaPartServiceModel> GetAtticaParts();

        public AtticaDetailServiceModel GetAtticaDetailById(int id);

    }
}