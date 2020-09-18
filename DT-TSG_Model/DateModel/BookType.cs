using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class BookType
	{
        [Key]
		public int B_TypeId { get; set; }
		public string B_TypeName { get; set; }
	}
}
