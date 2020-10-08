using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
    public class ForwardInfo
    {
        [Key]
		public int F_Id { get; set; }
        public BookInfo BookInfo { get; set; }

        public int BookId { get; set; }
		public int UserId { get; set; }
		public DateTime F_StartTime { get; set; }
		public DateTime F_EndTime { get; set; }

	}
}
