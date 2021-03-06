﻿using System;
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

        public StoreDba(Guid Id)
        {
            _storeObj = this.Read(Id);
        }


        private Store _storeObj;
        public Store StoreObj { get { return _storeObj; } }


        public List<Store> List(int CurrentPage, int PageSize)
        {
            using (var db = new CompaniesDBEntities())
            {
                var query = db.Stores.Include(x => x.Company).OrderBy(n => n.Name).Skip((CurrentPage - 1) * PageSize).Take(PageSize);
                return query.ToList();
            }
        }

        //Finds a particular store
        public Store Read(Guid Id) 
        {
            using (var db = new CompaniesDBEntities())
            {
                db.Companies.Load();
                return db.Stores.Include(x => x.Company).Where(y => y.Id == Id).First();
            }
        }

        public void Add(Store storeObject)
        {
            using (var db = new CompaniesDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Stores.Add(storeObject); 
                    db.SaveChanges(); 
                    transaction.Commit();
                }
            }
        }

        public void Update(Store storeObject)
        {

            using (var db = new CompaniesDBEntities())
            {

                db.Stores.Attach(storeObject);
                db.Entry(storeObject).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(Store storeObject)
        {
            using (var db = new CompaniesDBEntities())
            {
                Store storeItem = db.Stores.Find(storeObject.Id);
              
                db.Stores.Remove(storeItem);
                db.Entry(storeItem).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public int getNumberOfStores()
        {
            using (var db = new CompaniesDBEntities())
            {
                return db.Stores.Count();
            }
        }

    }
}
