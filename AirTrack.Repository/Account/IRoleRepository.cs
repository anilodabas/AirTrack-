using AirTrack.Entity.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirTrack.Repository.Account
{
    public interface IRoleRepository
    {
        List<Role> GetAll();

        Role GetById(int Id);
        Role Create(Role role);
        Role Update(Role role);
        void Delete(int Id);
        public Role GetRoleByName(string name);

        public User ClearRolesByUser(User user);




    }
}
