using System.Collections.Generic;
using AutoMapper;
using DomainLayer;
using RepositoryLayer;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class UserService : IUserService
    {
        #region Property

        private IUserRepository _repository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UserService(IUserRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        public IEnumerable<User> GetAllUsers()
        {
            return _repository.GetAll();
        }

        public User GetUser(int id)
        {
            return _repository.Get(id);
        }

        public bool InsertUser(User User)
        {
            if (Utils.IsValid(User))
            {
                if (_repository.Insert(User))
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateUser(User User)
        {
            _repository.Update(User);
        }

        public void DeleteUser(int id)
        {
            User User = GetUser(id);
            _repository.Remove(User);
            _repository.SaveChanges();
        }

        public UserResponseModel Authenticate(AuthenticateModel authenticateModel)
        {
            var user = _repository.GetFirst(authenticateModel.Email);
            if (user.Password.Equals(authenticateModel.Password))
            {
                var userResponse = _mapper.Map<UserResponseModel>(user);
                return userResponse;
            }

            return null;
        }
    }
}