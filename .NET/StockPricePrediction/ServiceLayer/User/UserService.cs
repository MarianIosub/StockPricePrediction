using System.Collections.Generic;
using DomainLayer;
using RepositoryLayer;

namespace ServiceLayer
{
    public class UserService : IUserService
    {
        #region Property

        private IUserRepository _repository;

        #endregion

        #region Constructor

        public UserService(IUserRepository repository)
        {
            _repository = repository;
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

        public void InsertUser(User User)
        {
            _repository.Insert(User);
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
    }
}