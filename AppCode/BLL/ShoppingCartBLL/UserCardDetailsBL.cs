using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class UserCardDetailsBL
    {
        public static void Add(SC_UserCardDetails entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_UserCardDetails.Add(entity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_UserCardDetails entity, ShoppingCartEntities context)
        {
            context.SC_UserCardDetails.Remove(entity);

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
            var query = (from set in context.SC_UserCardDetails
                         where set.UserCardDetailsID == id
                         select set);

            return query.Any();
        }

        public static SC_UserCardDetails GetObjectbyID(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_UserCardDetails
                         where set.UserCardDetailsID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_UserCardDetails GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static void Save(SC_UserCardDetails entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.UserCardDetailsID, context))
                {
                    var query = GetObjectbyID(entity.UserCardDetailsID, context);

                    query.CardNumber = entity.CardNumber;
                    query.CardTypeID = entity.CardTypeID;
                    query.NameOnCard = entity.NameOnCard;
                    query.SecurityCode = entity.SecurityCode;
                    query.Title = entity.Title;
                    query.Username = entity.Username;
                    query.ValidFrom = entity.ValidFrom;
                    query.ValidTill = entity.ValidTill;

                    context.SaveChanges();
                }
            }
        }
    }
}