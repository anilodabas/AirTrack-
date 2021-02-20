using AirTrack.Core.Types;
using AirTrack.Model.Account.User;
using AirTrack.Service.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTrack.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("users")]
        public Result<List<UserListModel>> GetAll()
        {
            return _userService.GetAll();
        }

        [HttpGet("/users/{Id}")]
        public Result<UserListModel> GetById(int Id)
        {
            return _userService.Get(Id);
        }


        [HttpGet("/users/role/{Name}")]
        public Result<List<UserListModel>> GetUsersByRoleName(string Name)
        {
            return _userService.GetUsersFromRole(Name);
        }


        [Route("users/isexist")]
        [HttpGet]
        public Result<bool> IsUserExist(string code)
        {
            return _userService.IsUserExist(code);
        }


        [HttpPost]
        [Route("users/Create")]
        public Result Post([FromBody] UserCreateModel userCreateModel)
        {
            return _userService.Create(userCreateModel);

        }

        [HttpPut]
        [Route("users/Update")]
        public Result Put([FromBody] UserUpdateModel userUpdateModel)
        {
            return _userService.Update2(userUpdateModel);
        }

        [HttpDelete("users/Delete")]
        public Result Delete(int Id)
        {
            return _userService.Delete(Id);
        }
        
       






    }
}
