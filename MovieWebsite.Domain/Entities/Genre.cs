using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Domain.Entities
{
    public class Genre:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; }
        public ICollection<Film> Films { get; set; }        
        public DateTime CreateDate { get ; set; }
        public DateTime? UpdateDate { get; set ; }
        public DateTime? DeleteDate { get ; set ; }
        public Status Status { get; set ; }
    }
}
