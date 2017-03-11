using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; 

namespace Repository.EntityModel
{
    public class CompanyDba
    {
        public CompanyDba() { }

        public CompanyDba(string Id)
        {
            _companyObj = this.Read(Id);
        }


        private Company _companyObj;
        public Company CompanyObj { get { return _companyObj; } }


        public List<Company> List()
        {
            using (var db = new CompaniesDBEntities())
            {
                var query = db.Companies.OrderBy(x => x.Name);
                return query.ToList();
            }
        }

        public Company Read(string Id)  //Finds a particular company
        {
            using (var db = new CompaniesDBEntities())
            {
                //db.Companies.Load();  fråga
                return db.Companies.Find(Id);
            }
        }
    }
}
