using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class Notice
	{
        [Key]
		public int NoticeId { get; set; }
		public int N_TypeId { get; set; }
		public int LibId { get; set; }
		public int UserId { get; set; }
		public string NoticeTitle { get; set; }
		public string NoticeContent { get; set; }
		public DateTime NoticeTime { get; set; }
		public bool IsRead { get; set; }
	}
}
