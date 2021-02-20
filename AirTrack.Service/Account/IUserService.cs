using AirTrack.Core.Types;
using AirTrack.Entity.Account;
using AirTrack.Model.Account.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirTrack.Service.Account
{
    public interface IUserService
    {
        Result<List<UserListModel>> GetAll();
        Result<UserListModel> Get(int id);
        Result<List<UserListModel>> GetUsersFromRole(string roleName);
        Result Create(UserCreateModel model);

        Result Delete(int Id);

        Result Update(UserUpdateModel model);

        Result Update2(UserUpdateModel model);


        Result<bool> IsUserExist(string code);

        Result<User> SetRoleToUser(int[] userId,int roleId);

        
    }
}
