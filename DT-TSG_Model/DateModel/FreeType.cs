using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class FreeType
	{
        [Key]
		public int F_TypeId { get; set; }
		public string F_TypeName { get; set; }
		public int Seo { get; set; }
	}
}
