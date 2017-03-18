using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.EntityModel;
using AutoMapper;
using Service.Configuration;

namespace Service.Models
{
    public class ListStoresModel
    {
        public List<store> Items { get; set; }
        public Pagination Pager { get; set; }
    }

    public class StoreManager

    {
        static private StoreDba _storeDba = new StoreDba();


        static public store getStore(Guid Id)
        {
            return Mapper.Map<store>(_storeDba.Read(Id));
        }


        static public List<store> getStores(Pagination pager)
        {
            return Mapper.Map<List<Store>, List<store>>(_storeDba.List(pager.CurrentPage, pager.PageSize));
        }

        static public void AddStore(store storeObject)
        {
            _storeDba.Add(Mapper.Map<store, Store>(storeObject));
        }

        static public void UpdateStore(store storeObject)
        {
            _storeDba.Update(Mapper.Map<store, Store>(storeObject));
        }

        static public void DeleteStore(store storeObject)
        {
            _storeDba.Delete(Mapper.Map<store, Store>(storeObject));
        }

        static public int getNumberOfStores()
        {
           return  _storeDba.getNumberOfStores();
        }
    }
}
