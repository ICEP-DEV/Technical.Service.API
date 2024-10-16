using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data.Model;

namespace TechTrackers.Service.General
{
    public interface IGeneralService
    {
        Task<Role> AddRole(Role role);
        IEnumerable<Role> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> UpdateRole(Role role);
        Task<bool> DeleteRole(int id);

        Task<bool> AssignRoleToUser(int userId, int roleId); // New method for assigning a role to a user


        // Category methods
        Task<Category> AddCategory(Category category);
        IEnumerable<Category> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);

        //Department methods
        Task<Department> AddDepartment(Department department);
        IEnumerable<Department> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<Department> UpdateDepartment(Department department);
        Task<bool> DeleteDepartment(int id);



    }
}
