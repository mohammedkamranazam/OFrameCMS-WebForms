using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.GalleryBLL
{
    public static class ActivityLogsBL
    {
        public static void Add(OW_ActivityLogs entity)
        {
            using (var context = new OWDAROEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(OW_ActivityLogs entity, OWDAROEntities context)
        {
            context.OW_ActivityLogs.Add(entity);

            context.SaveChanges();
        }

        public static async Task AddAsync(OW_ActivityLogs entity)
        {
            using (var context = new OWDAROEntities())
            {
                await AddAsync(entity, context);
            }
        }

        public static async Task AddAsync(OW_ActivityLogs entity, OWDAROEntities context)
        {
            context.OW_ActivityLogs.Add(entity);

            await context.SaveChangesAsync();
        }

        public static void Delete(OW_ActivityLogs entity)
        {
            using (var context = new OWDAROEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(OW_ActivityLogs entity, OWDAROEntities context)
        {
            context.OW_ActivityLogs.Remove(entity);

            context.SaveChanges();
        }

        public static OW_ActivityLogs GetObjectByID(Guid id)
        {
            using (var context = new OWDAROEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static OW_ActivityLogs GetObjectByID(Guid id, OWDAROEntities context)
        {
            var entity = (from set in context.OW_ActivityLogs
                          where set.ActivityLogID == id
                          select set).FirstOrDefault();

            return entity;
        }
    }
}