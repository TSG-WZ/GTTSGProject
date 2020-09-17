using System;
namespace DTTSG_Model
{
	public class ForwardInfo
	{
		public int F_Id { get; set; }
		public int BookId { get; set; }
		public int UserId { get; set; }
		public DateTime F_StartTime { get; set; }
		public DateTime F_EndTime { get; set; }
	}
}
