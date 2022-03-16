using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_App.Models
{
    public class Party
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Sisesta pidu nimi")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Sisesta päev")]
        public string Date { get; set; }

    }
}