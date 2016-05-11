using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ReviewsBL
    {
        public static void Add(SC_Reviews entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_Reviews.Add(entity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_Reviews entity, ShoppingCartEntities context)
        {
            context.SC_Reviews.Remove(entity);

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
            var query = (from set in context.SC_Reviews
                         where set.ReviewID == id
                         select set);

            return query.Any();
        }

        public static SC_Reviews GetObjectbyID(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_Reviews
                         where set.ReviewID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_Reviews GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static int GetReviewsCount(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetReviewsCount(productID, context);
            }
        }

        public static int GetReviewsCount(int productID, ShoppingCartEntities context)
        {
            var reviewsQuery = (from reviews in context.SC_Reviews
                                where reviews.ProductID == productID && reviews.Hide == false && reviews.Approved == true
                                select reviews);

            return reviewsQuery.Count();
        }

        public static void Save(SC_Reviews entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.ReviewID, context))
                {
                    var query = GetObjectbyID(entity.ReviewID, context);

                    query.ProductID = entity.ProductID;
                    query.DateTime = entity.DateTime;
                    query.Username = entity.Username;
                    query.Review = entity.Review;
                    query.Approved = entity.Approved;
                    query.Hide = entity.Hide;
                    query.RatingID = entity.RatingID;

                    context.SaveChanges();
                }
            }
        }
    }
}