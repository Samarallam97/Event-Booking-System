using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website.API.DTOs.Application.Tag;
using Website.API.Errors;
using Website.API.Helpers;
using Website.Core.Entities.Application;
using Website.Core.ServiceInterfaces;
using Website.Core.Specifications.Tags;

namespace Website.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class TagsController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ITagService _tagService;

		public TagsController(IMapper mapper, ITagService tagService)
		{
			_mapper=mapper;
			_tagService=tagService;
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost("add")]
		public async Task<IActionResult> AddTag([FromBody] TagToAddOrUpdate tagDTO)
		{
			var tag = _mapper.Map<TagToAddOrUpdate, Tag>(tagDTO);

			var added = await _tagService.Add(tag);

			if (!added)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(tag);
		}

		//[Authorize(Roles = "Admin")]
		[HttpPut("update")]
		public async Task<IActionResult> UpdateTag([FromBody] TagToAddOrUpdate tagDTO)
		{
			var tagFromDb = _tagService.GetTagById(tagDTO.Id);

			if (tagFromDb is null)
				return NotFound(new BaseErrorResponse(400 , $"Tag with Id {tagDTO.Id} Not Found"));

			tagFromDb.Name = tagDTO.Name;
			tagFromDb.NameAR = tagDTO.NameAR;

			var updated = await _tagService.Update(tagFromDb);

			if (!updated)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(tagFromDb);
		}

		//[Authorize(Roles = "Admin")]
		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteTag( string Id)
		{
			var tagFromDb = _tagService.GetTagById(Id);

			if (tagFromDb is null)
				return NotFound(new BaseErrorResponse(400, $"Tag with Id {Id} Not Found"));


			var deleted = await _tagService.Delete(tagFromDb);

			if (!deleted)
				return BadRequest(new BaseErrorResponse(400));
			return Ok();
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] TagsParams tagsParams)
		{
			var tags = _tagService.GetAllTags(tagsParams);

			var count = _tagService.GetCount(tagsParams);


			if(tagsParams.Language == "En")
			{

				var tagDTOs = _mapper.Map<IReadOnlyList<Tag>, IReadOnlyList<TagDTOEn>>(tags);
				
				return Ok(new PaginationResponse<TagDTOEn>
					(tagsParams.PageSize, tagsParams.PageIndex, count, tagDTOs));
			}
			else
			{
				var tagDTOs = _mapper.Map<IReadOnlyList<Tag>, IReadOnlyList<TagDTOAR>>(tags);

				return Ok( new PaginationResponse<TagDTOAR>
						(tagsParams.PageSize, tagsParams.PageIndex, count, tagDTOs));
			}

		}


		[HttpGet("{id}")]
		public IActionResult GetById( string id , string language = "En")
		{
			var tag = _tagService.GetTagById(id);

			if (tag is null)
				return NotFound(new BaseErrorResponse(404));

			TagDTO tagDTO;

			if (language == "En")
				tagDTO = _mapper.Map<Tag,TagDTOEn>(tag);
			else
				tagDTO = _mapper.Map<Tag, TagDTOAR>(tag);

			return Ok(tagDTO);
		}
	}
}
