using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCRUD.Models
{
    public class EmployeePaginationViewModel
    {
        public EmployeePaginationViewModel(ICollection<EmployeeViewModel> list, int pageNumber, int pageSize, int totalRecords, int pagerLength)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException($"{nameof(pageNumber)} can't be smaller than 1");
            }
            PageNumber = pageNumber;

            if (pageSize < 1)
            {
                throw new ArgumentException($"{nameof(pageSize)} can't be smaller than 1");
            }
            PageSize = pageSize;

            if (totalRecords < 0)
            {
                throw new ArgumentException($"{nameof(totalRecords)} can't be smaller than 0");
            }
            TotalRecords = totalRecords;

            if (pagerLength < 1)
            {
                throw new ArgumentException($"{nameof(pagerLength)} can't be smaller than 1");
            }
            PagerLength = pagerLength;

            FirstPage = 1;
            LastPage = Math.Max(1, (int)Math.Ceiling((double)TotalRecords / PageSize));

            PreviousPage = PageNumber - 1 < FirstPage ? null : PageNumber - 1;
            NextPage = PageNumber + 1 > LastPage ? null : PageNumber + 1;

            PagerLength = Math.Min(PagerLength, LastPage);
            PagerStartIndex = Math.Clamp(
                PageNumber - (PagerLength - 1) / 2,
                FirstPage,
                LastPage - PagerLength + 1);

            Employees = list;
        }

        public ICollection<EmployeeViewModel> Employees { get; protected set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int FirstPage { get; set; }

        public int LastPage { get; set; }

        public int? PreviousPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalRecords { get; set; }

        public int PagerLength { get; set; }

        public int PagerStartIndex { get; set; }
    }
}
