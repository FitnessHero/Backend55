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
        ICalorieCounterRepository CalorieCounter { get; }
        ICalorieRepository Calorie { get; }
        void Save();
    }
}
