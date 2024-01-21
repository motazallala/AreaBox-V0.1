using AreaBox_V0._1.Areas.Admin.Models.CategoriesModel.Send;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class CategoriesManagement : Controller
    {
        private readonly IUnitOfWork _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public CategoriesManagement(IUnitOfWork db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var getAllCategories = await _db.Categories.GetAllAsync<Categories, CategoriesIndexDto>();
            return View(getAllCategories);
        }

		[HttpGet]
		public async Task<IActionResult> GetAllCategory()
		{
			var getAllCategories = await _db.Categories.GetAllAsync<Categories, CategoriesIndexDto>();
			return Ok(getAllCategories);
		}

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] string categoryName)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return BadRequest("Please login in!");
            }

            if(categoryName == null)
            {
				return BadRequest("Please provide a valid category name to add!");
			}

			bool checkCategoryName = await _db.Categories.CheckItemExistence<Categories>(e => e.CategoryName == categoryName);

			if (checkCategoryName)
			{
				return BadRequest("Category name is exists");
			}

			var addCategory = new Categories
            {
                CategoryName = categoryName,
            };

            _db.Categories.Add(addCategory);
            await _db.Save();

            return Ok("Category has been added!");

        }

		[HttpPost]
		public async Task<IActionResult> RemoveCategory([FromForm] int categoreId)
		{
			var user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return BadRequest("Please login in!");
			}

			if (categoreId == null)
			{
				return BadRequest("Please select category to remove!");
			}

            var getCategory = await _db.Categories.GetByIdAsync(categoreId);

			_db.Categories.Remove(getCategory);
			await _db.Save();

			return Ok("Category has been removed!");

		}

		[HttpPost]
		public async Task<IActionResult> EditCategory([FromForm] int categoreId, string categoryName)
		{
			var user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return BadRequest("Please login in!");
			}

			if (categoreId == null)
			{
				return BadRequest("Please select category to edit!");
			}

			if (categoryName == null)
			{
				return BadRequest("Please provide the new category name!");
			}

			var getCategory = await _db.Categories.GetByIdAsync(categoreId);

			getCategory.CategoryId = categoreId;
			getCategory.CategoryName = categoryName;

			_db.Categories.Update(getCategory);
			await _db.Save();

			return Ok("Category has been updated!");

		}

	}
}
