using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class sysdiagrams
	{
        [Key]
		public string Name { get; set; }
		public int Principal_id { get; set; }
		public int Diagram_id { get; set; }
		public int Version { get; set; }
		public string Definition { get; set; }
	}
}
