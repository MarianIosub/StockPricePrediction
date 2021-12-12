using System.Collections.Generic;
using System.Linq;
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

        public ApiResponse<IEnumerable<User>> GetAllUsers()
        {
            return ApiResponse<IEnumerable<User>>.Success(_repository.GetAll());
        }

        public ApiResponse<User> GetUser(int id)
        {
            return id < 0 ? ApiResponse<User>.Fail("Invalid id") : ApiResponse<User>.Success(_repository.Get(id));
        }

        public ApiResponse<bool> InsertUser(User user)
        {
            if (_repository.GetByEmail(user.Email) != null)
                return ApiResponse<bool>.Fail("Invalid user");
            if (!Utils.IsValid(user)) return ApiResponse<bool>.Fail("Failed");
            user.Password = Utils.EncryptPassword(user.Password);
            return _repository.Insert(user) ? ApiResponse<bool>.Success(true) : ApiResponse<bool>.Fail("Failed");
        }

        public ApiResponse<bool> UpdateUser(User user)
        {
            var actualUser = GetUser(user.Id).Data;
            if (actualUser == null) return ApiResponse<bool>.Fail("Invalid User");
            _repository.Update(actualUser);
            return ApiResponse<bool>.Success(true);
        }

        public ApiResponse<bool> DeleteUser(int id)
        {
            var user = GetUser(id).Data;
            if (user == null) return ApiResponse<bool>.Fail("Invalid id");
            _repository.Remove(user);
            _repository.SaveChanges();
            return ApiResponse<bool>.Success(true);
        }

        public ApiResponse<bool> Exists(User user)
        {
            var foundUser = _repository.GetByEmail(user.Email);
            return foundUser == null ? ApiResponse<bool>.Fail("User not found") : ApiResponse<bool>.Success(true);
        }

        public ApiResponse<IEnumerable<Stock>> GetFavouriteStocks(User user)
        {
            List<Stock> stocks = null;
            if (user == null) return ApiResponse<IEnumerable<Stock>>.Fail("Invalid user");
            var stockIds = _repository.GetFavouriteStocks(user);
            stocks = stockIds.Select(id => _stockRepository.Get(id)).ToList();

            return ApiResponse<IEnumerable<Stock>>.Success(stocks);
        }

        public ApiResponse<bool> RemoveFavouriteStock(User user, string stockSymbol)
        {
            var stock = _stockRepository.GetBySymbol(stockSymbol);
            if (user == null) return ApiResponse<bool>.Fail("Invalid user");
            _repository.RemoveFavouriteStock(user, stock);
            _repository.SaveChanges();
            return ApiResponse<bool>.Success(true);
        }

        public ApiResponse<bool> AddFavouriteStock(User user, string stockSymbol)
        {
            var stock = _stockRepository.GetBySymbol(stockSymbol);
            if (user == null || stock == null)
            {
                return ApiResponse<bool>.Fail("Invalid stock or user");
            }

            _repository.AddFavouriteStock(user, stock);
            _repository.SaveChanges();
            return ApiResponse<bool>.Success(true);
        }

        public ApiResponse<int?> ValidateUser(string token)
        {
            return token == null
                ? ApiResponse<int?>.Fail("Invalid token")
                : ApiResponse<int?>.Success(JwtConfig.ValidateToken(token));
        }

        public ApiResponse<UserResponseModel> Authenticate(AuthenticateModel authenticateModel)
        {
            var user = _repository.GetByEmail(authenticateModel.Email);
            if (user == null)
            {
                return ApiResponse<UserResponseModel>.Fail("Invalid user");
            }

            if (!user.Password.Equals(Utils.EncryptPassword(authenticateModel.Password)))
                return ApiResponse<UserResponseModel>.Fail("Failed to authenticate");
            var userResponse = _mapper.Map<UserResponseModel>(user);
            return ApiResponse<UserResponseModel>.Success(userResponse);
        }
    }
}