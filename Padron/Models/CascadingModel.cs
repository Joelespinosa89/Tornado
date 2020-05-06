using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padron.Models
{
    public class CascadingModel
    {
        public CascadingModel()
        {
            this.Provincias = new List<SelectListItem>();
            this.Municipios = new List<SelectListItem>();
            this.ElectoralColleges = new List<SelectListItem>();
        }

        public List<SelectListItem> Provincias { get; set; }
        public List<SelectListItem> Municipios { get; set; }
        public List<SelectListItem> ElectoralColleges { get; set; }
    }
}