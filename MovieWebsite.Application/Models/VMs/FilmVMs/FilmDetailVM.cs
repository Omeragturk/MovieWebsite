﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.VMs.FilmVMs
{
    public class FilmDetailVM
    {       
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string GenreName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
