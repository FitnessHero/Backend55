using Contracts;
using Entities;
using Entities.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CalorieRepository : RepositoryBase<Calorie>, ICalorieRepository
    {
        public CalorieRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
