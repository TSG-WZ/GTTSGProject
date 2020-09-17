using System;
namespace DTTSG_Model
{
	public class LoginInfo
	{
		public int LoginId { get; set; }
		public int UserId { get; set; }
		public DateTime LastTime { get; set; }
		public string LastIp { get; set; }
	}
}
