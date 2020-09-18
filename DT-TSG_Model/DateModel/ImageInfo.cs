using System;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model
{
	public class ImageInfo
	{
        [Key]
		public int ImageId { get; set; }
		public string ImageLink { get; set; }
	}
}
