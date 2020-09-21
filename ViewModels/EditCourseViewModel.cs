using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.ViewModels
{
    public class EditCourseViewModel
    {
        public EditCourseViewModel(int id)
        {
            this.ID = id;
        }

        public readonly int ID;

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(1, 100)]
        public int Credits { get; set; }

        public int DepartmentId { get; set; }

        public List<SelectListItem> DepartmentsSelectListItems { get; set; }
    }
}
