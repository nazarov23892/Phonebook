using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.ViewModels
{
    public class ContactListViewModel
    {
        private IEnumerable<Contact> contacts;

        public int PageNo { get; set; } = 1;

        public int PageSize { get; set; } = 12;

        public string FilterName { get; set; }

        public string FilterPhonenumber { get; set; }

        public int PageCount {
            get
            {
                int count = contacts?.Count() ?? 0;
                int pageCount = (count / PageSize)
                    + (count % PageSize > 0 ? 1 : 0);
                return pageCount;
            }
        }

        public IEnumerable<Contact> Contacts 
        {
            get
            {
                if (PageNo <= 0)
                {
                    throw new IndexOutOfRangeException($"{nameof(this.PageNo)}={PageNo}");
                }
                int first = (PageNo - 1) * PageSize; ;
                var sublist = contacts?
                    .Skip(first)?
                    .Take(PageSize);
                return sublist;
            }
            set => contacts = value; 
        }

    }
}
