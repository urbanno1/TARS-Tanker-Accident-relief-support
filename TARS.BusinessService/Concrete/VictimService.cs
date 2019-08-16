using TARS.BusinessEntity;
using TARS.BusinessService.Abstract;
using System;
using System.Transactions;
using TARS.DataModel.UnitOfWork;
using TARS.DataModel.DataModel;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace TARS.BusinessService.Concrete
{
    public class VictimService : IVictimService
    {
        private readonly UnitOfWork _unitOfWork;
        public VictimService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public int InsertVictim(VictimModel insertData, string userId)
        {
            using (var scope = new TransactionScope())
            {
                var percentProfile = 0;
                if (insertData.Street != null && insertData.CityId != null
                    && insertData.IncidentType != null && insertData.HomeAddress != null
                    && insertData.IncidentDescription != null)
                {
                    percentProfile = 100;
                }
                else
                {
                    percentProfile = 20;
                }
                var victim = _unitOfWork.victimRepository.Get(c => c.VictimStatus == true && c.UserId == userId);
                if (victim != null)
                {
                    victim.UserId = userId;
                    victim.Street = insertData.Street;
                    victim.CityId = insertData.CityId;
                    victim.IncidentType = insertData.IncidentType;
                    victim.IncidentDate = insertData.IncidentDate;
                    victim.HomeAddress = insertData.HomeAddress;
                    victim.IncidentDescription = insertData.IncidentDescription;
                    victim.CreatedDate = DateTime.Now;
                    victim.VictimStatus = true;
                    victim.percentageProfile = percentProfile;
                    _unitOfWork.victimRepository.Update(victim);
                    _unitOfWork.Save();
                    scope.Complete();

                    return victim.VictimId;
                }
                else
                {
                    var city = _unitOfWork.CityRepository.GetFirst(c => c.CityStatus == true && c.CityId > 0).CityId;
                    var victimm = new Victim()
                    {
                        UserId = userId,
                        CityId = city,
                        CreatedDate = DateTime.Now,
                        VictimStatus = true,
                        percentageProfile = percentProfile,
                    };
                    _unitOfWork.victimRepository.Insert(victimm);
                    _unitOfWork.Save();
                    scope.Complete();

                    return victimm.VictimId;
                }

            }
        }

        public int InsertDonor(DonorEntityModel insertData)
        {
            using (var scope = new TransactionScope())
            {
                var donor = new Donor()
                {
                    DonatingFor = insertData.DonatingFor,
                    DonorFor = insertData.DonorFor,
                    FullName = insertData.FullName,
                    PhoneNumber = insertData.PhoneNumber,
                    Email = insertData.Email,
                    BankName = insertData.BankName,
                    TransationAmount = insertData.TransationAmount,
                    TransactionDate = insertData.TransactionDate,
                    TransactionId = insertData.TransactionId,
                    CreatedDate = DateTime.Now,
                    DonorStatus = true,
                };
                _unitOfWork.DonorRepository.Insert(donor);
                _unitOfWork.Save();
                scope.Complete();
                return donor.DonorId;
            }
        }

        public StateEntity GetStates()
        {
            var states = _unitOfWork.StateRepository.Find(c => c.StateStatus == true && c.Country.CountryCode == "NG").ToList();
            if (states.Count() > 0)
            {
                var stateEntity = new StateEntity()
                {
                    Data = Mapper.Map<IList<State>, IList<StateModel>>(states),
                };
                return stateEntity;
            }
            return null;
        }

        public LgaEntity GetLga(int studentId)
        {
            var lgas = _unitOfWork.LgaRepository.Find(c => c.LgaStatus == true && c.State.StateId == studentId).ToList();
            if (lgas.Count() > 0)
            {
                var lgaEntity = new LgaEntity()
                {
                    Data = Mapper.Map<IList<Lga>, IList<LgaModel>>(lgas),
                };
                return lgaEntity;
            }
            return null;
        }

        public CityEntity GetCity(int lgaId)
        {
            var city = _unitOfWork.CityRepository.Find(c => c.CityStatus == true && c.Lga.LgaId == lgaId).ToList();
            if (city.Count() > 0)
            {
                var cityEntity = new CityEntity()
                {
                    Data = Mapper.Map<IList<City>, IList<CityModel>>(city),
                };
                return cityEntity;
            }
            return null;
        }

        public void UploadImage(List<Tuple<string, string>> photoUrl, string userId)
        {
            using (var scope = new TransactionScope())
            {
                foreach (var url in photoUrl)
                {
                    var victimId = _unitOfWork.victimRepository.Get(c => c.VictimStatus == true && c.UserId == userId);
                    var victimPhoto = _unitOfWork.victimPhotoRepository.Get(c => c.VictimId == victimId.VictimId
                    && c.PhotoTitle.ToLower().Trim() == url.Item2.ToLower().Trim());
                    if (victimPhoto != null)
                    {
                        victimPhoto.PhotoTitle = url.Item2;
                        victimPhoto.PhotoUrl = url.Item1;
                        victimPhoto.CreatedDate = DateTime.Now;
                        victimPhoto.PhotoStatus = true;

                        _unitOfWork.victimPhotoRepository.Update(victimPhoto);
                    }
                    else
                    {
                        var photo = new VictimPhoto()
                        {
                            VictimId = victimId.VictimId,
                            PhotoUrl = url.Item1,
                            PhotoTitle = url.Item2,
                            CreatedDate = DateTime.Now,
                            PhotoStatus = true,
                        };
                        _unitOfWork.victimPhotoRepository.Insert(photo);
                    }
                }
                _unitOfWork.Save();
                scope.Complete();

            }
        }

        public DashboardEntity GetDashboard(string userId)
        {
            var victimProfileCount = 0;
            var victimProfilePer = 0;
            var totalProfile = 0;
            var totalPhoto = 0;
            var averageProfilePercentage = 0;
            var averagePhotoPercentage = 0;
            var victimPhotoPer = 0;
            var donotTotal = 0;


            var victimProfileC = _unitOfWork.victimProfileRepository.Find(c => c.VictimStatus == true);
            if (victimProfileC != null)
            {
                victimProfileCount = victimProfileC.Count();
            }

            var victimProfilePr = _unitOfWork.victimPercProfileRepository.GetFirst(c => c.PercentageSum > 0);
            if (victimProfilePr != null)
            {
                victimProfilePer = (int)victimProfilePr.PercentageSum;
            }

            var victimPhotoPr = _unitOfWork.victimPercPhotoRepository.Find(c => c.PhotoStatus == true);
            if (victimPhotoPr != null)
            {
                victimPhotoPer = (int)victimPhotoPr.Count();
            }

            var donotTot = _unitOfWork.DonorRepository.Find(c => c.DonorStatus == true);
            if (donotTot != null)
            {
                donotTotal = donotTot.Count();
            }

            totalProfile = victimProfileCount;
            totalPhoto = (int)Math.Ceiling((decimal)victimPhotoPer / 5);
            averageProfilePercentage = PercentageGeneralProfile(victimProfileCount, victimProfilePer);
            averagePhotoPercentage = PercentageGeneralPhoto(victimProfileCount, victimPhotoPer);

            var victim = _unitOfWork.victimRepository.Get(c => c.VictimStatus == true && c.UserId == userId);
            if (victim != null)
            {
                var percentagePhoto = 0;
                var victimMap = Mapper.Map<Victim, VictimModel>(victim);
                var percentagePhot = _unitOfWork.victimPhotoRepository.Find(c => c.PhotoStatus == true && c.VictimId == victim.VictimId);
                if(percentagePhot != null)
                {
                    percentagePhoto = percentagePhot.Count(); ;

                }
                var dash1 = new DashboardEntity();
                var percentProfile = 20;

                if (victimMap.Street != null && victimMap.CityId != null
                    && victimMap.IncidentType != null && victimMap.HomeAddress != null
                    && victimMap.IncidentDescription != null)
                    percentProfile = 100;
                else if (victimMap.Street == null && victimMap.CityId != null
                    && victimMap.IncidentType != null && victimMap.HomeAddress != null
                    && victimMap.IncidentDescription != null)
                    percentProfile = 80;
                else if (victimMap.HomeAddress == null && victimMap.Street != null && victimMap.CityId != null
                    && victimMap.IncidentType != null && victimMap.IncidentDescription != null)
                    percentProfile = 80;

                switch (percentagePhoto)
                {
                    case 0:
                        dash1.PercentagePhoto = 0;
                        break;
                    case 1:
                        dash1.PercentagePhoto = 20;
                        break;
                    case 2:
                        dash1.PercentagePhoto = 40;
                        break;
                    case 3:
                        dash1.PercentagePhoto = 60;
                        break;
                    case 4:
                        dash1.PercentagePhoto = 80;
                        break;
                    case 5:
                        dash1.PercentagePhoto = 100;
                        break;
                }

                var dash = new DashboardEntity()
                {
                    PercentageProfile = percentProfile,
                    PercentagePhoto = dash1.PercentagePhoto,
                };
                return dash;
            }
            else
            {
                var dash = new DashboardEntity()
                {
                    PercentageGeneralPercentageProfile = averageProfilePercentage,
                    PercentageGeneralPhoto = averagePhotoPercentage,
                    TotalGeneralProfile = totalProfile,
                    TotalGeneralPhoto = totalPhoto,
                    TotalGeneralDonors = donotTotal,
                };
                return dash;
            }
        }

        public VictimEntity LoadVictimProfile(string userId)
        {
            if (userId != null)
            {
                var victim = _unitOfWork.victimRepository.Get(c => c.VictimStatus == true && c.UserId == userId);
                if (victim.CityId != null)
                {
                    var city = _unitOfWork.CityRepository.Get(c => c.CityStatus == true && c.CityId == victim.CityId);
                    var lga = _unitOfWork.LgaRepository.Get(c => c.LgaStatus == true && c.LgaId == city.LgaId);
                    var state = _unitOfWork.StateRepository.Get(c => c.StateStatus == true && c.StateId == lga.StateId);
                    var data = new VictimMod()
                    {
                        Victim = Mapper.Map<Victim, VictimModel>(victim),
                        State = Mapper.Map<State, StateModel>(state),
                        City = Mapper.Map<City, CityModel>(city),
                        Lga = Mapper.Map<Lga, LgaModel>(lga),
                    };

                    var dataModel = new VictimEntity()
                    {
                        Data = data,
                    };
                    return dataModel;
                }
            }
            return null;
        }

        public VictimPhotoEntity LoadVictimPhoto(string userId)
        {
            var victimPhotos = _unitOfWork.victimPhotoRepository.Find(c => c.PhotoStatus == true && c.Victim.UserId == userId).ToList();
            if (victimPhotos.Count() > 0)
            {
                var photo = new VictimPhotoEntity()
                {
                    Data = Mapper.Map<IList<VictimPhoto>, IList<VictimPhotoModel>>(victimPhotos),
                    Records = victimPhotos.Count(),
                };
                return photo;
            }
            return null;
        }


        public string GetVictimPhoto(string photoUrl)
        {
            var victimPhoto = _unitOfWork.victimPhotoRepository.Get(c => c.PhotoUrl == photoUrl && c.PhotoStatus == true);
            if (victimPhoto != null)
            {
                var photo = Mapper.Map<VictimPhoto, VictimPhotoModel>(victimPhoto);
                var photoUrlLink = photo.PhotoUrl;
                var stringCar = photoUrlLink.Split('/');
                var photoName = stringCar[2];
                return photoName;
            }
            return null;
        }

        public string getImageName(string fileName)
        {
            if (fileName != null)
            {
                var name = "";
                if (fileName.ToLower().Trim().Contains("certificate"))
                {
                    name = "Death Certificate";
                    return name;
                }
                else if (fileName.ToLower().Trim().Contains("police"))
                {
                    name = "Police Report";
                    return name;
                }
                else if (fileName.ToLower().Trim().Contains("medical"))
                {
                    name = "Medical Report";
                    return name;
                }

                else if (fileName.ToLower().Trim().Contains("affidavit"))
                {
                    name = "Affidavit";
                    return name;
                }
                else if (fileName.ToLower().Trim().Contains("accident"))
                {
                    name = "Accident Scene";
                    return name;
                }

            }
            return null;
        }

    
        public static int PercentageGeneralProfile(int profileCount, int sumPercentage)
        {
            if (profileCount > 0 && sumPercentage > 0)
            {
                var averagePerc = (int)Math.Round((double)sumPercentage / profileCount);
                return averagePerc;
            }
            return 0;
        }

        public static int PercentageGeneralPhoto(int profileCount, int sumphoto)
        {
            if (sumphoto > 0)
            {
                var photoPerc = ((double)sumphoto / 5);
                var averagePerc = (int)Math.Round(((decimal)photoPerc / profileCount) * (100));
                return averagePerc;
            }
            return 0;
        }
    }
}
