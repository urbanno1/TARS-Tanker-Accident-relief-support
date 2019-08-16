using AutoMapper;
using TARS.BusinessEntity;
using TARS.DataModel.DataModel;

namespace TARS.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<State, StateModel>();
                cfg.CreateMap<Country, CountryModel>();
                cfg.CreateMap<Lga, LgaModel>();
                cfg.CreateMap<City, CityModel>();


                cfg.CreateMap<Victim, VictimEntity>();
                cfg.CreateMap<VictimPhoto, VictimPhotoEntity>();
            });
        }
    }
}