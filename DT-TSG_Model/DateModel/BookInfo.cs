using System;
namespace DTTSG_Model
{
	public class BookInfo
	{
		public int BookId { get; set; }
        public BookStatu BookStatu { get; set; }
        public int B_StatuId { get; set; }
        public BookType BookType { get; set; }
        public int B_TypeId { get; set; }
        public MechanInfo MechanInfo { get; set; }
        public int MechanId { get; set; }
        public ImageInfo ImageInfo { get; set; }
        public int ImageId { get; set; }
		public int BookCode { get; set; }
		public string BookName { get; set; }
		public double BookPrice { get; set; }
		public string BookPublic { get; set; }
		public string BookAuthor { get; set; }
		public string BookPubtime { get; set; }
		public bool IsValid { get; set; }
	}
}
