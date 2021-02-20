using AirTrack.Entity;
using AirTrack.Entity.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirTrack.Repository.Account
{
    public class RoleRepository : IRoleRepository
    {
        public Role Create(Role role)
        {
            using (var context = new AirTrackContext())
            {
                context.Roles.Add(role);
                context.SaveChanges();
                return role;
            }
        }

      
        public void Delete(int Id)
        {
            using (var context = new AirTrackContext())
            {
                var role = GetById(Id);
                context.Roles.Remove(role);
                context.SaveChanges();
            }
        }

        public List<Role> GetAll()
        {
            using (var context = new AirTrackContext())
            {
               return context.Roles.ToList();
            }
        }

        public Role GetById(int Id)
        {
            using (var context = new AirTrackContext())
            {
               return context.Roles.Find(Id);

            }
        }


        public Role GetRoleByName(string name )
        {
            using (var context = new AirTrackContext())
            {
                return context.Roles.Where(x => x.Name.Equals(name)).FirstOrDefault();
            }

        }

 

        public Role Update(Role role)
        {
            using (var context = new AirTrackContext())
            {
                
                context.Roles.Update(role);
                context.SaveChanges();
                return role;
            }
        }

        public User ClearRolesByUser(User user)
        {
            using (var context = new AirTrackContext()) { 
            
                context.SaveChanges();
                return user;
            }
        }

    }
}
