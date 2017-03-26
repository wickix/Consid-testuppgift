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

        public CompanyDba(Guid Id)
        {
            _companyObj = this.Read(Id);
        }


        private Company _companyObj;
        public Company CompanyObj { get { return _companyObj; } }


        public List<Company> List(int CurrentPage, int PageSize)
        {
            using (var db = new CompaniesDBEntities())
            {
                var query = db.Companies.Include(x => x.Stores).OrderBy(n => n.Name).Skip((CurrentPage - 1) * PageSize).Take(PageSize);
                return query.ToList();
            }
        }

        public List<Company> List()
        {
            using (var db = new CompaniesDBEntities())
            {
                var query = db.Companies.Include(x => x.Stores).OrderBy(n => n.Name);
                return query.ToList();
            }
        }

        //Finds a particular company
        public Company Read(Guid Id)
        {
            using (var db = new CompaniesDBEntities())
            {
                db.Companies.Load();  
                return db.Companies.Include(s => s.Stores).Where(x => x.Id == Id).First();
            }
        }

        public void Add(Company companyObject)
        {
            using (var db = new CompaniesDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Companies.Add(companyObject);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
        }

        public void Update(Company companyObject)
        {

            using (var db = new CompaniesDBEntities())
            {

                db.Companies.Attach(companyObject);
                db.Entry(companyObject).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(Company companyObject)
        {
            using (var db = new CompaniesDBEntities())
            {
                Company companyItem = db.Companies.Find(companyObject.Id);
                db.Companies.Remove(companyItem);
                db.SaveChanges();
            }
        }

        public int getNumberOfCompanies()
        {
            using (var db = new CompaniesDBEntities())
            {
                return db.Companies.Count();
            }
        }
    }
}
