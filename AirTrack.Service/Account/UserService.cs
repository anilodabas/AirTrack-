using AirTrack.Core.Helpers;
using AirTrack.Core.Types;
using AirTrack.Entity.Account;
using AirTrack.Model.Account.User;
using AirTrack.Repository.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AirTrack.Service.Account

{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;

        }
        public Result Create(UserCreateModel model)
        {
            var result = new Result();
            var users = _userRepository.GetAll();
            var userinput = model.Code;
            foreach (User user in users)
            {
                if (user.Code == userinput )
                {
                    result.Success = false;
                    result.Message = "Such a user already exists";
                    return result;
                    
                }
                else if (userinput == null)
                {
                    result.Success = false;
                    result.Message = "Please enter a valid code";
                    return result;
                }
                else
                {
                    result.Success = true;
                }
            }
        
            try
            {


                var user = new User()

                {

                    Name = model.Name,
                    Surname = model.Surname,
                    Code = model.Code,
                    BirthDate = model.BirthDate,
                    Occupation = model.Occupation,
                    Email = model.Email,
                    Password = CryptoHelper.Encrypt(model.Password),
                    Homebase = model.HomeBase,
                    Phone = model.Phone,
                    PhonePrivate = model.PhonePrivate,
                    IsActive = model.IsActive

                };

                user.InsertedDate = DateTime.Now;
                var usr = _userRepository.Create(user);
                
                if (model.SelectedRoleId.Length > 0)
                {
                    
                        var res = SetRoleToUser(model.SelectedRoleId, usr.Id);
                        if (res.Success == false)
                            return res;
                  
                    
                }
                else
                {
                    result.Success = false;
                    result.Message = "Please specify a role";
                    return result;
                }
              

                result.Success = true;
                result.Message = "User was created succesfully";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "It has a an error when creating user " + ex.Message;
            }

            return result;

        }

        public Result Delete(int Id)
        {
            var result = new Result();
            try
            {
                var user = _userRepository.GetById(Id);
                if (user == null || user.IsDeleted || !user.IsActive)
                {
                    result.Success = false;
                    result.Message = "Couldn't find user with id  which taken by input";
                    return result;
                }
                user.IsActive = false;
                user.IsDeleted = true;

                user.UpdatedDate = DateTime.Now;
                _userRepository.Update(user);

                result.Success = true;
                result.Message = "User was deleted successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occured when creating user " + ex.Message;
            }
            return result;
        }

        public Result<List<UserListModel>> GetAll()
        {
            var result = new Result<List<UserListModel>>();
            try
            {
                var users = _userRepository.GetAll().ToList();
                List<UserListModel> usersModel = new List<UserListModel>();

                foreach (User user in users)
                {
                    if (!user.IsDeleted && user.IsActive)
                    {
                        var userRoles = "";
                        foreach (Role role in user.Roles)
                        {
                            userRoles += role.Name;
                            if (user.Roles.Last() != role)
                                userRoles += ",";
                         }
                        usersModel.Add(new UserListModel()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Surname = user.Surname,
                            Code = user.Code,
                            BirthDate = user.BirthDate,
                            Occupation = user.Occupation,
                            Email = user.Email,
                            Password = user.Password,
                            Homebase = user.Homebase,
                            Phone = user.Phone,
                            PhonePrivate = user.PhonePrivate,
                            IsActive = user.IsActive,
                            CreateDate = user.InsertedDate,
                            Role = userRoles




                        });
                    }

                }
                result.Success = true;
                result.Message = "All users fetched successfully";
                result.Data = usersModel;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occured when fetching users! " + ex;
            }

            return result;
        }

        public Result<UserListModel> Get(int id)
        {
            Result<UserListModel> result = new Result<UserListModel>();
            try
            {
                var user = _userRepository.GetById(id);


                if (user == null)
                {
                    result.Success = false;
                    result.Message = "Could't found user in database which given id";
                    return result;
                }
                var userRoles = "";
                foreach (Role role in user.Roles)
                {
                     userRoles += role.Name;
                    if (user.Roles.Last() != role)
                        userRoles += ",";
                }

                        var userList = new UserListModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Code = user.Code,
                    BirthDate = user.BirthDate,
                    Occupation = user.Occupation,
                    Email = user.Email,
                    Homebase = user.Homebase,
                    Phone = user.Phone,
                    PhonePrivate = user.PhonePrivate,
                    IsActive = user.IsActive,
                    CreateDate = user.InsertedDate,
                    Role = userRoles
                };
                result.Success = true;
                result.Message = "User was fetched successfully from database";
                result.Data = userList;
                return result;


            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error occured when fetching user from database "+ ex;
                return result;
            }

        }

        public Result<bool> IsUserExist(string code)
        {
            var user = _userRepository.GetAll().ToList().Find(x => x.Code == code && x.IsDeleted == false);
            var ret = false;
            if (user != null)
                ret = true;
            Result<bool> result = new Result<bool>();
            result.Success = true;
            result.Message = "Activity checked successfully from database";
            result.Data = ret;
            return result;
        }

        public Result<User> SetRoleToUser(int[] roles, int userId)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                return new Result<User>()
                {
                    Message = "User couldn't find in database",
                    Success = false
                };
            }

            var usr = _roleRepository.ClearRolesByUser(user);
            foreach (int inputRole in roles)
            {
                var role = _roleRepository.GetById(inputRole);
                 if (role == null)
                    return new Result<User>()
                    {
                        Message = "Couldn't find role in database",
                        Success = false
                    };
                else
                {
                    usr.Roles.Add(role);
                    
                  
                }
               
               
            }
            return new Result<User>() { Message = "Success", Success = true, Data = usr };
        }

        public Result Update(UserUpdateModel model)
        {

            var result = new Result();

            try
            {
                var user = _userRepository.GetById(model.Id);
                if (user == null)
                {
                    result.Success = false;
                    result.Message = "Users couln't find in database";
                    return result;
                }

                user.Id = model.Id;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.BirthDate = model.BirthDate;
                user.Occupation = model.Occupation;
                user.Email = model.Email;
                user.Password = CryptoHelper.Encrypt(model.Password);
                user.Homebase = model.HomeBase;
                user.Phone = model.Phone;
                user.PhonePrivate = model.PhonePrivate;
                user.IsActive = model.IsActive;
                user.UpdatedDate = DateTime.Now;
                user.Roles = new List<Role>(); 

                
                //_roleRepository.ClearRolesByUser(user);
                _userRepository.Update(user);

                Result<User> res = new Result<User>();
                if (model.SelectedRoleId.Length > 0)
                {
                        res = SetRoleToUser(model.SelectedRoleId, user.Id);
                    if (res.Success == false)
                        return res;
                    else
                    {
                        _userRepository.Update(res.Data);
                    }
                }

                result.Success = true;
                result.Message = "User was updated succesfully";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "There is an error when updating user " + ex.Message;
            }

            return result;
        }

        public Result<List<UserListModel>> GetUsersFromRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return new Result<List<UserListModel>>()
                {
                    Success = false,
                    Message = "RoleName is cannot be blank or null!"
                };
            }
            else if (_roleRepository.GetRoleByName(roleName)== null)
            {
                return new Result<List<UserListModel>>()
                {
                    Success = false,
                    Message = "RoleName is couldn't find in database!"
                };

            }

            var users = _userRepository.GetUsersRole(roleName);

            var userList = new List<User>();
            foreach (User user in users)
            {
                foreach (Role role in user.Roles)
                {
                    if (role.Name.Equals(roleName))
                        userList.Add(user);
                }
            }


            List<UserListModel> userResponse = new List<UserListModel>();
            foreach (User user in userList)
            {
                var userItem = new UserListModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Code = user.Code,
                    BirthDate = user.BirthDate,
                    Occupation = user.Occupation,
                    Email = user.Email,
                    Password = user.Password,
                    Homebase = user.Homebase,
                    Phone = user.Phone,
                    PhonePrivate = user.PhonePrivate,
                    IsActive = user.IsActive,
                    CreateDate = user.InsertedDate
                };
                userResponse.Add(userItem);
            }

            return new Result<List<UserListModel>>()
            {
                Success = true,
                Message = "Users fetched successfully by RoleName",
                Data = userResponse
            };

        }


        public Result Update2(UserUpdateModel model)
        {

            var result = new Result();

            try
            {
                var user = _userRepository.GetById(model.Id);
                if (user == null)
                {
                    result.Success = false;
                    result.Message = "Users couln't find in database";
                    return result;
                }

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.BirthDate = model.BirthDate;
                user.Occupation = model.Occupation;
                user.Email = model.Email;
                user.Password = CryptoHelper.Encrypt(model.Password);
                user.Homebase = model.HomeBase;
                user.Phone = model.Phone;
                user.PhonePrivate = model.PhonePrivate;
                user.IsActive = model.IsActive;
                user.UpdatedDate = DateTime.Now;
                user.Roles = new List<Role>();



                user.Roles.Clear();
                _userRepository.Update(user);

                if (model.SelectedRoleId.Length > 0)
                {
                    foreach (var roleId in model.SelectedRoleId)
                    {
                        user.Roles.Add(_roleRepository.GetById(roleId));
                    }
                }
                _userRepository.Update(user);

                result.Success = true;
                result.Message = "User was updated succesfully";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "There is an error when updating user " + ex.Message;
            }

            return result;
        }


    }
}
