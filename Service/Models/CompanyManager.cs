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
    public class CompanyManager
    {
        static private CompanyDba _companyDba = new CompanyDba();


        static public company getcompany(Guid Id)
        {
            return Mapper.Map<company>(_companyDba.Read(Id));
        }
            static public List<company> getCompanies()
        {
            return Mapper.Map<List<Company>, List<company>>(_companyDba.List());
        }

        static public void AddCompany(company companyObject)
        {
            _companyDba.Add(Mapper.Map <company, Company> (companyObject));
        }

        static public void UpdateCompany(company companyObject)
        {
            _companyDba.Update(Mapper.Map<company, Company>(companyObject));
        }

        static public void DeleteCompany(company companyObject)
        {
            _companyDba.Delete(Mapper.Map<company, Company>(companyObject));
        }
    }
}
