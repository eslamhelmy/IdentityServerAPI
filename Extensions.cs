using UserIdentity.Entities;
using UserIdentity.Records;

namespace UserIdentity.Extensions
{

    public static class Extensions
    {
        public static UserDto AsDto(this ApplicationUser user)
        {
            return new UserDto(
                user.Id, 
                user.UserName, 
                user.IsApproved);
        }
    }
}