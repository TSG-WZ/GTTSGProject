using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class MechanInfo
	{
        [Key]
		public int MechanId { get; set; }
		public int M_StatuId { get; set; }
		public int MechanMax { get; set; }
		public string MechanName { get; set; }
		public string MechanAddress { get; set; }
		public double MechanPrice { get; set; }


	}
}
