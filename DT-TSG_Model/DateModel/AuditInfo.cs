using System;
namespace DTTSG_Model
{
	public class AuditInfo
	{
		public int AuditId { get; set; }
		public int AdminId { get; set; }
		public int LibId { get; set; }
		public string AuditContent { get; set; }
		public int A_TypeId { get; set; }
		public bool AuditResult { get; set; }
	}
}
