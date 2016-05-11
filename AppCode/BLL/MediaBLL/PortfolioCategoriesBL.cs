using ProjectJKL.AppCode.DAL.MediaModel;
using System.Linq;

namespace OWDARO.BLL.MediaBLL
{
    public static class PortfolioCategoriesBL
    {
        public static void Add(ME_PortfolioCategories entity)
        {
            using (var context = new MediaEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(ME_PortfolioCategories entity, MediaEntities context)
        {
            context.ME_PortfolioCategories.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(int id)
        {
            using (var context = new MediaEntities())
            {
                Delete(id, context);
            }
        }

        public static void Delete(int id, MediaEntities context)
        {
            var query = GetObjectByID(id, context);

            context.ME_PortfolioCategories.Remove(query);

            context.SaveChanges();
        }

        public static bool Exists(int projectCategoryID, int portfolioID)
        {
            using (var context = new MediaEntities())
            {
                return Exists(projectCategoryID, portfolioID, context);
            }
        }

        public static bool Exists(int projectCategoryID, int portfolioID, MediaEntities context)
        {
            var query = (from set in context.ME_PortfolioCategories
                         where set.PortfolioID == portfolioID && set.ProjectCategoryID == projectCategoryID
                         select set);
            return query.Any();
        }

        public static ME_PortfolioCategories GetObjectByID(int id)
        {
            using (var context = new MediaEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static ME_PortfolioCategories GetObjectByID(int id, MediaEntities context)
        {
            var entity = (ME_PortfolioCategories)(from set in context.ME_PortfolioCategories
                                                  where set.PortfolioCategoryID == id
                                                  select set).FirstOrDefault();

            return entity;
        }

        public static bool RelatedRecordsExists(int id)
        {
            using (var context = new MediaEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(int id, MediaEntities context)
        {
            return false;
        }

        public static void Save(MediaEntities context)
        {
            context.SaveChanges();
        }
    }
}