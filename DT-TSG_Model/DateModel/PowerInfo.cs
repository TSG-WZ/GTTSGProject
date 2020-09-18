using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class PowerInfo
	{
        [Key]
		public int P_TypeId { get; set; }
		public int LibId { get; set; }
	}
}
