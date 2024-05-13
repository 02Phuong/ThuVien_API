﻿namespace ThuVien_API.Models.DTO
{
	public class BookWithAuthorAndPublisherDTO
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public bool IsRead { get; set; }
		public DateTime? DateRead { get; set; }
		public int? Rate { get; set; }
		public int Genre { get; set; }
		public string? CoverUrl { get; set; }
		public DateTime DateAdded { get; set; }
		public string PublisherName { get; set; }
		public List<string> AuthorName { get; set; }
	}
}
