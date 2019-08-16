using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARS.BusinessEntity;

namespace TARS.BusinessService.Abstract
{
   public interface IAdminService
    {
        VictimView GetListOfVictimsProfile(AppFilter filter);
        DonorEntity GetListOfDonors(AppFilter filter);
        VictimPhotoEntity GetListOfVictimsPhotos(int victimId);
    }
}
