using System;
namespace DTTSG_Model
{
	public class OrderInfo
	{
		public int OrderId { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public int F_TypeId { get; set; }
		public DateTime OrderTime { get; set; }
		public double TruePrice { get; set; }
	}
}
