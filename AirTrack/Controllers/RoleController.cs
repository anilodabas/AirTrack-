using AirTrack.Core.Types;
using AirTrack.Model.Account.Role;
using AirTrack.Service.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet]
        [Route("Roles")]
        public Result<List<RoleListModel>> GetAll()
        {
            return _roleService.GetAll();
        }

        [HttpPost]
        [Route("Roles/Create")]

        public Result Post([FromBody] RoleCreateModel userCreateModel)
        {
            return _roleService.Create(userCreateModel);

        }

        [HttpPut]
        [Route("Roles/Update")]
        public Result Put([FromBody] RoleUpdateModel userUpdateModel)
        {
            return _roleService.Update(userUpdateModel);
        }

        [HttpDelete("Roles/Delete")]
        public Result Delete(int Id)
        {
            return _roleService.Delete(Id);
        }




    }
}
