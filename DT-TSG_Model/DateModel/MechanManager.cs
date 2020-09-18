using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class MechanManager
	{
        [Key]
		public int Manger_Mid { get; set; }
		public int MechanId { get; set; }
		public int LibId { get; set; }
	}
}
