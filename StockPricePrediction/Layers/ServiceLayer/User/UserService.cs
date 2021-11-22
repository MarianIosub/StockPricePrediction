using System;
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

        public UserService(IUserRepository repository, IMapper mapper)
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
            if (_repository.GetByEmail(User.Email) != null)
                return false;
            if (Utils.IsValid(User))
            {
                User.Password = Utils.EncryptPassword(User.Password);
                if (_repository.Insert(User))
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateUser(User User)
        {
            var _user = GetUser(User.Id);
            if (_user != null)
            {
                _repository.Update(_user);
            }
        }

        public void DeleteUser(int id)
        {
            User User = GetUser(id);
            if (User != null)
            {
                _repository.Remove(User);
                _repository.SaveChanges();
            }
        }

        public UserResponseModel Authenticate(AuthenticateModel authenticateModel)
        {
            var user = _repository.GetByEmail(authenticateModel.Email);
            if (user == null)
                return null;
            if (user.Password.Equals(Utils.EncryptPassword(authenticateModel.Password)))
            {
                var userResponse = _mapper.Map<UserResponseModel>(user);
                return userResponse;
            }

            return null;
        }
    }
}