using AutoMapper;
using ProjectN.Repository.Dtos.Condition;
using ProjectN.Repository.Dtos.DataModel;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Dtos.ResultModel;

namespace ProjectN.Service.Mappings
{
    /// <summary>
    /// Service Mappings
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ServiceMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceMappings"/> class.
        /// </summary>
        public ServiceMappings()
        {
            // Info -> Condition
            this.CreateMap<CardInfo, CardCondition>();
            this.CreateMap<CardSearchInfo, CardSearchCondition>();

            // DataModel -> ResultModel
            this.CreateMap<CardDataModel, CardResultModel>();
        }
    }
}
