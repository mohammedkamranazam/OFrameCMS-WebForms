using ProjectJKL.AppCode.DAL.MembershipModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.MembershipBLL
{
    public static class UserAddressBL
    {
        public static void DeleteUserAddress(int id)
        {
            using (var context = new MembershipEntities())
            {
                DeleteUserAddress(id, context);
            }
        }

        public static void DeleteUserAddress(int id, MembershipEntities context)
        {
            var query = (from userAddresses in context.MS_UserAdresses
                         where userAddresses.AddressID == id
                         select userAddresses);

            if (query.Any())
            {
                context.MS_UserAdresses.Remove(query.FirstOrDefault());
            }

            context.SaveChanges();
        }

        public static async Task DeleteUserAddressAsync(int id)
        {
            using (var context = new MembershipEntities())
            {
                await DeleteUserAddressAsync(id, context);
            }
        }

        public static async Task DeleteUserAddressAsync(int id, MembershipEntities context)
        {
            var query = await (from userAddresses in context.MS_UserAdresses
                               where userAddresses.AddressID == id
                               select userAddresses).FirstOrDefaultAsync();

            if (query != null)
            {
                context.MS_UserAdresses.Remove(query);

                await context.SaveChangesAsync();
            }
        }
    }
}