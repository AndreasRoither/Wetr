using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoUserDao : IUserDao
    {
        private readonly AdoTemplate template;

        public AdoUserDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static User MapRow(IDataRecord row)
        {
            return new User()
            {
                UserId = (int)row["userId"],
                FirstName = (string)row["firstName"],
                LastName = (string)row["lastName"],
                Password = (string)row["password"],
                Email = (string)row["email"],
            };
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            return await this.template.ExecuteAsync(
               @"delete from user" +
                   "where userId = @userId",
               new Parameter("@userId", userId)) == 1;
        }

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from user", MapRow);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var result = await this.template.QueryAsync(
                "select * from user where email = @email",
                MapRow,
                new Parameter("@email", email));

            return result.SingleOrDefault();
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            var result = await this.template.QueryAsync(
               "select * from user where userId = @userId",
               MapRow,
               new Parameter("@userId", userId));

            return result.SingleOrDefault();
        }

        public async Task<bool> InsertAsync(User user)
        {
            // no id since it's set to auto increment
            return await this.template.ExecuteAsync(
                @"insert into user (firstName, lastName, password, email) VALUES" +
                    "(@firstName, @lastName, @password, @email)",
                new Parameter("@firstName", user.FirstName),
                new Parameter("@lastName", user.LastName),
                new Parameter("@password", user.Password),
                new Parameter("@email", user.Email)) == 1;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            return await this.template.ExecuteAsync(
                @"update user set" +
                    "firstName = @firstName" +
                    "lastName = @lastName" +
                    "password = @password" +
                    "email = @email" +
                "where userId = @userId",
                new Parameter("@firstName", user.FirstName),
                new Parameter("@lastName", user.LastName),
                new Parameter("@password", user.Password),
                new Parameter("@email", user.Email),
                new Parameter("@userId", user.UserId)) == 1;
        }
    }
}