using ProjectJKL.AppCode.DAL.MembershipModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.MembershipBLL
{
    public static class UserWorkBL
    {
        public static void DeleteUserWork(int id)
        {
            using (var context = new MembershipEntities())
            {
                DeleteUserWork(id, context);
            }
        }

        public static void DeleteUserWork(int id, MembershipEntities context)
        {
            var query = (from userWorks in context.MS_UserWorks
                         where userWorks.WorkID == id
                         select userWorks);

            if (query.Any())
            {
                context.MS_UserWorks.Remove(query.FirstOrDefault());
            }

            context.SaveChanges();
        }

        public static async Task DeleteUserWorkAsync(int id)
        {
            using (var context = new MembershipEntities())
            {
                await DeleteUserWorkAsync(id, context);
            }
        }

        public static async Task DeleteUserWorkAsync(int id, MembershipEntities context)
        {
            var query = await (from userWorks in context.MS_UserWorks
                               where userWorks.WorkID == id
                               select userWorks).FirstOrDefaultAsync();

            if (query != null)
            {
                context.MS_UserWorks.Remove(query);

                await context.SaveChangesAsync();
            }
        }
    }
}