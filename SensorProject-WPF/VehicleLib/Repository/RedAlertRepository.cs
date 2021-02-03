using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleLib.Interface;

namespace VehicleLib.Repository
{
    public class RedAlertRepository:Repository<RedAlert>, IRedAlertRepository
    {
        public RedAlertRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
