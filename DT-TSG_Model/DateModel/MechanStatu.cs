using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class MechanStatu
	{
        [Key]
		public int M_StatuId { get; set; }
		public string M_StatuName { get; set; }
	}
}
