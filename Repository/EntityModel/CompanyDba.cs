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


        public List<Company> List()
        {
            using (var db = new CompaniesDBEntities())
            {
                var query = db.Companies.Include(x => x.Stores).OrderBy(n => n.Name);
                return query.ToList();
            }
        }

        public Company Read(Guid Id)  //Finds a particular company
        {
            using (var db = new CompaniesDBEntities())
            {
                //db.Companies.Load();  fråga
                return db.Companies.Include(s => s.Stores).Where(x => x.Id == Id).First();
            }
        }

        public void Add(Company companyObject)
        {
            using (var db = new CompaniesDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction()) // Starts a transaction
                {
                    db.Companies.Add(companyObject);  // Prepare query  
                    db.SaveChanges();   // Run the query
                    transaction.Commit();   //  Permanent the result, writing to disc and closing transaction
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
                //companyObject.Stores.Clear();
                //foreach (var store in companyObject.Stores)
                //{
                //    companyObject.Stores.Clear(store);
                //   // db.Stores.Remove(store);
                //}
              //  StoreDba storeDba = new StoreDba();
                Company companyItem = db.Companies.Find(companyObject.Id);
              /*  List<Store> listStore = companyItem.Stores.ToList();
                int size = companyItem.Stores.Count();


                for (int i = size - 1; i >= 0; i--)
                {

                    companyItem.Stores.Remove(companyItem.Stores.ToList()[i]);
                    storeDba.Delete(listStore[i]);
                    listStore.RemoveAt(i);


                }*/
                //companyItem.Stores.Clear();

                //foreach (var store in companyItem.Stores)
                //{
                //    companyItem.Stores.Remove(store);
                //    storeDba.Delete(store);
                //}

                
                //db.Companies.Attach(companyItem);

               // db.Stores.Load();
               // db.Companies.Load();
                db.Companies.Remove(companyItem);
               // db.Entry(companyItem).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}
