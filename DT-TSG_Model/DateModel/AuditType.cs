using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class AuditType
	{
        [Key]
		public int A_TypeId { get; set; }
		public string A_TypeName { get; set; }
		public int Seo { get; set; }
	}
}
