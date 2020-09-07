using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SampleTrackingUi.ViewModels;

namespace SampleTrackingUi.Data
{
    public class UserStore : IUserStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>,
        IUserTwoFactorStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        static HttpClient client = new HttpClient();
        private static string _baseAddress;

        private readonly string _connectionString;

        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration["database:connectionString"];
            _baseAddress = configuration.GetSection("Api").GetSection("BaseAddress").Value;
        }

        //int userId = 0;
        //var conn = new SqlConnection(_connectionString);
        //var cmd = new SqlCommand("sp_IST_AddUser", conn);
        //cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar,256).Value = user.UserName;
        //        cmd.Parameters.Add("@UserNameNormalized", System.Data.SqlDbType.NVarChar, 256).Value = user.UserNameNormalized;
        //        cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 256).Value = user.Email;
        //        cmd.Parameters.Add("@EmailNormalized", System.Data.SqlDbType.NVarChar, 256).Value = user.EmailNormalized;
        //        cmd.Parameters.Add("@PasswordHash", System.Data.SqlDbType.NVarChar).Value = user.PasswordHash;
        //        cmd.Parameters.Add("@StatusId", System.Data.SqlDbType.NChar).Value = user.Status;
        //        cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.Int).Value = user.CreatedBy;
        //        cmd.Parameters.Add("@CreatedDate", SqlDbType.Date).Value = user.CreatedDateTime;
        //        cmd.Parameters.Add("@ModifyBy", System.Data.SqlDbType.Int).Value = user.ModifyBy;
        //        cmd.Parameters.Add("@ModifyDate", SqlDbType.Date).Value = user.ModifyDateTime;
        //        var returnParameter = cmd.Parameters.Add("@PurchaseId", SqlDbType.Int);
        //returnParameter.Direction = ParameterDirection.ReturnValue;
        //    try
        //    {
        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //            userId = Convert.ToInt32(returnParameter.Value);
        //    }
        //        catch (Exception ex)
        //        {
        //            //_logger.LogError(ex, $"Add Purchase Detail {ex.Message}");
        //            throw;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }



        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                user.UserId = await connection.QuerySingleAsync<int>($@"INSERT INTO [IST_User] ([UserName], [NormalizedUserName], [Email],
                    [NormalizedEmail], [PasswordHash], [StatusId], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate])
                    VALUES (@{nameof(ApplicationUser.UserName)}, @{nameof(ApplicationUser.UserNameNormalized)}, @{nameof(ApplicationUser.Email)},
                    @{nameof(ApplicationUser.EmailNormalized)}, @{nameof(ApplicationUser.PasswordHash)}, @{nameof(ApplicationUser.Status)}, 
                    @{nameof(ApplicationUser.CreatedBy)}, @{nameof(ApplicationUser.CreatedDateTime)}, @{nameof(ApplicationUser.ModifyBy)}, 
    `               @{nameof(ApplicationUser.ModifyDateTime)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [IST_User] WHERE [UserId] = @{nameof(ApplicationUser.UserId)}", user);
            }

            return IdentityResult.Success;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM [IST_User]
                    WHERE [UserId] = @{nameof(userId)}", new { userId });
            }
        }


        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM [IST_User]
                    WHERE [EmailNormalized] = @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserNameNormalized);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserNameNormalized = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                await connection.ExecuteAsync($@"UPDATE [IST_User] SET
                    [UserName] = @{nameof(ApplicationUser.UserName)},
                    [UserNameNormalized] = @{nameof(ApplicationUser.UserNameNormalized)},
                    [Email] = @{nameof(ApplicationUser.Email)},
                    [EmailNormalized] = @{nameof(ApplicationUser.EmailNormalized)},
                    [PasswordHash] = @{nameof(ApplicationUser.PasswordHash)}
                    WHERE [UserId] = @{nameof(ApplicationUser.UserId)}", user);
            }

            return IdentityResult.Success;
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM [IST_User]
                    WHERE [EmailNormalized] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailNormalized);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.EmailNormalized = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM [IST_ApplicationRole] WHERE [NormalizedName] = @{nameof(normalizedName)}", new { normalizedName });
                if (!roleId.HasValue)
                    roleId = await connection.ExecuteAsync($"INSERT INTO [IST_ApplicationRole]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                        new { roleName, normalizedName });

                await connection.ExecuteAsync($"IF NOT EXISTS(SELECT 1 FROM [IST_ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}) " +
                    $"INSERT INTO [IST_ApplicationUserRole]([UserId], [RoleId]) VALUES(@userId, @{nameof(roleId)})",
                    new { userId = user.UserId, roleId });
            }
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [IST_ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (!roleId.HasValue)
                    await connection.ExecuteAsync($"DELETE FROM [IST_ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.UserId, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResults = await connection.QueryAsync<string>("SELECT r.[Name] FROM [IST_ApplicationRole] r INNER JOIN [IST_ApplicationUserRole] ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.UserId });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [IST_ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (roleId == default(int)) return false;
                var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM [IST_ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                    new { userId = user.UserId, roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var queryResults = await connection.QueryAsync<ApplicationUser>("SELECT u.* FROM [IST_User] u " +
                    "INNER JOIN [IST_ApplicationUserRole] ur ON ur.[UserId] = u.[UserId] INNER JOIN [IST_ApplicationRole] r ON r.[UserId] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
