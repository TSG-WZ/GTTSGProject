using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_Model
{
    public class FowardType
    {
        [Key]
        public int Fo_TypeId { get; set; }

        public string Fo_TypeName { get; set; }
    }
}
