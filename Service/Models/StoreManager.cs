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
    public class StoreManager
    {
        static private StoreDba _storeDba = new StoreDba();


        static public store getStore(Guid Id)
        {
            return Mapper.Map<store>(_storeDba.Read(Id));
        }


        static public List<store> getStores()
        {
            return Mapper.Map<List<Store>, List<store>>(_storeDba.List());
        }

        static public void AddStore(store storeObject)
        {
            _storeDba.Add(Mapper.Map<store, Store>(storeObject));
        }

        static public void UpdateStore(store storeObject)
        {
            _storeDba.Update(Mapper.Map<store, Store>(storeObject));
        }
    }
}
