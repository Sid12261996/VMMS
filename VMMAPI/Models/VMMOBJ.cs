using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VMMAPI.Models
{
    public class VMMOBJ
    {
        [Required(ErrorMessage = "Please select customer")]
        [Display(Name = "ddlCustName")]
        public string ddlCustName { get; set; }

        [Required(ErrorMessage = "Please select city")]
        [Display(Name = "ddlCity")]
        public string ddlCity { get; set; }

        [Required(ErrorMessage = "Please select Asset Type")]
        [Display(Name = "ddlAssestType")]
        public string ddlAssestType { get; set; }
      

        public string ddlVendor { get; set; }



        public string ddlActType { get; set; }

        public string ddlTaskType { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "txtAddress")]
        public string txtAddress { get; set; }

        [Required(ErrorMessage = "Please enter Contact No")]
        [Display(Name = "txtContactNo")]
        public string txtContactNo { get; set; }

        [Required(ErrorMessage = "Please enter Spoc Name")]
        [Display(Name = "txtSpocName")]
        public string txtSpocName { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        [Display(Name = "txtState")]
        public string txtState { get; set; }

        [Required(ErrorMessage = "Please enter no of assets")]
        [Display(Name = "txtAssestNo")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        public string txtAssestNo { get; set; }

    }
  
}