using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class NoticeType
	{
        [Key]
		public int N_TypeId { get; set; }
		public string N_TypeName { get; set; }
	}
}
