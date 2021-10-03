using AutoMapper;
using ProjectN.Models;
using ProjectN.Parameter;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Dtos.ResultModel;

namespace ProjectN.Mappings
{
    /// <summary>
    /// Controller Mappings
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ControllerMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerMappings"/> class.
        /// </summary>
        public ControllerMappings()
        {
            // Parameter -> Info
            this.CreateMap<CardParameter, CardInfo>();
            this.CreateMap<CardSearchParameter, CardSearchInfo>();

            // ResultModel -> ViewModel
            this.CreateMap<CardResultModel, CardViewModel>();
        }
    }
}
