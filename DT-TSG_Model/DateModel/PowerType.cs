using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class PowerType
	{
        [Key]
		public int P_TypeId { get; set; }
		public string P_TypeName { get; set; }
		public string P_Link { get; set; }
		public string P_Icon { get; set; }
		public int Seo { get; set; }
	}
}
