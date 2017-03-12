using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repository.EntityModel
{
    public class StoreDba
    {
        public StoreDba() { }

        public StoreDba(string Id)
        {
            _storeObj = this.Read(Id);
        }


        private Store _storeObj;
        public Store StoreObj { get { return _storeObj; } }


        public List<Store> List()
        {
            using (var db = new CompaniesDBEntities())
            {
                var query = db.Stores.Include(x => x.Company).OrderBy(n => n.Name);
                return query.ToList();
            }
        }

        public Store Read(string Id)  //Finds a particular company
        {
            using (var db = new CompaniesDBEntities())
            {
                //db.Companies.Load();  fråga
                return db.Stores.Find(Id);
            }
        }

    }
}
