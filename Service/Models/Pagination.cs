using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class Pagination
    {
        public Pagination Pager { get; set; }

        public Pagination(int totalOfItems, int? page, int pageSize = 4)
        {
            var totalOfPages = (int)Math.Ceiling((decimal)totalOfItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 2;
            var endPage = currentPage + 2;

            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalOfPages)
            {
                endPage = totalOfPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalOfItems = totalOfPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalOfPages = totalOfPages;
            StartPage = startPage;
            EndPage = endPage;

        }

        public int TotalOfItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalOfPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

    }
}
