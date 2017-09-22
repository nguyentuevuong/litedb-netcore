using LiteDB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LiteDb.AspNetCore.Identity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Id = ObjectId.NewObjectId().ToString();
            Roles = new List<string>();
            Logins = new List<UserLoginInfo>();
            SerializableLogins = new List<SerializableUserLoginInfo>();
            Claims = new List<IdentityUserClaim>();
        }

        [BsonId]
        public new string Id { get; set; }

        public string FullName { get; set; }

        public new EmailInfo Email { get; set; }

        public LockoutInfo Lockout { get; internal set; }

        public PhoneInfo Phone { get; internal set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual List<string> Roles { get; set; }

        public bool UsesTwoFactorAuthentication { get; internal set; }

        public virtual void AddRole(string role) => Roles.Add(role);

        public virtual void RemoveRole(string role) => Roles.Remove(role);

        public List<SerializableUserLoginInfo> SerializableLogins { get; set; }

        [BsonIgnore]
        public virtual List<UserLoginInfo> Logins
        {
            get
            {
                return SerializableLogins?.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey, "")).ToList() ?? new List<UserLoginInfo>();
            }
            set
            {
                SerializableLogins = value?.Select(x => new SerializableUserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList() ?? new List<SerializableUserLoginInfo>();
            }
        }

        public virtual void AddLogin(UserLoginInfo login)
        {
            SerializableLogins.Add(new SerializableUserLoginInfo(login.LoginProvider, login.ProviderKey));
        }

        public virtual void RemoveLogin(UserLoginInfo login)
        {
            var loginsToRemove = SerializableLogins
                .Where(l => l.LoginProvider == login.LoginProvider)
                .Where(l => l.ProviderKey == login.ProviderKey);

            SerializableLogins = SerializableLogins.Except(loginsToRemove).ToList();
        }

        public virtual bool HasPassword() => false;

        public virtual List<IdentityUserClaim> Claims { get; set; }

        public virtual void AddClaim(Claim claim) => Claims.Add(new IdentityUserClaim(claim));

        public virtual void RemoveClaim(Claim claim)
        {
            var claimsToRemove = Claims
                .Where(c => c.Type == claim.Type)
                .Where(c => c.Value == claim.Value);

            Claims = Claims.Except(claimsToRemove).ToList();
        }
    }
}
