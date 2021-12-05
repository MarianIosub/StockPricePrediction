﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DomainLayer;
using RepositoryLayer;

namespace ServiceLayer
{
    public class UserService : IUserService
    {
        #region Property

        private IUserRepository _repository;
        private IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UserService(IUserRepository repository, IMapper mapper, IStockRepository stockRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _stockRepository = stockRepository;
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

        public bool InsertUser(User user)
        {
            if (_repository.GetByEmail(user.Email) != null)
                return false;
            if (Utils.IsValid(user))
            {
                user.Password = Utils.EncryptPassword(user.Password);
                if (_repository.Insert(user))
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateUser(User user)
        {
            var actualUser = GetUser(user.Id);
            if (actualUser != null)
            {
                _repository.Update(actualUser);
            }
        }

        public void DeleteUser(int id)
        {
            User user = GetUser(id);
            if (user != null)
            {
                _repository.Remove(user);
                _repository.SaveChanges();
            }
        }

        public bool Exists(User user)
        {
            return _repository.GetByEmail(user.Email) is not null;
        }

        public IEnumerable<Stock> GetFavouriteStocks(User user)
        {
            List<Stock> stocks = null;
            if (user != null)
            {
                var stockIds = _repository.GetFavouriteStocks(user);
                stocks = stockIds.Select(id => _stockRepository.Get(id)).ToList();
            }

            return stocks;
        }

        public void RemoveFavouriteStock(User user, string stockSymbol)
        {
            var stock = _stockRepository.GetBySymbol(stockSymbol);
            if (user != null)
            {
                _repository.RemoveFavouriteStock(user, stock);
                _repository.SaveChanges();
            }
        }

        public void AddFavouriteStock(User user, string stockSymbol)
        {
            var stock = _stockRepository.GetBySymbol(stockSymbol);
            if (user != null && stock != null)
            {
                _repository.AddFavouriteStock(user, stock);
                _repository.SaveChanges();
            }
        }

        public int? ValidateUser(string token)
        {
            return JwtConfig.ValidateToken(token);
        }

        public UserResponseModel Authenticate(AuthenticateModel authenticateModel)
        {
            var user = _repository.GetByEmail(authenticateModel.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Password.Equals(Utils.EncryptPassword(authenticateModel.Password)))
            {
                var userResponse = _mapper.Map<UserResponseModel>(user);
                return userResponse;
            }

            return null;
        }
    }
}