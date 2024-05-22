using ThuVien_API.Models;

namespace ThuVien_API.Repositories
{
	public interface IImageRepository
	{
		Image Upload(Image image);
		List<Image> GetAllInfoImages();
		(byte[], string, string) DownloadFile(int id);
	}
}
