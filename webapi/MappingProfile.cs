using AutoMapper;
using webapi.DTO;
using webapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Team, TeamListDTO>();
            CreateMap<TeamListDTO, Team>();
            CreateMap<Team, CreateTeamDTO>();
            CreateMap<CreateTeamDTO, Team>();
            CreateMap<Game, CreateGameDTO>();
            CreateMap<CreateGameDTO, Game>();
            CreateMap<Game, GetGameDTO>();
            CreateMap<GetGameDTO, Game>();

            
        }
    }
}
