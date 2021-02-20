using AirTrack.Entity.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirTrack.Repository.Account
{
  public  interface IUserRepository
    {
        List<User> GetAll();

        User GetById(int Id);
        public List<User> GetUsersRole(string roleName);
        User Create(User user);
        User Update(User user);
        void Delete(int Id);
       


    }
}
