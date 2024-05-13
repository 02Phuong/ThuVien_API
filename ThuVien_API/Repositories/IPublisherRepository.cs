using ThuVien_API.Models.DTO;
using ThuVien_API.Models;

namespace ThuVien_API.Repositories
{
	public interface IPublisherRepository
	{
		List<PublisherDTO> GetAllPublishers();
		PublisherNoIdDTO GetPublisherById(int id);
		AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
		PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
		Publishers? DeletePublisherById(int id);
	}
}
