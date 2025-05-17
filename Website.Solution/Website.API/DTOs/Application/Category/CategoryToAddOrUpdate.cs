namespace Website.API.DTOs.Application.Category
{
	public class CategoryToAddOrUpdate : CategoryDTO
	{
		public string Name { get; set; }
		public string NameAR { get; set; }
		public string Description { get; set; }
		public string DescriptionAR { get; set; }

		public string IconUrl { get; set; }
		public string Color { get; set; }
	}
}
