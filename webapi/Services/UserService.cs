using AutoMapper;
using webapi.Context;

namespace webapi.Services
{
    public class UserService : IUserService
    {
        private OnlabProjectContext dB;
        private readonly IMapper mapper;

        public UserService(OnlabProjectContext _dB, IMapper _mapper)
        {
            dB = _dB;
            mapper = _mapper;
        }

        public string getUserName(string id)
        {
            return dB.Users.FirstOrDefault(u => u.Id == id).UserName;
        }
    }
}
