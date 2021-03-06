using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class MessageInfo
	{
        [Key]
		public int MessageId { get; set; }
		public int M_TypeId { get; set; }
        public MessageType MessageType { get; set; }
		public int AdminId { get; set; }
		public string MessageTitle { get; set; }
		public string MessageContent { get; set; }
		public DateTime MessageTime { get; set; }
		public bool IsValid { get; set; }
	}
}
