﻿using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ThuVien_API.Repositories;
using ThuVien_API.Models;
using ThuVien_API.Models.DTO;

namespace ThuVien_API.Controllers
{
	[Route("api/[Controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository _imageRepository;
		public ImagesController(IImageRepository imageRepository)
		{
			_imageRepository = imageRepository;
		}
		[HttpPost] 
		[Route("Upload")]
		public IActionResult Upload([FromForm] ImageUploadRequestDTO request)
		{
			ValidateFileUpload(request);
			if (ModelState.IsValid)
			{
				//convert DTO to Domain model
				var imageDomainModel = new Image
				{
					File=request.File,
					FileExtension =Path.GetExtension(request.File.FileName),
					FileSizeInBytes = request.File.Length,
					FileName= request.FileName, 
					FileDescription =request.FileDescription,
				};				
				// User repository to upload image
				_imageRepository.Upload(imageDomainModel);
				return Ok(imageDomainModel);
			}
			return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult GetAllIntoImage()
		{
			var allImages=_imageRepository.GetAllInfoImages();
			return Ok(allImages);
		}
		[HttpGet]
		[Route("Download")]
		public IActionResult DownloadFile(int id)
		{
			var result= _imageRepository.DownloadFile(id);
			return File(result.Item1,result.Item2 ,result.Item3);
		}
		private void ValidateFileUpload(ImageUploadRequestDTO request)
		{
			var allowExtensions= new string[] { "jpg", "jpeg", ".png" };
			if (!allowExtensions.Contains(Path.GetExtension(request.File.FileName)))
			{
				ModelState.AddModelError("file", "Unsupported file extension");
			}
			if (request.File.Length > 1040000)
			{
				ModelState.AddModelError("file", "File size too big, please upload file <10M");
			}
		}
	}
}


