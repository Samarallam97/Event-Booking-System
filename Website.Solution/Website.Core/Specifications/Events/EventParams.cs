

namespace Website.Core.Specifications.Product
{
	public class EventParams
	{
		public string? Language { get; set; }
		public string? TagId { get; set; }
		public string? CategoryId { get; set; }
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }

		public DateTime? Date { get; set; }

		public bool IncludeCompleted { get; set; } = false;

        private string? search;
		public string? Search
		{
			get { return search; }
			set { search = value?.ToLower(); }
		}


		private const int MAX_PAGE_SIZE = 6;

		private int pageSize = MAX_PAGE_SIZE;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value; }
		}
		public int PageIndex { get; set; } = 1;

	}
}

