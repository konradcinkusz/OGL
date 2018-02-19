using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repozytorium.Model
{
    public class UsedNumber
    {
        public UsedNumber()
        {
            
        }
        [Display(Name = "Id:")]
        public int ID { get; set; }
        public int Used { get; set; }
    }
}
