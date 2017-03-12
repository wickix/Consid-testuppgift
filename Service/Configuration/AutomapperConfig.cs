using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repository;
using Service.Models;

namespace Service.Configuration
{
    public static class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ToCompanyProfile());
                cfg.AddProfile(new FromCompanyProfile());
                cfg.AddProfile(new ToStoreProfile());
                cfg.AddProfile(new FromStoreProfile());
            });
        }

    }

    public class ToCompanyProfile : Profile
    {
        public ToCompanyProfile()
        {
            CreateMap<Company, company>().MaxDepth(3);
        }
    }
    public class FromCompanyProfile : Profile
    {
        public FromCompanyProfile()
        {
            CreateMap<company, Company>().MaxDepth(3);
        }
    }

    public class ToStoreProfile : Profile
    {
        public ToStoreProfile()
        {
            CreateMap<Store, store>().MaxDepth(3);
        }
    }
    public class FromStoreProfile : Profile
    {
        public FromStoreProfile()
        {
            CreateMap<store, Store>().MaxDepth(3);
        }
    }


}
