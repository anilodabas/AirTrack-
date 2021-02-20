using AirTrack.Entity;
using AirTrack.Entity.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirTrack.Repository.Account
{
    public class UserRepository : IUserRepository
    {
        public User Create(User user)
        {
            using (var context = new AirTrackContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }

        public void Delete(int Id)
        {
            using (var context = new AirTrackContext())
            {
                var user = GetById(Id);
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public List<User> GetAll()
        {
            using (var context = new AirTrackContext())
            {
                
                var users = context.Users.Include(x => x.Roles).ToList<User>();
                context.SaveChanges();
                return users;    
            }

        }

        public User GetById(int Id)
        {
            using (var context = new AirTrackContext())
            {

                // var user = context.Users.Include(x => x.Roles).Where(x=>x.Id == Id).FirstOrDefault();
                return context.Users.Find();

            }
        }

        public User Update(User user)
        {
            using (var context = new AirTrackContext())
            {
                context.Users.Update(user);
              
                context.SaveChanges();
                return user;
            }
        }


        public List<User> GetUsersRole(string roleName)
        {
            using (var context = new AirTrackContext())
            {
                var users = context.Users.Include(u => u.Roles).ToList();  
                return users;
            }
        }

    }
}
