using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUserRepository _user;
        private IFoodRepository _food;
        private ICalorieCounterRepository _calorieCounter;
        private ICalorieRepository _calorie;
        
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public IFoodRepository Food
        {
            get
            {
                if (_food == null)
                {
                    _food = new FoodRepository(_repoContext);
                }
                return _food;
            }
        }

        public ICalorieCounterRepository CalorieCounter
        {
            get
            {
                if (_calorieCounter == null)
                {
                    _calorieCounter = new CalorieCounterRepository(_repoContext);
                }
                return _calorieCounter;
            }
        }

        public ICalorieRepository Calorie
        {
            get
            {
                if (_calorie == null)
                {
                    _calorie = new CalorieRepository(_repoContext);
                }
                return _calorie;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
