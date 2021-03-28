using AutoMapper;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class UsersService : IUsersService {
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper) {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public ServiceResponse<User> GetUserByLogin(UserLoginDTO userLogin) {
            var user = usersRepository.GetUserByLogin(userLogin.Login);

            if (user == null)
                return new ServiceErrorResponse<User>("Incorrect login");
            
            if (user.Password != userLogin.Password)
                return new ServiceErrorResponse<User>("Incorrect password");

            return new ServiceResponse<User>(user);
        }

        public ServiceResponse<User> RegisterUser(UserRegisterDTO userRegister) {
            if (string.IsNullOrWhiteSpace(userRegister.Password))
                return new ServiceErrorResponse<User>("Invalid password");
            
            if (string.IsNullOrWhiteSpace(userRegister.Login))
                return new ServiceErrorResponse<User>("Invalid login");

            var user = mapper.Map<User>(userRegister);

            if (!usersRepository.CreateUser(user))
                return new ServiceErrorResponse<User>("Login already occupied");

            usersRepository.SaveChanges();

            return new ServiceResponse<User>(user);
        }
    }
}