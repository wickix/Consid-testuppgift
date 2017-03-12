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
    public class company
    {
      public System.Guid Id { get; set; }
      public string Name { get; set; }
      public int OrganizationNumber { get; set; }
      public string Notes { get; set; }

      public List<Store> Stores { get; set; }
    }
}
