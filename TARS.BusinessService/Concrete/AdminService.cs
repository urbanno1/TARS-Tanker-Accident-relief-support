using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TARS.BusinessEntity;
using TARS.BusinessService.Abstract;
using TARS.DataModel.DataModel;
using TARS.DataModel.UnitOfWork;

namespace TARS.BusinessService.Concrete
{
    public class AdminService : IAdminService
    {

        private readonly UnitOfWork _unitOfWork;

        public AdminService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public VictimView GetListOfVictimsProfile(AppFilter filterApp)
        {
            var victimList = _unitOfWork.victimProfileRepository.Find(c => c.VictimStatus == true)
                .Where(c => filterApp.filters.rules[0].Data == ""
                ||c.LastName.Trim().Contains(filterApp.filters.rules[0].Data.Trim())
                || c.LastName.Trim().Contains(filterApp.filters.rules[0].Data.Trim()));

            if (victimList.Count() > 0)
            {
                var victims = victimList
                    .OrderBy(c => filterApp.sidx)
                    .Skip((filterApp.page - 1) * filterApp.rows)
                .Take(filterApp.rows)
                .ToList();


                var victimsView = new VictimView()
                {
                    Data = Mapper.Map<List<VictimProfile>, List<VictimViewModel>>(victims),
                    Records = victimList.Count(),
                };
                return victimsView;
            }

            return new VictimView();
        }

        public VictimPhotoEntity GetListOfVictimsPhotos(int victimId)
        {
            var victimPhotos = _unitOfWork.victimPhotoRepository.Find(c => c.PhotoStatus == true && c.VictimId == victimId).ToList();
            if (victimPhotos.Count() > 0)
            {
                var victimPhoto = new VictimPhotoEntity()
                {
                    Data = Mapper.Map<List<VictimPhoto>, List<VictimPhotoModel>>(victimPhotos),
                };
                return victimPhoto;
            }
            return null;
        }

        public DonorEntity GetListOfDonors(AppFilter filter)
        {
            var donors = _unitOfWork.DonorRepository.Find(c => c.DonorStatus == true).ToList();
            if (donors.Count() > 0)
            {
                var donorProfiles = new DonorEntity()
                {
                    Data = Mapper.Map<List<Donor>, List<DonorEntityModel>>(donors),
                    Records = donors.Count,
                };
                return donorProfiles;
            }
            return null;
        }


    }



    //    public StateEntity GetStates(AppFilter filterApp)
    //    {
    //        var stateList = _unitOfWork.StateRepository.Find(c => c.State_Status == true)
    //            .Where(c=> c.Country.Country__Code == filterApp.filters.rules[0].Data && filterApp.filters.rules[1].Data == "" 
    //            || c.Country.Country__Code == filterApp.filters.rules[0].Data 
    //            && c.State_Name.ToLower().Trim().Contains(filterApp.filters.rules[1].Data.ToLower().Trim()));

    //        if(stateList.Count() > 0)
    //        {
    //            var states = stateList
    //            .OrderBy(c => filterApp.sidx)
    //            .Skip((filterApp.page - 1) * filterApp.rows)
    //            .Take(filterApp.rows)
    //            .ToList();

    //            var dataState = Mapper.Map<List<State>, List<StateModel>>(states);

    //            var stateEntity = new StateEntity()
    //            {
    //                Data = dataState,
    //                Records = stateList.Count(),
    //            };
    //            return stateEntity;
    //        }
    //        return new StateEntity();
    //    }

    //    #endregion

}

