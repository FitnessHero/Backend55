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
    public class NutrientRepository : RepositoryBase<Nutrient>, INutrientRepository
    {
        public NutrientRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
