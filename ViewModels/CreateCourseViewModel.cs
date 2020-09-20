using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.ViewModels
{
    public class CreateCourseViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int Credits { get; set; }

        public Department Department { get; set; }

        public List<SelectListItem> DepartmentsSelectList { get; set; }
    }
}
