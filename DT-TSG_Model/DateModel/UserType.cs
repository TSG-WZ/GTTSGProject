using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
    public class UserType
    {
        [Key]
		public int TypeId { get; set; }
		public string TypeName { get; set; }
	}
}
