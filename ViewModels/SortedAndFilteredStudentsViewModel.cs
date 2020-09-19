using ContosoUniversity.Controllers;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ContosoUniversity.ViewModels
{

    public class SortedAndFilteredStudentsViewModel
    {
        public IEnumerable<Student> Students { get; set; }

        public string SearchString { get; set; }

        public string SortyByFirstName { get; set; }
        public string SortByLastName { get; set; }
        public string SortyByEnollmentDate { get; set; }

        public int MaxPageCount { get; set; }
        public int CurrentPage { get; set; }

        public int NextPage
        {
            get
            {
                if (NextPageExists)
                {
                    return CurrentPage+1;
                }
                else
                {
                    return MaxPageCount;
                }
            }
        }
        public int PreviousPage
        {
            get
            {
                if (PreviousPageExist)
                {
                    return CurrentPage-1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public bool NextPageExists
        {
            get
            {
                return CurrentPage < MaxPageCount;
            }
        }
        public bool PreviousPageExist
        {
            get
            {
                return CurrentPage > 1;
            }
        }

    }
}
