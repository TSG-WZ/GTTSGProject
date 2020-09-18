using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class OperateInfo
	{
        [Key]
		public int OperaId { get; set; }
		public int O_TypeId { get; set; }
		public int LibId { get; set; }
		public DateTime OperaTime { get; set; }
	}
}
