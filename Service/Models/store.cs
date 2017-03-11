using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using AutoMapper;
using Service.Configuration;

namespace Service.Models
{
    public class store
    {
        public System.Guid Id { get; set; }
        public System.Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public virtual Company Company { get; set; }
    }
}
