using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class CompensateInfo
	{
        [Key]
		public int CompenId { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public string CompenResult { get; set; }
		public DateTime CompenTime { get; set; }
	}
}
