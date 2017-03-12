using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Repository
{
    public partial class CompaniesDBEntities : DbContext
    {
        static CompaniesDBEntities()
        {
            var foo = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }


    }
}
