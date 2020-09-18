using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class BookStatu
	{
        [Key]
		public int B_StatuId { get; set; }
		public string B_StatuName { get; set; }
		public int Seo { get; set; }
	}
}
