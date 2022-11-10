using Microsoft.EntityFrameworkCore;
using ProjectAPI.DataAccessLayer;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.DI
{
    public class UserRepository : IUser
    {
        public readonly PMSDbContext dbContext = null;
        public UserRepository(PMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> DeleteUser(string emailid)
        {
            var ar = await dbContext.userModels.Where(x => x.Email == emailid).FirstOrDefaultAsync();
            if (ar != null)
            {
                dbContext.userModels.Remove(ar);
            }
            await dbContext.SaveChangesAsync();
            return emailid;
        }

        public async Task<List<PolicyRegistrationModel>> GetbyType(string Policytype)
        {
            var ar = await dbContext.policyRegistrationModels.Where(x => x.PolicyType == Policytype).ToListAsync();
            return ar;
        }

        public async Task<string> Register(UserModel userModel)
        {
            dbContext.userModels.Add(userModel);
            await dbContext.SaveChangesAsync();
            return userModel.Email;
        }

        public async Task<List<PolicyRegistrationModel>> ShowPolicies()
        {
            var ar = await dbContext.policyRegistrationModels.ToListAsync();
            return ar;
        }

        public async Task<string> UpdateUser(string emailid, UserModel userModel)
        {
            var ar = await dbContext.userModels.Where(x => x.Email == userModel.Email).FirstOrDefaultAsync();
            if (ar != null)
            {
                ar.Password = userModel.Password;
                ar.CPassword = userModel.CPassword;
                ar.Address = userModel.Address;
                ar.ContactNo = userModel.ContactNo;
                ar.DOB = userModel.DOB;
                ar.Fname = userModel.Fname;
                ar.Lname = userModel.Lname;
                ar.Salary = userModel.Salary;
                ar.PANNo = userModel.PANNo;
            }
            await dbContext.SaveChangesAsync();
            return userModel.Email;
        }

        public async Task<string> UserLogin(UserModel userModel)
        {
            //var ar = await dbContext.userModels.Where(x =>x.Email == email && x.Password ==
            // password).FirstOrDefaultAsync();

            //return email;
            //if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            //{
            //    return null;
            //}
            var customer = await dbContext.userModels.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefaultAsync();
            if (customer == null)
            {
                return null;
            }
            return (customer.Email);
        }
        public async Task<string> ForgotPassword(string email)
        {
            var ar = await dbContext.userModels.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (ar == null)
            {
                return null;
            }
            else
            {
                return ar.Password;
            }
        }
    }
}
