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
    public class CalorieCounterRepository : RepositoryBase<CalorieCounter>, ICalorieCounterRepository
    {
        public CalorieCounterRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
