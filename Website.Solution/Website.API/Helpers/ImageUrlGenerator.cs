namespace Website.API.Helpers
{
	public static class ImageUrlGenerator
	{
		public async static Task<string?> GetImageUrl(IFormFile image)
		{
			if (image == null || image.Length == 0) return null;


			if (!image.ContentType.StartsWith("image/")) return null;

			if (image.Length > 5 * 1024 * 1024) return null;

			try
			{
				var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
				var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

				Directory.CreateDirectory(Path.GetDirectoryName(savePath));

				using (var stream = new FileStream(savePath, FileMode.Create))
				{
					await image.CopyToAsync(stream);
				}

				return $"wwwroot/images/{fileName}";
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
