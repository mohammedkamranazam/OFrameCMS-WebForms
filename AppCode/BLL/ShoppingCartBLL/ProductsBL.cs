using OWDARO;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ProductsBL
    {
        public static void Add(SC_Products entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(SC_Products entity, ShoppingCartEntities context)
        {
            context.SC_Products.Add(entity);

            context.SaveChanges();
        }

        public static bool DecreaseQuantity(int productID, double quantity)
        {
            using (var context = new ShoppingCartEntities())
            {
                return DecreaseQuantity(productID, quantity, context);
            }
        }

        public static bool DecreaseQuantity(int productID, double quantity, ShoppingCartEntities context)
        {
            var productEntity = GetObjectByID(productID, context);

            return DecreaseQuantity(productEntity, quantity, context);
        }

        public static bool DecreaseQuantity(SC_Products productEntity, double quantity, ShoppingCartEntities context)
        {
            var success = false;

            if (productEntity != null && IsQuantityAvailable(productEntity))
            {
                productEntity.AvailableQuantity -= quantity;
                try
                {
                    Save(productEntity, context);
                    success = true;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }

            return success;
        }

        public static void Delete(SC_Products entity, ShoppingCartEntities context)
        {
            context.SC_Products.Remove(entity);

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
            var query = (from products in context.SC_Products
                         where products.ProductID == id
                         select products);

            return query.Any();
        }

        public static string GetHasDiscountText(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetHasDiscountText(productID, context);
            }
        }

        public static string GetHasDiscountText(int productID, ShoppingCartEntities context)
        {
            var productQuery = GetObjectByID(productID, context);

            var hasDiscount = productQuery.HasDiscount;
            var discountPercentage = productQuery.DiscountPercentage;
            var discounAmount = productQuery.DiscountAmount;
            var discountText = string.Empty;

            if (hasDiscount == false)
            {
                return discountText;
            }
            else
            {
                if (discountPercentage > 0)
                {
                    discountText = string.Format("<span class='DiscountBadge'>{0}% off</span>", discountPercentage);
                }
                else
                {
                    if (discounAmount > 0)
                    {
                        discountText = string.Format("<span class='DiscountBadge'>Rs.{0} off</span>", discounAmount);
                    }
                }
                return discountText;
            }
        }

        public static SC_Products GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_Products GetObjectByID(int id, ShoppingCartEntities context)
        {
            var productQuery = (from products in context.SC_Products
                                where products.ProductID == id
                                select products);

            return productQuery.FirstOrDefault();
        }

        public static double GetPrice(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetPrice(productID, context);
            }
        }

        public static double GetPrice(int productID, ShoppingCartEntities context)
        {
            var productQuery = GetObjectByID(productID, context);

            var hasDiscount = productQuery.HasDiscount;
            var discountPercentage = productQuery.DiscountPercentage;
            var discounAmount = productQuery.DiscountAmount;
            var originalPrice = (double)productQuery.Price;
            double discountPrice = 0;

            if (hasDiscount == false)
            {
                return originalPrice;
            }
            else
            {
                if (discountPercentage > 0)
                {
                    discountPrice = originalPrice - (((double)discountPercentage / 100) * originalPrice);
                }
                else
                {
                    if (discounAmount > 0 && discounAmount <= originalPrice)
                    {
                        discountPrice = originalPrice - (double)discounAmount;
                    }
                }
                return discountPrice;
            }
        }

        public static string GetPriceText(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetPriceText(productID, context);
            }
        }

        public static string GetPriceText(int productID, ShoppingCartEntities context)
        {
            var productQuery = GetObjectByID(productID, context);

            var hasDiscount = productQuery.HasDiscount;
            var originalPrice = productQuery.Price;

            var priceText = string.Format("<span class='OriginalPrice'>Rs.{0}</span>", originalPrice);

            if (hasDiscount)
            {
                priceText = string.Format("<span class='OriginalPriceStriked'>Rs.{0}</span>&nbsp;<span class='DiscountPrice'>Rs.{1}</span>", originalPrice, GetPrice(productID, context));
            }

            return priceText;
        }

        public static bool IsHotItem(double soldOutCount)
        {
            return (soldOutCount >= AppConfig.ProductHotItemSoldOutCount) ? true : false;
        }

        public static bool IsNewProduct(DateTime uploadedOn)
        {
            var isNew = false;

            var timeDifference = Utilities.DateTimeNow() - uploadedOn;

            isNew = (timeDifference.Days <= AppConfig.KeepProductNewForDays) ? true : false;

            return isNew;
        }

        public static bool IsOutOfStock(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return IsOutOfStock(productID, context);
            }
        }

        public static bool IsOutOfStock(int productID, ShoppingCartEntities context)
        {
            var outOfStock = true;

            var productEntity = GetObjectByID(productID, context);

            var sectionID = (int)productEntity.SectionID;
            var categoryID = (int)productEntity.CategoryID;
            var subCategoryID = (int)productEntity.SubCategoryID;
            var itemNumber = productEntity.ItemNumber;

            var productsWithQuantity = (from set in context.SC_Products
                                        where set.AvailableQuantity > 0 &&
                                        set.ItemNumber == itemNumber &&
                                        set.SectionID == sectionID &&
                                        set.CategoryID == categoryID &&
                                        set.SubCategoryID == subCategoryID && set.Hide == false
                                        select set);

            if (productsWithQuantity.Any())
            {
                outOfStock = false;
            }

            return outOfStock;
        }

        public static bool IsQuantityAvailable(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return IsQuantityAvailable(GetObjectByID(productID, context));
            }
        }

        public static bool IsQuantityAvailable(SC_Products productEntity)
        {
            return productEntity != null ? productEntity.AvailableQuantity > 0 : false;
        }

        public static void Save(SC_Products entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(SC_Products entity, ShoppingCartEntities context)
        {
            if (Exists(entity.ProductID, context))
            {
                var query = GetObjectByID(entity.ProductID, context);

                query.Title = entity.Title;
                query.CostPrice = entity.CostPrice;
                query.IsDiscountRangeEnabled = entity.IsDiscountRangeEnabled;
                query.RemovePreOrderOnDate = entity.RemovePreOrderOnDate;
                query.PreOrderReleaseDate = entity.PreOrderReleaseDate;
                query.IsPriceOnRequest = entity.IsPriceOnRequest;
                query.IsDownloadable = entity.IsDownloadable;
                query.SubTitle = entity.SubTitle;
                query.Description = entity.Description;
                query.Price = entity.Price;
                query.AvailableQuantity = entity.AvailableQuantity;
                query.HasDiscount = entity.HasDiscount;
                query.DiscountPercentage = entity.DiscountPercentage;
                query.DiscountAmount = entity.DiscountAmount;
                query.PriceDescription = entity.PriceDescription;
                query.SpecialOffer = entity.SpecialOffer;
                query.Details = entity.Details;
                query.PreOderFlag = entity.PreOderFlag;
                query.PreOrderDescription = entity.PreOrderDescription;
                query.SectionID = entity.SectionID;
                query.CategoryID = entity.CategoryID;
                query.SubCategoryID = entity.SubCategoryID;
                query.BrandID = entity.BrandID;
                query.ProductModelID = entity.ProductModelID;
                query.ProductTypeID = entity.ProductTypeID;
                query.Hide = entity.Hide;
                query.AvailabilityTypeID = entity.AvailabilityTypeID;
                query.Model = entity.Model;
                query.Manufacturer = entity.Manufacturer;
                query.ShowInCart = entity.ShowInCart;
                query.ItemNumber = entity.ItemNumber;
                query.UnitID = entity.UnitID;
                query.MinOQ = entity.MinOQ;
                query.MaxOQ = entity.MaxOQ;
                query.Tags = entity.Tags;
                query.ColorID = entity.ColorID;
                query.SizeID = entity.SizeID;
                query.FloatField1 = entity.FloatField1;
                query.FloatField2 = entity.FloatField2;
                query.FloatField3 = entity.FloatField3;
                query.IntField1 = entity.IntField1;
                query.IntField2 = entity.IntField2;
                query.IntField3 = entity.IntField3;
                query.StringField1 = entity.StringField1;
                query.StringField2 = entity.StringField2;
                query.StringField3 = entity.StringField3;

                context.SaveChanges();
            }
        }

        public static SC_Products SwitchShowInCart(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return SwitchShowInCart(productID, context);
            }
        }

        public static SC_Products SwitchShowInCart(int productID, ShoppingCartEntities context)
        {
            return SwitchShowInCart(GetObjectByID(productID, context), context);
        }

        public static SC_Products SwitchShowInCart(SC_Products productEntity, ShoppingCartEntities context)
        {
            if (productEntity != null)
            {
                if (productEntity.ShowInCart && productEntity.AvailableQuantity <= 0)
                {
                    var productID = productEntity.ProductID;
                    var sectionID = productEntity.SectionID;
                    var categoryID = productEntity.CategoryID;
                    var subCategoryID = productEntity.SubCategoryID;
                    var itemNumber = productEntity.ItemNumber;

                    var productsByGroup = (from set in context.SC_Products
                                           where set.ProductID != productID &&
                                           set.AvailableQuantity > 0 &&
                                           set.ItemNumber == itemNumber &&
                                           set.SectionID == sectionID &&
                                           set.CategoryID == categoryID &&
                                           set.SubCategoryID == subCategoryID &&
                                           set.Hide == false
                                           select set);

                    if (productsByGroup.Any())
                    {
                        var productToShow = productsByGroup.FirstOrDefault();

                        return productToShow;
                    }
                }
            }

            return productEntity;
        }
    }
}