using OWDARO;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class RatingsBL
    {
        public static void Add(SC_Ratings entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_Ratings.Add(entity);

                context.SaveChanges();
            }
        }

        public static int CurrentRating(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return CurrentRating(productID, context);
            }
        }

        public static int CurrentRating(int productID, ShoppingCartEntities context)
        {
            var averageRating = (int)GetAverageRating(productID, context);
            return (averageRating <= 0) ? 0 : averageRating;
        }

        public static void Delete(SC_Ratings entity, ShoppingCartEntities context)
        {
            context.SC_Ratings.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_Ratings
                         where set.RatingID == id
                         select set);

            return query.Any();
        }

        public static double GetAverageRating(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetAverageRating(productID, context);
            }
        }

        public static double GetAverageRating(int productID, ShoppingCartEntities context)
        {
            var ratingsQuery = (from ratings in context.SC_Ratings
                                where ratings.ProductID == productID
                                select ratings);

            double averageRating = 0;

            foreach (SC_Ratings rating in ratingsQuery)
            {
                averageRating += rating.Rating;
            }

            averageRating = averageRating / ratingsQuery.Count();

            return averageRating;
        }

        public static SC_Ratings GetObjectbyID(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_Ratings
                         where set.RatingID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_Ratings GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static int ReviewsCount(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return ReviewsCount(productID, context);
            }
        }

        public static int ReviewsCount(int productID, ShoppingCartEntities context)
        {
            return (from ratings in context.SC_Ratings
                    where ratings.ProductID == productID
                    select ratings).Count();
        }

        public static void Save(SC_Ratings entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.RatingID, context))
                {
                    var query = GetObjectbyID(entity.RatingID, context);

                    query.ProductID = entity.ProductID;
                    query.Rating = entity.Rating;

                    context.SaveChanges();
                }
            }
        }

        public static bool ShowRating(int productID)
        {
            var show = false;

            var currentRating = CurrentRating(productID);

            if (currentRating >= AppConfig.MinimumProductRatingToShow)
            {
                show = true;
            }

            return show;
        }
    }
}