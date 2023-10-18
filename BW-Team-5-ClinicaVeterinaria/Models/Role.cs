using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BW_Team_5_ClinicaVeterinaria.Models
{
    public class Role : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            ContextDbModel dbContext = new ContextDbModel();

            try
            {
                var userRoles = (from u in dbContext.Utente
                                 join r in dbContext.Ruoli on u.IdRuoli equals r.IdRuoli
                                 where u.Email == username
                                 select r.Ruolo).ToArray();

                return userRoles;
            }
            catch (Exception ex)
            {
                throw new Exception("Si è verificato un errore durante l'accesso ai ruoli dell'utente.", ex);
            }
            finally
            {
                dbContext.Dispose();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}