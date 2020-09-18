using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class UserCollect
	{
        [Key]
		public int CollectId { get; set; }
		public int BookId { get; set; }
		public int UserId { get; set; }
	}
}
