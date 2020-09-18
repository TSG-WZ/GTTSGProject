using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class MessageType
	{
        [Key]
		public int M_TypeId { get; set; }
		public string M_TypeName { get; set; }
	}
}
