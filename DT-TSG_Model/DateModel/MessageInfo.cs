using System;
namespace DTTSG_Model
{
	public class MessageInfo
	{
		public int MessageId { get; set; }
		public int M_TypeId { get; set; }
		public int AdminId { get; set; }
		public string MessageTitle { get; set; }
		public string MessageContent { get; set; }
		public DateTime MessageTIme { get; set; }
		public bool IsValid { get; set; }
	}
}
