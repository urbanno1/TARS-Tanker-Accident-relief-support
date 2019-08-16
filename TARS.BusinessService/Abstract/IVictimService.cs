using TARS.BusinessEntity;
using System.Collections.Generic;
using System;

namespace TARS.BusinessService.Abstract
{
    public interface IVictimService
    {
        int InsertVictim(VictimModel insertData, string userId);
        int InsertDonor(DonorEntityModel insertData);
        StateEntity GetStates();
        LgaEntity GetLga(int studentId);
        CityEntity GetCity(int lgaId);
        void UploadImage(List<Tuple<string, string>> photoUrl, string userId);
        DashboardEntity GetDashboard(string userId);
        VictimEntity LoadVictimProfile(string userId);
        VictimPhotoEntity LoadVictimPhoto(string userId);
        string GetVictimPhoto(string photoUrl);
        string getImageName(string fileName);

    }
}
