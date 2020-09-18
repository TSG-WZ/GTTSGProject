using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class BorrowType
	{
        [Key]
		public int Bo_TypeId { get; set; }
		public string Bo_TypeName { get; set; }
		public int Seo { get; set; }
	}
}
