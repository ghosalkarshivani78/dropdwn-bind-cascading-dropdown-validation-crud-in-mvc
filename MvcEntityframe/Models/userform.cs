using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace MvcEntityframe.Models
{
    public class person
    {
        public int id { get; set; }

         [Required(ErrorMessage = "Enter First Name")]
        
        public string firstname { get; set; }


         [Required(ErrorMessage = "Enter Last Name")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Enter Email")]
         //[Display(Name = "User Email")]
         [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Invalid Email")]
        public string email { get; set; }


         [Required(ErrorMessage = "Enter address")]
        public string address { get; set; }


         [Required(ErrorMessage = "Enter State ")]
         public string stateid { get; set; }

         [Required(ErrorMessage = "Enter City ")]
         public string cityid { get; set; }
   

        [Required(ErrorMessage = "Enter Number")]
       
        public string number { get; set; }

       
         public SelectList cities { get; set; }

         public SelectList states { get; set; }
    }
    public class ddl
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}