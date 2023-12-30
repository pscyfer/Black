using Common.Application.Exceptions;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Identity.Core.Repository.Users
{
    public class UserRepository : UserManager<User>, IUserRepository
    {
        public UserRepository(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, PersianIdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
        #region Methods
        public async Task<User?> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            try
            {
                return await Users.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<int> TotalCount()
        {
            try
            {
                return await Users.CountAsync();
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }
        #endregion

    }
}
