using AirTrack.Core.Types;
using AirTrack.Model.Account.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirTrack.Service.Account
{
   public interface IRoleService
    {
        Result<List<RoleListModel>> GetAll();

        Result Create(RoleCreateModel model);

        Result Delete(int Id);

        Result Update(RoleUpdateModel model);
        
       
    }
}
