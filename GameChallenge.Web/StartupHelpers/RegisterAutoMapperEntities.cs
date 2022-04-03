using AutoMapper;
using GameChallenge.Core.DBEntities;
using GameChallenge.Web.EnpointModel;
using GameChallenge.Web.Model;

namespace GameChallenge.Web
{
    public class RegisterAutoMapperEntities : Profile
    {
        public RegisterAutoMapperEntities()
        {
            CreateMap<Player, PlayerModel>();
        }
    }

    
}
