using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class BorrowInfo
	{
        [Key]
		public int BorrowId { get; set; }
        public BookInfo BookInfo { get; set; }
        public int BookId { get; set; }
        public MechanInfo MechanInfo { get; set; }
		public int B_MechanId { get; set; }
        public BorrowType BorrowType { get; set; }
        public int Bo_TypeId { get; set; }
        public UserInfo UserInfo{ get; set; }

        public int UserId { get; set; }
		public DateTime B_StartTime { get; set; }
		public DateTime B_EndTime { get; set; }
		public DateTime B_ReturnTime { get; set; }

	}
}
