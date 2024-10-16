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
    public class GeneralService: IGeneralService
    {
        private readonly TeckTrackersDbContext _dbContext;

        public GeneralService(TeckTrackersDbContext dbContext)
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
            var existingCategory = await _dbContext.Categories.FindAsync(category.Category_ID);
            if (existingCategory == null) throw new KeyNotFoundException($"Category with ID {category.Category_ID} not found.");
            existingCategory.Category_Name = category.Category_Name;
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
            var existingRole = await _dbContext.Roles.FindAsync(role.Role_ID);
            if (existingRole == null)
            {
                throw new KeyNotFoundException($"Role with ID {role.Role_ID} not found.");
            } 

            existingRole.Role_Name = role.Role_Name;
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
            var existingDepartment = await _dbContext.Departments.FindAsync(department.Department_ID);
            if (existingDepartment == null)

            {
                throw new KeyNotFoundException($"Department with ID {department.Department_ID} not found.");
            } 
            existingDepartment.Department_Name = department.Department_Name;
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

        public async Task<bool> AssignRoleToUser(int userId, int roleId)
        {
            // Check if the user exists
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            // Check if the role exists
            var role = await _dbContext.Roles.FindAsync(roleId);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }

            // Check if the User_Role already exists (to avoid duplicates)
            var userRoleExists = await _dbContext.User_Roles
                .AnyAsync(ur => ur.User_ID == userId && ur.Role_ID == roleId);

            if (userRoleExists)
            {
                return false; // Role already assigned to the user
            }

            // Create new User_Role entry
            var userRole = new User_Role
            {
                User_ID = userId,
                Role_ID = roleId
            };

            await _dbContext.User_Roles.AddAsync(userRole);
            await _dbContext.SaveChangesAsync();

            return true; // Role successfully assigned
        }

        public async Task<User> AddTechnician(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Assign the Technician role (role_ID = 4)
            await AssignRoleToUser(user.User_ID, 4);

            return user;
        }
    }
}
