using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IFoodRepository Food { get; }
        IFoodDiaryRepository FoodDiary { get; }
        INutrientRepository Nutrient { get; }
        void Save();
	}
}
