using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class UserStatu
	{
        [Key]
		public int U_StatuId { get; set; }
		public string U_StatuName { get; set; }
	}
}
