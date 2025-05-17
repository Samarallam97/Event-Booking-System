using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Website.API.DTOs.Application.Category;
using Website.API.Errors;
using Website.API.Helpers;
using Website.Core.Entities.Application;
using Website.Core.ServiceInterfaces;
using Website.Core.Specifications.Categories;

namespace Website.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryToAddOrUpdate categoryDTO)
        {
            var category = _mapper.Map<CategoryToAddOrUpdate, Category>(categoryDTO);

            var added = await _categoryService.Add(category);

            if (!added)
                return BadRequest(new BaseErrorResponse(400));

            return Ok(category);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory([FromBody] CategoryToAddOrUpdate categoryDTO)
        {
            var categoryFromDb = _categoryService.GetCategoryById(categoryDTO.Id);

            if (categoryFromDb is null)
                return NotFound(new BaseErrorResponse(404, $"Category with Id {categoryDTO.Id} Not Found"));

            //var IconUrl = await ImageUrlGenerator.GetImageUrl(categoryDTO.IconImage);

            //if (IconUrl is null)
            //	return BadRequest(new BaseErrorResponse(400, "Error while processing the image , images with size > 5MB not allowed"));

            categoryFromDb.Name = categoryDTO.Name;
            categoryFromDb.Description = categoryDTO.Description;
            categoryFromDb.Color = categoryDTO.Color;
            categoryFromDb.IconUrl = categoryDTO.IconUrl;

            var updated = await _categoryService.Update(categoryFromDb);

            if (!updated)
                return BadRequest(new BaseErrorResponse(400));

            return Ok(categoryDTO);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(string Id)
        {
            var categoryFromDb = _categoryService.GetCategoryById(Id);

            if (categoryFromDb is null)
                return NotFound(new BaseErrorResponse(404, $"Category with Id {Id} Not Found"));

            var deleted = await _categoryService.Delete(categoryFromDb);

            if (!deleted)
                return BadRequest(new BaseErrorResponse(400));
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] CategoryParams categoryParams)
        {
            var categories = _categoryService.GetAllCategories(categoryParams);

            var count = _categoryService.GetCount(categoryParams);

            if (categoryParams.Language == "En")
            {

                var categoryDTOs = _mapper.Map<List<Category>, List<CategoryDTOEn>>(categories.ToList());
                return Ok(new PaginationResponse<CategoryDTOEn>
                    (categoryParams.PageSize, categoryParams.PageIndex, count, categoryDTOs));
            }
            else
            {
                var categoryDTOs = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDTOAR>>(categories);
                return Ok(new PaginationResponse<CategoryDTOAR>
                        (categoryParams.PageSize, categoryParams.PageIndex, count, categoryDTOs));
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id, string language)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category is null)
                return NotFound(new BaseErrorResponse(404));


            CategoryDTO categoryDTO;

            if (language == "En")
                categoryDTO = _mapper.Map<Category, CategoryDTOEn>(category);
            else
                categoryDTO = _mapper.Map<Category, CategoryDTOAR>(category);

            return Ok(categoryDTO);
        }


    }
}
