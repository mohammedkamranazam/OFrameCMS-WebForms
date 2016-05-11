using ProjectJKL.AppCode.DAL.MembershipModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.MembershipBLL
{
    public static class UserEducationBL
    {
        public static void DeleteUserEducation(int id)
        {
            using (var context = new MembershipEntities())
            {
                DeleteUserEducation(id, context);
            }
        }

        public static void DeleteUserEducation(int id, MembershipEntities context)
        {
            var query = (from userEducations in context.MS_UserEducations
                         where userEducations.EducationID == id
                         select userEducations);

            if (query.Any())
            {
                context.MS_UserEducations.Remove(query.FirstOrDefault());
                context.SaveChanges();
            }
        }

        public static async Task DeleteUserEducationAsync(int id)
        {
            using (var context = new MembershipEntities())
            {
                await DeleteUserEducationAsync(id, context);
            }
        }

        public static async Task DeleteUserEducationAsync(int id, MembershipEntities context)
        {
            var query = await (from userEducations in context.MS_UserEducations
                               where userEducations.EducationID == id
                               select userEducations).FirstOrDefaultAsync();

            if (query != null)
            {
                context.MS_UserEducations.Remove(query);
                await context.SaveChangesAsync();
            }
        }
    }
}