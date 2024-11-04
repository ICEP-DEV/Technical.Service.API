using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data;
using TechTrackers.Data.Model;

namespace TechTrackers.Service.General
{
    public class GeneralService : IGeneralService
    {

        private readonly TechTrackersDbContext _dbContext;

        public GeneralService(TechTrackersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> AddCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }


        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) return false;
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found.");
            }

            return category;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(category.CategoryId);
            if (existingCategory == null) throw new KeyNotFoundException($"Category with ID {category.CategoryId} not found.");
            existingCategory.CategoryName = category.CategoryName;
            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }



        //Roles |||||||||||||||||||||||||||||||||||||

        public async Task<Role> AddRole(Role role)
        {
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }


        public async Task<bool> DeleteRole(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role == null) return false;
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<Role> GetRoleById(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);

            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id {id} not found.");
            }

            return role;
        }

        public async Task<Role> UpdateRole(Role role)
        {
            var existingRole = await _dbContext.Roles.FindAsync(role.RoleId);
            if (existingRole == null)
            {
                throw new KeyNotFoundException($"Role with ID {role.RoleId} not found.");
            }

            existingRole.RoleName = role.RoleName;
            existingRole.Description = role.Description;

            await _dbContext.SaveChangesAsync();
            return existingRole;
        }


        //Departments ||||||||||||||||
        public async Task<Department> AddDepartment(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);

            if (department == null)
            {
                throw new KeyNotFoundException($"Department with id {id} not found.");
            }

            return department;
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            var existingDepartment = await _dbContext.Departments.FindAsync(department.DepartmentId);
            if (existingDepartment == null)

            {
                throw new KeyNotFoundException($"Department with ID {department.DepartmentId} not found.");
            }
            existingDepartment.DepartmentName = department.DepartmentName;
            await _dbContext.SaveChangesAsync();
            return existingDepartment;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null) return false;
            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        IEnumerable<Department> IGeneralService.GetAllDepartments()
        {
            return _dbContext.Departments.ToList();
        }

        IEnumerable<Role> IGeneralService.GetAllRoles()
        {
            return _dbContext.Roles.ToList();
        }

        IEnumerable<Category> IGeneralService.GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

    

      
    }
}
