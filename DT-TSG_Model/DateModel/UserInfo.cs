using System;
namespace DTTSG_Model
{
	public class UserInfo
	{
		public int UserId { get; set; }
        public UserType UserType { get; set; }
        public int TypeId { get; set; }
        public UserStatu UserStatu { get; set; }
        public int StatuId { get; set; }
		public int ImageId { get; set; }
		public string UserPwd { get; set; }
		public string UserName { get; set; }
		public bool UserSex { get; set; }
		public string UserAddress { get; set; }
		public string UserPhone { get; set; }
		public bool IsValid { get; set; }
	}
}
