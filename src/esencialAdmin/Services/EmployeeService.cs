using esencialAdmin.Data.Models;
using esencialAdmin.Models.CustomerViewModels;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using esencialAdmin.Data;
using esencialAdmin.Models.EmployeeViewModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace esencialAdmin.Services
{
    public class EmployeeService : IEmployeeService
    {
        protected readonly esencialAdminContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeService(
            esencialAdminContext context,
            UserManager<ApplicationUser> userManager)

        {
            _context = context;
            _userManager = userManager;

        }


        public async System.Threading.Tasks.Task<bool> deleteEmployee(string username)
        {
            try
            {
                await _userManager.DeleteAsync(await _userManager.FindByNameAsync(username));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public JsonResult loadEmployeeDataTable(HttpRequest Request)
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name                  
                var columnIndex = Request.Form["order[0][column]"].ToString();

                // var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                string sortColumn = Request.Form[$"columns[{columnIndex}][data]"].ToString();

                var sortDirection = Request.Form["order[0][dir]"].ToString();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var employeeData = (from tempemployee in _context.AspNetUsers
                                    select new
                                    {
                                        firstName = tempemployee.FirstName,
                                        lastName= tempemployee.LastName,
                                        username = tempemployee.UserName,
                                        role = tempemployee.AspNetUserRoles.FirstOrDefault().Role.Name
                                    });

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    employeeData = employeeData.OrderBy(sortColumn + ' ' + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    employeeData = employeeData.Where(m => m.firstName.StartsWith(searchValue) || m.lastName.StartsWith(searchValue) || m.username.StartsWith(searchValue) || m.role.StartsWith(searchValue));
                }

                //total number of rows count   
                recordsTotal = employeeData.Count();
                //Paging   
                var data = employeeData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public EmployeeEditViewModel loadEmployeeEditModel(string username)
        {
            try
            {
                var applicationUser = _context.AspNetUsers.Where(x => x.UserName == username).FirstOrDefault();

                if (applicationUser == null)
                {
                    return null;
                }
                EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
                {
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    Email = applicationUser.Email,
                    currentEmail = applicationUser.UserName
                };

                employeeEditViewModel.EmployeeRoles = getAvailableRoles();

                string role = _context.AspNetUserRoles.Where(x => x.UserId == applicationUser.Id).Select(y => y.Role.Name).FirstOrDefault();
                if (role != null)
                {
                    employeeEditViewModel.Role = role;
                }

                return employeeEditViewModel;
            }
            catch(Exception ex)
            {
                return null;
            }         
        }

        public async System.Threading.Tasks.Task<bool> updateEmployee(EmployeeEditViewModel employeeToUpdate)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(employeeToUpdate.currentEmail);

                if (user == null)
                {
                    return false;
                }

                var email = user.Email;
                if (employeeToUpdate.Email != employeeToUpdate.currentEmail)
                {
                    var setEmailResult = await _userManager.SetEmailAsync(user, employeeToUpdate.Email);
                    if (!setEmailResult.Succeeded)
                    {
                        throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                    }

                    var setUserNameResult = await _userManager.SetUserNameAsync(user, employeeToUpdate.Email);
                    if (!setEmailResult.Succeeded)
                    {
                        await _userManager.SetEmailAsync(user, user.UserName);
                        throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                    }
                }



                var employeeToEdit = this._context.AspNetUsers
                  .Where(c => c.UserName == employeeToUpdate.Email)
                  .FirstOrDefault();
                if (employeeToEdit == null)
                {
                    return false;
                }

                employeeToEdit.FirstName = employeeToUpdate.FirstName;
                employeeToEdit.LastName = employeeToUpdate.LastName;
                this._context.SaveChanges();

                string role = _context.AspNetUserRoles.Where(x => x.UserId == user.Id).Select(y => y.Role.Name).FirstOrDefault();

                if (role != employeeToUpdate.Role)
                {
                    await _userManager.RemoveFromRoleAsync(user,role);
                    await _userManager.AddToRoleAsync(user, employeeToUpdate.Role);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<EmployeeRolesViewModel> getAvailableRoles()
        {
            List<EmployeeRolesViewModel> roleList = new List<EmployeeRolesViewModel>();

            foreach (AspNetRoles role in _context.AspNetRoles)
            {
                roleList.Add(new EmployeeRolesViewModel
                {
                    Id = role.Name,
                    Role = role.Name
                });
            }

            return roleList;
        }

        public bool isUsernameUnique(string username)
        {
            return !_context.AspNetUsers.Any(x => x.UserName == username);

        }
    }
}
