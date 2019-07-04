using BTA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTA.ViewModels
{
    public class LifeInCItyViewModel
    {
        [Key]
        public int Id { get; set; }
        public City City { get; set; }
        public POI POI { get; set; }
        public Category Category { get; set; }
        public Line Line { get; set; }
    }
}