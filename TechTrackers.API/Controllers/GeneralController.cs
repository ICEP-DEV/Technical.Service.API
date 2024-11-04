using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model.dto;
using TechTrackers.Data.Model;
using TechTrackers.Service.General;

namespace TechTrackers.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class GeneralController : Controller
    {
        private readonly IGeneralService _generalService;

        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest(new { message = "Invalid role data" });
            }

            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to create a role"
            };

            var addedRole = await _generalService.AddRole(role);

            if (addedRole != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Role created successfully!",
                    Result = addedRole
                };
            }

            return Ok(responseWrapper);
        }

        // Update Role
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Role role)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to update role"
            };

            var updatedRole = await _generalService.UpdateRole(role);

            if (updatedRole != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Role updated successfully!",
                    Result = updatedRole
                };
            }

            return Ok(responseWrapper);
        }

        // Delete Role
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to delete role"
            };

            var isDeleted = await _generalService.DeleteRole(id);

            if (isDeleted)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Role deleted successfully!"
                };
            }

            return Ok(responseWrapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Role not found"
            };

            var role = await _generalService.GetRoleById(id);

            if (role != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved role",
                    Result = role
                };
            }

            return Ok(responseWrapper);
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var respondWrapper = new RespondWrapper
            {

                IsSuccess = false,
                Message = "Failed to retrive roles"
            };

            var results = _generalService.GetAllRoles();

            if (results.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved all roles",
                    Result = results

                };
            }

            return Ok(respondWrapper);
        }

        // Add Department
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to create a department"
            };

            var addedDepartment = await _generalService.AddDepartment(department);

            if (addedDepartment != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Department created successfully!",
                    Result = addedDepartment
                };
            }

            return Ok(responseWrapper);
        }

        // Update Department
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to update department"
            };

            var updatedDepartment = await _generalService.UpdateDepartment(department);

            if (updatedDepartment != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Department updated successfully!",
                    Result = updatedDepartment
                };
            }

            return Ok(responseWrapper);
        }

        // Delete Department
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to delete department"
            };

            var isDeleted = await _generalService.DeleteDepartment(id);

            if (isDeleted)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Department deleted successfully!"
                };
            }

            return Ok(responseWrapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Department not found"
            };

            var department = await _generalService.GetDepartmentById(id);

            if (department != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved department",
                    Result = department
                };
            }

            return Ok(responseWrapper);
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var respondWrapper = new RespondWrapper
            {

                IsSuccess = false,
                Message = "Failed to retrive departments"
            };

            var results = _generalService.GetAllDepartments();

            if (results.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved all departments",
                    Result = results

                };
            }

            return Ok(respondWrapper); ;
        }

        // Add Category
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to create a category"
            };

            var addedCategory = await _generalService.AddCategory(category);

            if (addedCategory != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Category created successfully!",
                    Result = addedCategory
                };
            }

            return Ok(responseWrapper);
        }

        // Update Category
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to update category"
            };

            var updatedCategory = await _generalService.UpdateCategory(category);

            if (updatedCategory != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Category updated successfully!",
                    Result = updatedCategory
                };
            }

            return Ok(responseWrapper);
        }

        // Delete Category
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to delete category"
            };

            var isDeleted = await _generalService.DeleteCategory(id);

            if (isDeleted)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Category deleted successfully!"
                };
            }

            return Ok(responseWrapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Category not found"
            };

            var category = await _generalService.GetCategoryById(id);

            if (category != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved category",
                    Result = category
                };
            }

            return Ok(responseWrapper);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var respondWrapper = new RespondWrapper
            {

                IsSuccess = false,
                Message = "Failed to retrive categories"
            };

            var results = _generalService.GetAllCategories();
            if (results.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved all categories",
                    Result = results

                };
            }

            return Ok(respondWrapper); ;
        }


    }
}



