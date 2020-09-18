using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class AdminInfo
	{
        [Key]
		public int AdminId { get; set; }
		public int ImageId { get; set; }
		public string AdminPwd { get; set; }
		public string AdminName { get; set; }
		public bool AdminSex { get; set; }
		public string AdminAddress { get; set; }
		public string AdminPhone { get; set; }
	}
}
