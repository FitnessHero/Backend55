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
        private IFoodDiaryRepository _foodDiary;
        private INutrientRepository _calorie;
        
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

        public IFoodDiaryRepository CalorieCounter
        {
            get
            {
                if (_foodDiary == null)
                {
                    _foodDiary = new FoodDiaryRepository(_repoContext);
                }
                return _foodDiary;
            }
        }

        public INutrientRepository Nutrient
		{
            get
            {
                if (_calorie == null)
                {
                    _calorie = new NutrientRepository(_repoContext);
                }
                return _calorie;
            }
        }

		public IFoodDiaryRepository FoodDiary
        {
			get
			{
				if (_foodDiary == null)
				{
					_foodDiary = new FoodDiaryRepository(_repoContext);
				}
				return _foodDiary;
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
