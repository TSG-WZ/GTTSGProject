using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class OperateType
	{
        [Key]
		public int O_TypeId { get; set; }
		public string O_TypeName { get; set; }
		public int Seo { get; set; }
	}
}
