using AirTrack.Core.Types;
using AirTrack.Entity.Account;
using AirTrack.Model.Account.Role;
using AirTrack.Repository.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirTrack.Service.Account
{
    
    public class RoleService : IRoleService
    {
            private IRoleRepository _roleRepository;
            private IUserRepository _userRepository;

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
            {
                _roleRepository = roleRepository;
               _userRepository = userRepository;
            } 
            public Result Create(RoleCreateModel model)
        {
            var result = new Result();
            var roles = _roleRepository.GetAll(); 
            var userInputRole = model.Name;
            foreach (Role role  in roles)
            {
                if (role.Name == userInputRole)
                {
                    result.Success = false;
                    result.Message = "This role already exists";
                    return result;

                }
            }

                try

                {

                var role = new Role()

                {
                    Name = model.Name,
                    IsActive = model.IsActive
                    
                        
                      
                    };
                //TODO: CryptoHelperdan parola cryptolanacak 
                role.InsertedDate = DateTime.Now;
                _roleRepository.Create(role);

               

                result.Success = true;
                    result.Message = "Role was created succesfully";

                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = "It has a an error when creating role " + ex.Message;
                }

                return result;
            }

        public Result Delete(int Id)
        {
            var result = new Result();
      
            try
            {
                var user = _roleRepository.GetById(Id);
                if (user == null)
                {
                    result.Success = false;
                    result.Message = "Couldn't find user     id  which taken by input";
                    return result;
                }

                user.IsActive = false;
                user.IsDeleted = true;

                user.UpdatedDate = DateTime.Now;
                _roleRepository.Update(user);
                
                

                result.Success = true;
                result.Message = "User was deleted successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "It has a an error when creating user " + ex.Message;
            }
            return result;
        }

        public Result<List<RoleListModel>> GetAll()
        {
            var result = new Result<List<RoleListModel>>();
            try
            {
                var roles = _roleRepository.GetAll().ToList();
                List<RoleListModel> usersModel = new List<RoleListModel>();

                foreach (Role role in roles)
                {
                    if (!role.IsDeleted)
                    {
                        usersModel.Add(new RoleListModel()
                        {
                            Id = role.Id,
                            Name = role.Name,
                           IsActive = role.IsActive

                        });;
                    }

                }
                result.Success = true;
                result.Message = "All users fetched successfully";
                result.Data = usersModel;

            }
            catch (Exception ex)
            {
                result.Success = true;
                result.Message = "It has an error when fetching users! " + ex;
            }

            return result;
        }

       
        public Result Update(RoleUpdateModel model)
        {
            
            var result = new Result();
           
            try
            {
                 

                var role = _roleRepository.GetById(model.Id);
                if (role == null)
                {
                    result.Success = false;
                    result.Message = "Role not found";
                    return result;
                }

                role.Name = model.Name;
                role.IsActive = model.IsActive;
                role.UpdatedDate = DateTime.Now;
               
               
              
                //TODO: CryptoHelperdan parola cryptolanacak 

                _roleRepository.Update(role);
                result.Success = true;
                result.Message = "Role was updated succesfully";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "It has a an error when creating role " + ex.Message;
            }

            return result;
        }
    }
}
