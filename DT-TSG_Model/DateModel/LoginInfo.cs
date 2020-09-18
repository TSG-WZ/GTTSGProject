using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class LoginInfo
	{
        [Key]
		public int LoginId { get; set; }
		public int UserId { get; set; }
		public DateTime LastTime { get; set; }
		public string LastIp { get; set; }
	}
}
