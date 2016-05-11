using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class CardTypesBL
    {
        public static void Add(SC_CardTypes cardTypeEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_CardTypes.Add(cardTypeEntity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_CardTypes entity, ShoppingCartEntities context)
        {
            context.SC_CardTypes.Remove(entity);

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
            var query = (from cardTypes in context.SC_CardTypes
                         where cardTypes.CardTypeID == id
                         select cardTypes);

            return query.Any();
        }

        public static SC_CardTypes GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_CardTypes GetObjectByID(int id, ShoppingCartEntities context)
        {
            var cardTypeQuery = (from cardTypes in context.SC_CardTypes
                                 where cardTypes.CardTypeID == id
                                 select cardTypes);

            return cardTypeQuery.FirstOrDefault();
        }

        public static void Save(SC_CardTypes cardTypeEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(cardTypeEntity.CardTypeID, context))
                {
                    var cardTypeFromDB = GetObjectByID(cardTypeEntity.CardTypeID, context);

                    cardTypeFromDB.Title = cardTypeEntity.Title;
                    cardTypeFromDB.Description = cardTypeEntity.Description;
                    cardTypeFromDB.Hide = cardTypeEntity.Hide;

                    context.SaveChanges();
                }
            }
        }
    }
}