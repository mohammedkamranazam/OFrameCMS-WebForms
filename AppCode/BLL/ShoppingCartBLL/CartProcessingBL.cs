using OWDARO;
using OWDARO.Helpers;
using OWDARO.ILL;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class CartProcessingBL
    {
        public static bool AddOrderDetails(string orderNumber, MembershipUser user, ShoppingCartEntities context)
        {
            var success = false;

            if (user != null)
            {
                var userCartQuery = (from userCarts in context.SC_UserCart
                                     where userCarts.Username == user.UserName
                                     select userCarts);

                if (userCartQuery.Any())
                {
                    foreach (SC_UserCart userCart in userCartQuery)
                    {
                        var orderDetailsEntity = new SC_OrderDetails();
                        orderDetailsEntity.OrderNumber = orderNumber;
                        orderDetailsEntity.Price = ProductsBL.GetPrice(userCart.ProductID, context) * userCart.Quantity;
                        orderDetailsEntity.ProductID = userCart.ProductID;
                        orderDetailsEntity.Quantity = userCart.Quantity;

                        context.SC_OrderDetails.Add(orderDetailsEntity);
                    }

                    try
                    {
                        context.SaveChanges();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                    }
                }
            }
            else
            {
                var anonymousID = MembershipHelper.GetAnonymousID();

                var tempCartQuery = (from tempCarts in context.SC_TempCart
                                     where tempCarts.AnonymousUserID == anonymousID
                                     select tempCarts);

                if (tempCartQuery.Any())
                {
                    foreach (SC_TempCart tempCart in tempCartQuery)
                    {
                        var orderDetailsEntity = new SC_OrderDetails();
                        orderDetailsEntity.OrderNumber = orderNumber;
                        orderDetailsEntity.Price = ProductsBL.GetPrice(tempCart.ProductID, context);
                        orderDetailsEntity.ProductID = tempCart.ProductID;
                        orderDetailsEntity.Quantity = tempCart.Quantity;

                        context.SC_OrderDetails.Add(orderDetailsEntity);
                    }

                    try
                    {
                        context.SaveChanges();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                    }
                }
            }

            return success;
        }

        public static bool AddToGuestOrders(string orderNumber, string emailID, string mobile, ShoppingCartEntities context)
        {
            var success = true;

            var guestOrdersEntity = new SC_GuestOrders();
            guestOrdersEntity.OrderNumber = orderNumber;
            guestOrdersEntity.EmailID = emailID;
            guestOrdersEntity.Mobile = mobile;

            context.SC_GuestOrders.Add(guestOrdersEntity);

            try
            {
                context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }

            return success;
        }

        public static void AddToTempCart(SC_TempCart tempCart)
        {
            using (var context = new ShoppingCartEntities())
            {
                AddToTempCart(tempCart, context);
            }
        }

        public static void AddToTempCart(SC_TempCart tempCart, ShoppingCartEntities context)
        {
            var tempCartEntity = new SC_TempCart();

            tempCartEntity.ProductID = tempCart.ProductID;
            tempCartEntity.Quantity = tempCart.Quantity;
            tempCartEntity.AnonymousUserID = tempCart.AnonymousUserID;

            context.SC_TempCart.Add(tempCartEntity);

            context.SaveChanges();
        }

        public static void AddToUserCart(SC_UserCart userCart)
        {
            using (var context = new ShoppingCartEntities())
            {
                AddToUserCart(userCart, context);
            }
        }

        public static void AddToUserCart(SC_UserCart userCart, ShoppingCartEntities context)
        {
            var userCartEntity = new SC_UserCart();

            userCartEntity.ProductID = userCart.ProductID;
            userCartEntity.Quantity = userCart.Quantity;
            userCartEntity.Username = userCart.Username;

            context.SC_UserCart.Add(userCartEntity);

            context.SaveChanges();
        }

        public static bool AddToUserOrders(string orderNumber, MembershipUser user, ShoppingCartEntities context)
        {
            var success = true;

            var userOrdersEntity = new SC_UserOrders();
            userOrdersEntity.OrderNumber = orderNumber;
            userOrdersEntity.Username = user.UserName;

            context.SC_UserOrders.Add(userOrdersEntity);

            try
            {
                context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }

            return success;
        }

        public static double CalculateShipmentCost(MS_UserAdresses deliveryAddress)
        {
            return 0;
        }

        public static void CheckPaymentConfirmation()
        {
            var user = Membership.GetUser();

            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["PaymentDone"]) && !string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["OrderNumber"]))
            {
                var paymentDone = DataParser.BoolParse(HttpContext.Current.Request.QueryString["PaymentDone"]);
                var orderNumber = HttpContext.Current.Request.QueryString["OrderNumber"];

                using (var context = new ShoppingCartEntities())
                {
                    if (paymentDone)
                    {
                        if (!IsOrderTimedOut(user, context))
                        {
                            OnSuccess(user, orderNumber, context);
                        }
                        else
                        {
                            if (DoesCartHaveOutOfStockItems(user, context))
                            {
                                OnFailure(user, orderNumber, context);
                                UpdateOrderStatus(context, orderNumber, false, false, true, true, true, false, false, false);
                            }
                            else
                            {
                                OnSuccess(user, orderNumber, context);
                            }
                        }
                    }
                    else
                    {
                        OnFailure(user, orderNumber, context);
                        UpdateOrderStatus(context, orderNumber, false, false, true, false, true, false, false, false);
                    }
                }
            }
        }

        public static bool DoesCartHaveOutOfStockItems(MembershipUser user, ShoppingCartEntities context)
        {
            if (user == null)
            {
                var anonymousUserID = MembershipHelper.GetAnonymousID();

                var tempCartsQuery = (from tempCarts in context.SC_TempCart
                                      where tempCarts.AnonymousUserID == anonymousUserID
                                      select tempCarts);

                if (tempCartsQuery.Any())
                {
                    foreach (SC_TempCart tempCartQuery in tempCartsQuery)
                    {
                        if (IsProductOutOfStock(context, tempCartQuery.SC_Products))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                var userCartsQuery = (from userCarts in context.SC_UserCart
                                      where userCarts.Username == user.UserName
                                      select userCarts);

                if (userCartsQuery.Any())
                {
                    foreach (SC_UserCart userCartQuery in userCartsQuery)
                    {
                        if (IsProductOutOfStock(context, userCartQuery.SC_Products))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static void EmptyCart(MembershipUser user, ShoppingCartEntities context)
        {
            if (user == null)
            {
                EmptyTempCart(MembershipHelper.GetAnonymousID(), context);
            }
            else
            {
                EmptyUserCart(user.UserName, context);
            }
        }

        public static void EmptyTempCart(string anonymousUserID)
        {
            using (var context = new ShoppingCartEntities())
            {
                EmptyTempCart(anonymousUserID, context);
            }
        }

        public static void EmptyTempCart(string anonymousUserID, ShoppingCartEntities context)
        {
            var tempCartQuery = (from tempCarts in context.SC_TempCart
                                 where tempCarts.AnonymousUserID == anonymousUserID
                                 select tempCarts);

            foreach (SC_TempCart tempCart in tempCartQuery)
            {
                context.SC_TempCart.Remove(tempCart);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        public static void EmptyUserCart(string username)
        {
            using (var context = new ShoppingCartEntities())
            {
                EmptyUserCart(username, context);
            }
        }

        public static void EmptyUserCart(string username, ShoppingCartEntities context)
        {
            var userCartQuery = (from userCarts in context.SC_UserCart
                                 where userCarts.Username == username
                                 select userCarts);

            foreach (SC_UserCart userCart in userCartQuery)
            {
                context.SC_UserCart.Remove(userCart);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        public static int GetCartCount()
        {
            var user = Membership.GetUser();

            return GetCartCount(user);
        }

        public static int GetCartCount(ShoppingCartEntities context)
        {
            var user = Membership.GetUser();

            return GetCartCount(user, context);
        }

        public static int GetCartCount(MembershipUser user)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetCartCount(user, context);
            }
        }

        public static int GetCartCount(MembershipUser user, ShoppingCartEntities context)
        {
            var count = 0;

            if (user == null)
            {
                var anonymousID = MembershipHelper.GetAnonymousID();

                var tempCartQuery = (from tempCarts in context.SC_TempCart
                                     where tempCarts.AnonymousUserID == anonymousID
                                     select tempCarts);

                count = tempCartQuery.Count();
            }
            else
            {
                var userCartQuery = (from userCarts in context.SC_UserCart
                                     where userCarts.Username == user.UserName
                                     select userCarts);

                count = userCartQuery.Count();
            }

            return count;
        }

        public static double GetCartSubTotal(MembershipUser user)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetCartSubTotal(user, context);
            }
        }

        public static double GetCartSubTotal(MembershipUser user, ShoppingCartEntities context)
        {
            double totalAmount = 0;

            if (user == null)
            {
                var anonymousID = MembershipHelper.GetAnonymousID();

                var query = (from tempCarts in context.SC_TempCart
                             where tempCarts.AnonymousUserID == anonymousID
                             select tempCarts);

                if (query.Any())
                {
                    foreach (SC_TempCart tempCart in query)
                    {
                        totalAmount = totalAmount + ((double)tempCart.SC_Products.Price * tempCart.Quantity);
                    }
                }
            }
            else
            {
                var query = (from userCarts in context.SC_UserCart
                             where userCarts.Username == user.UserName
                             select userCarts);

                if (query.Any())
                {
                    foreach (SC_UserCart userCarts in query)
                    {
                        totalAmount = totalAmount + ((double)userCarts.SC_Products.Price * userCarts.Quantity);
                    }
                }
            }

            return totalAmount;
        }

        public static double GetCartTotal()
        {
            var user = Membership.GetUser();

            return GetCartTotal(user);
        }

        public static double GetCartTotal(MembershipUser user)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetCartTotal(user, context);
            }
        }

        public static double GetCartTotal(MembershipUser user, ShoppingCartEntities context)
        {
            double totalAmount = 0;

            if (user == null)
            {
                var anonymousID = MembershipHelper.GetAnonymousID();

                var query = (from tempCarts in context.SC_TempCart
                             where tempCarts.AnonymousUserID == anonymousID
                             select tempCarts);

                if (query.Any())
                {
                    foreach (SC_TempCart tempCart in query)
                    {
                        totalAmount = totalAmount + (ProductsBL.GetPrice(tempCart.SC_Products.ProductID, context) * tempCart.Quantity);
                    }
                }
            }
            else
            {
                var query = (from userCarts in context.SC_UserCart
                             where userCarts.Username == user.UserName
                             select userCarts);

                if (query.Any())
                {
                    foreach (SC_UserCart userCart in query)
                    {
                        totalAmount = totalAmount + (ProductsBL.GetPrice(userCart.SC_Products.ProductID, context) * userCart.Quantity);
                    }
                }
            }

            return totalAmount;
        }

        public static string GetNewOrderNumber(ShoppingCartEntities context)
        {
            var orderNumber = string.Empty;
            var now = Utilities.DateTimeNow();

            orderNumber = string.Format("{0}{1}{2}-{3}{4}{5}-{6}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, Utilities.GetRandomNumber(111, 9999));

            var ordersQuery = (from orders in context.SC_Orders
                               where orders.OrderNumber == orderNumber
                               select orders);

            if (ordersQuery.Any())
            {
                GetNewOrderNumber(context);
            }

            return orderNumber;
        }

        public static bool HasOrderedProductsGoneOutOfStock(SC_Orders orderQuery, ShoppingCartEntities context)
        {
            var outOfStock = false;

            if (orderQuery.SC_OrderDetails.Any())
            {
                foreach (SC_OrderDetails orderDetail in orderQuery.SC_OrderDetails)
                {
                    if (CartProcessingBL.IsProductOutOfStock(context, orderDetail.SC_Products))
                    {
                        outOfStock = true;
                    }
                }
            }

            return outOfStock;
        }

        public static bool IfProductInTempCart(SC_TempCart tempCart)
        {
            using (var context = new ShoppingCartEntities())
            {
                return IfProductInTempCart(tempCart, context);
            }
        }

        public static bool IfProductInTempCart(SC_TempCart tempCart, ShoppingCartEntities context)
        {
            var productPresent = false;

            var tempCartQuery = (from tempCarts in context.SC_TempCart
                                 where tempCarts.ProductID == tempCart.ProductID & tempCarts.AnonymousUserID == tempCart.AnonymousUserID
                                 select tempCarts);

            if (tempCartQuery.Any())
            {
                productPresent = true;
            }

            return productPresent;
        }

        public static bool IfProductInUserCart(SC_UserCart userCart)
        {
            using (var context = new ShoppingCartEntities())
            {
                return IfProductInUserCart(userCart, context);
            }
        }

        public static bool IfProductInUserCart(SC_UserCart userCart, ShoppingCartEntities context)
        {
            var productPresent = false;

            var userCartQuery = (from userCarts in context.SC_UserCart
                                 where userCarts.ProductID == userCart.ProductID & userCarts.Username == userCart.Username
                                 select userCarts);

            if (userCartQuery.Any())
            {
                productPresent = true;
            }

            return productPresent;
        }

        public static void InitializeTempCartGridView(GridView gridview1)
        {
            var entityDataSource = new EntityDataSource();
            entityDataSource.ConnectionString = "name=ShoppingCartEntities";
            entityDataSource.DefaultContainerName = "ShoppingCartEntities";
            entityDataSource.EntitySetName = "SC_TempCart";
            entityDataSource.EnableFlattening = false;
            entityDataSource.Include = "SC_Products";

            entityDataSource.Where = "it.[AnonymousUserID]==@AnonymousUserID";
            entityDataSource.OrderBy = "it.ProductID";

            var parameter1 = new Parameter();
            parameter1.Type = TypeCode.String;
            parameter1.Name = "AnonymousUserID";

            entityDataSource.WhereParameters.Add(parameter1);

            entityDataSource.WhereParameters["AnonymousUserID"].DefaultValue = MembershipHelper.GetAnonymousID();

            gridview1.DataSource = entityDataSource;
            gridview1.DataBind();
        }

        public static void InitializeUserCartGridView(GridView gridview1, MembershipUser user)
        {
            var entityDataSource = new EntityDataSource();
            entityDataSource.ConnectionString = "name=ShoppingCartEntities";
            entityDataSource.DefaultContainerName = "ShoppingCartEntities";
            entityDataSource.EntitySetName = "SC_UserCart";
            entityDataSource.EnableFlattening = false;
            entityDataSource.Include = "SC_Products";

            entityDataSource.Where = "it.[Username]==@Username";
            entityDataSource.OrderBy = "it.ProductID";

            var parameter1 = new Parameter();
            parameter1.Type = TypeCode.String;
            parameter1.Name = "Username";

            entityDataSource.WhereParameters.Add(parameter1);

            entityDataSource.WhereParameters["Username"].DefaultValue = user.UserName;

            gridview1.DataSource = entityDataSource;
            gridview1.DataBind();
        }

        public static bool IsOrderTimedOut(MembershipUser user, ShoppingCartEntities context)
        {
            var isTimedOut = false;
            var now = Utilities.DateTimeNow();

            if (user == null)
            {
                var anonymousUserID = MembershipHelper.GetAnonymousID();

                var productLocksQuery = (from productLocks in context.SC_ProductLocks
                                         where productLocks.AnonymousUserID == anonymousUserID && productLocks.Timeout < now
                                         select productLocks);

                if (productLocksQuery.Any())
                {
                    isTimedOut = true;
                }
            }
            else
            {
                var productLocksQuery = (from productLocks in context.SC_ProductLocks
                                         where productLocks.Username == user.UserName && productLocks.Timeout < now
                                         select productLocks);

                if (productLocksQuery.Any())
                {
                    isTimedOut = true;
                }
            }

            return isTimedOut;
        }

        public static bool IsProductOutOfStock(SC_Products product)
        {
            using (var context = new ShoppingCartEntities())
            {
                return IsProductOutOfStock(context, product);
            }
        }

        public static bool IsProductOutOfStock(ShoppingCartEntities context, SC_Products productQuery)
        {
            var outOfStock = false;
            var now = Utilities.DateTimeNow();

            var productLockQuery = (from productLocks in context.SC_ProductLocks
                                    where productLocks.Timeout > now && productLocks.ProductID == productQuery.ProductID
                                    group productLocks by productLocks.ProductID
                                        into grp
                                        select new
                                        {
                                            ProductID = grp.Key,
                                            Quantity = grp.Sum(c => c.Quantity)
                                        }).FirstOrDefault();

            if (productLockQuery != null)
            {
                if (productLockQuery.Quantity >= productQuery.AvailableQuantity)
                {
                    outOfStock = true;
                }
            }
            else
            {
                if (productQuery.AvailableQuantity <= 0)
                {
                    outOfStock = true;
                }
            }
            return outOfStock;
        }

        public static bool LockTempCartItems(ShoppingCartEntities context)
        {
            var success = false;

            var anonymousUserID = MembershipHelper.GetAnonymousID();

            var tempCartsQuery = (from tempCarts in context.SC_TempCart
                                  where tempCarts.AnonymousUserID == anonymousUserID
                                  select tempCarts);

            if (tempCartsQuery.Any())
            {
                foreach (SC_TempCart tempCartQuery in tempCartsQuery)
                {
                    var prodLock = new SC_ProductLocks();
                    prodLock.ProductLockID = Guid.NewGuid();
                    prodLock.AnonymousUserID = tempCartQuery.AnonymousUserID;
                    prodLock.ProductID = tempCartQuery.ProductID;
                    prodLock.Quantity = tempCartQuery.Quantity;
                    prodLock.Timeout = Utilities.DateTimeNow().AddMinutes(AppConfig.ProductLockTimeOutInMinutes);

                    context.SC_ProductLocks.Add(prodLock);
                }

                try
                {
                    context.SaveChanges();
                    success = true;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }

            return success;
        }

        public static bool LockUserCartItems(MembershipUser user, ShoppingCartEntities context)
        {
            var success = false;

            var username = user.UserName;

            var userCartsQuery = (from userCarts in context.SC_UserCart
                                  where userCarts.Username == username
                                  select userCarts);

            if (userCartsQuery.Any())
            {
                foreach (SC_UserCart userCartQuery in userCartsQuery)
                {
                    var prodLock = new SC_ProductLocks();
                    prodLock.ProductLockID = Guid.NewGuid();
                    prodLock.Username = userCartQuery.Username;
                    prodLock.ProductID = userCartQuery.ProductID;
                    prodLock.Quantity = userCartQuery.Quantity;
                    prodLock.Timeout = Utilities.DateTimeNow().AddMinutes(AppConfig.ProductLockTimeOutInMinutes);

                    context.SC_ProductLocks.Add(prodLock);
                }

                try
                {
                    context.SaveChanges();
                    success = true;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }

            return success;
        }

        public static void MoveFromTempCartToUserCart(string username)
        {
            using (var context = new ShoppingCartEntities())
            {
                MoveFromTempCartToUserCart(username, context);
            }
        }

        public static void MoveFromTempCartToUserCart(string username, ShoppingCartEntities context)
        {
            var anonymousID = MembershipHelper.GetAnonymousID();

            var tempCartQuery = (from tempCarts in context.SC_TempCart
                                 where tempCarts.AnonymousUserID == anonymousID
                                 select tempCarts);

            if (tempCartQuery.Any())
            {
                foreach (SC_TempCart tempCart in tempCartQuery)
                {
                    var userCart = new SC_UserCart();
                    userCart.ProductID = tempCart.ProductID;
                    userCart.Quantity = tempCart.Quantity;
                    userCart.Username = username;

                    if (!IfProductInUserCart(userCart, context))
                    {
                        context.SC_UserCart.Add(userCart);
                    }

                    context.SC_TempCart.Remove(tempCart);
                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }
        }

        public static async Task MoveFromTempCartToUserCartAsync(string username)
        {
            using (var context = new ShoppingCartEntities())
            {
                await MoveFromTempCartToUserCartAsync(username, context);
            }
        }

        public static async Task MoveFromTempCartToUserCartAsync(string username, ShoppingCartEntities context)
        {
            var anonymousID = MembershipHelper.GetAnonymousID();

            var tempCartQuery = await (from tempCarts in context.SC_TempCart
                                       where tempCarts.AnonymousUserID == anonymousID
                                       select tempCarts).ToListAsync();

            if (tempCartQuery.Any())
            {
                tempCartQuery.ForEach(tempCart =>
                {
                    var userCart = new SC_UserCart();
                    userCart.ProductID = tempCart.ProductID;
                    userCart.Quantity = tempCart.Quantity;
                    userCart.Username = username;

                    if (!IfProductInUserCart(userCart, context))
                    {
                        context.SC_UserCart.Add(userCart);
                    }

                    context.SC_TempCart.Remove(tempCart);
                });

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }
        }

        public static void OnFailure(MembershipUser user, string orderNumber, ShoppingCartEntities context)
        {
            UnlockQuantity(user, context);

            HttpContext.Current.Response.Redirect(string.Format("~/UI/Pages/Open/ShoppingCart/CheckOut/OrderConfirmed.aspx?OrderNumber={0}&Success={1}", orderNumber, false));
        }

        public static void OnSuccess(MembershipUser user, string orderNumber, ShoppingCartEntities context)
        {
            ReduceQuantityFromMainStock(orderNumber, context);

            UnlockQuantity(user, context);

            UpdateOrderStatus(context, orderNumber, false, false, true, true, false, false, false, false);

            EmptyCart(user, context);

            HttpContext.Current.Response.Redirect(string.Format("~/UI/Pages/Open/ShoppingCart/CheckOut/OrderConfirmed.aspx?OrderNumber={0}&Success={1}", orderNumber, true));
        }

        public static void ProcessOrderQuantity(SC_Orders orderQuery)
        {
            if (orderQuery.SC_OrderDetails.Any())
            {
                var outOfStockProducts = new List<SC_Products>();

                foreach (SC_OrderDetails orderDetail in orderQuery.SC_OrderDetails)
                {
                    orderDetail.SC_Products.AvailableQuantity -= orderDetail.Quantity;
                    orderDetail.SC_Products.SoldOutCount += orderDetail.Quantity;

                    if (orderDetail.SC_Products.AvailableQuantity <= 0)
                    {
                        outOfStockProducts.Add(orderDetail.SC_Products);
                    }
                }
            }
        }

        public static bool ReduceQuantityFromMainStock(string orderNumber, ShoppingCartEntities context)
        {
            var success = false;

            var orderDetailsQuery = (from orderDetails in context.SC_OrderDetails
                                     where orderDetails.OrderNumber == orderNumber
                                     select orderDetails);

            if (orderDetailsQuery.Any())
            {
                var outOfStockProducts = new List<SC_Products>();

                foreach (SC_OrderDetails orderDetailQuery in orderDetailsQuery)
                {
                    var productQuery = ProductsBL.GetObjectByID(orderDetailQuery.ProductID, context);
                    productQuery.AvailableQuantity -= orderDetailQuery.Quantity;
                    productQuery.SoldOutCount += orderDetailQuery.Quantity;

                    if (productQuery.AvailableQuantity <= 0)
                    {
                        outOfStockProducts.Add(productQuery);
                    }
                }

                try
                {
                    context.SaveChanges();
                    success = true;

                    if (outOfStockProducts.Any())
                    {
                        SendOutOfStockNotification(outOfStockProducts);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }

            return success;
        }

        public static void RollBackOrderQuantity(string orderNumber, ShoppingCartEntities context)
        {
            var orderDetails = (from set in context.SC_OrderDetails
                                where set.OrderNumber == orderNumber
                                select set);

            if (orderDetails.Any())
            {
                foreach (SC_OrderDetails orderDetail in orderDetails)
                {
                    orderDetail.SC_Products.AvailableQuantity += orderDetail.Quantity;
                    orderDetail.SC_Products.SoldOutCount -= orderDetail.Quantity;
                }
            }
        }

        public static void SendEmails(string emailID, string html, string orderNumber)
        {
            var emailTemplate = MailHelper.GetEmailTemplateFromFile(AppConfig.EmailTemplate1);

            var eph = new EmailPlaceHolder();
            eph.PlaceHolder1 = DataParser.GetDateTimeFormattedString(Utilities.DateTimeNow());
            eph.PlaceHolder2 = html;

            var emailBody = MailHelper.GenerateEmailBody(eph, emailTemplate);

            MailHelper.Send(AppConfig.WebsiteMainEmail, AppConfig.WebsiteAdminEmail, string.Format("New Order Number: {0}", orderNumber), emailBody);
            MailHelper.Send(AppConfig.WebsiteMainEmail, emailID, string.Format("{0} - Order Number: {1}", AppConfig.SiteName, orderNumber), emailBody);
        }

        public static void SendOutOfStockNotification(List<SC_Products> outOfStockProducts)
        {
            var emailTemplate = MailHelper.GetEmailTemplateFromFile(AppConfig.EmailTemplate1);
            var html = "<div style='padding:20px;'><h4 style='font-family:verdana; font-size:25px; text-decoration:underline; margin-bottom:20px;'>Product(s) Gone Out Of Stock</h4>{0}</div>";
            var productAnchors = string.Empty;

            outOfStockProducts.ForEach(product =>
            {
                var productAnchor = "<a href='{1}'>{0}</a><br />";
                productAnchor = string.Format(productAnchor, product.Title, string.Format("~/UI/Pages/Admin/ShoppingCart/ProductsManage.aspx?ProductID={0}", product.ProductID).GetAbsoluteURL());
                productAnchors += productAnchor;
            });

            html = string.Format(html, productAnchors);

            var eph = new EmailPlaceHolder();
            eph.PlaceHolder1 = DataParser.GetDateTimeFormattedString(Utilities.DateTimeNow());
            eph.PlaceHolder2 = html;

            var emailBody = MailHelper.GenerateEmailBody(eph, emailTemplate);

            MailHelper.Send(AppConfig.WebsiteMainEmail, AppConfig.WebsiteAdminEmail, "Product(s) Gone Out Of Stock", emailBody);
        }

        public static bool UnlockGuestQuantity(ShoppingCartEntities context)
        {
            var success = false;

            var anonymousUserID = MembershipHelper.GetAnonymousID();

            var productLocksQuery = (from guestLocks in context.SC_ProductLocks
                                     where guestLocks.AnonymousUserID == anonymousUserID
                                     select guestLocks);

            if (productLocksQuery.Any())
            {
                foreach (SC_ProductLocks productLockQuery in productLocksQuery)
                {
                    context.SC_ProductLocks.Remove(productLockQuery);
                }

                try
                {
                    context.SaveChanges();
                    success = true;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }

            return success;
        }

        public static void UnlockQuantity(MembershipUser user, ShoppingCartEntities context)
        {
            if (user == null)
            {
                UnlockGuestQuantity(context);
            }
            else
            {
                UnlockUserQuantity(user, context);
            }
        }

        public static bool UnlockUserQuantity(MembershipUser user, ShoppingCartEntities context)
        {
            var success = false;

            var username = user.UserName;

            var productLocksQuery = (from userLocks in context.SC_ProductLocks
                                     where userLocks.Username == username
                                     select userLocks);

            if (productLocksQuery.Any())
            {
                foreach (SC_ProductLocks productLockQuery in productLocksQuery)
                {
                    context.SC_ProductLocks.Remove(productLockQuery);
                }

                try
                {
                    context.SaveChanges();
                    success = true;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }

            return success;
        }

        public static void UpdateOrderStatus(ShoppingCartEntities context, string orderNumber, bool isCancelled, bool isCompleted, bool isConfirmed, bool isPaid, bool isFailed, bool isDispatched, bool isRefund, bool isReturned)
        {
            var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

            if (orderQuery != null)
            {
                orderQuery.IsCancelled = isCancelled;
                orderQuery.IsCompleted = isCompleted;
                orderQuery.IsConfirmed = isConfirmed;
                orderQuery.IsDispatched = isDispatched;
                orderQuery.IsFailed = isFailed;
                orderQuery.IsPaid = isPaid;
                orderQuery.IsRefund = isRefund;
                orderQuery.IsReturned = isReturned;

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }
        }

        public static void UpdateTempCartFromCart(SC_TempCart tempCart, ShoppingCartEntities context)
        {
            var tempCartQuery = (from tempCarts in context.SC_TempCart
                                 where tempCarts.ProductID == tempCart.ProductID & tempCarts.AnonymousUserID == tempCart.AnonymousUserID
                                 select tempCarts).First();

            tempCartQuery.Quantity = tempCart.Quantity;

            context.SaveChanges();
        }

        public static void UpdateUserCartFromCart(SC_UserCart userCart, ShoppingCartEntities context)
        {
            var userCartQuery = (from userCarts in context.SC_UserCart
                                 where userCarts.ProductID == userCart.ProductID & userCarts.Username == userCart.Username
                                 select userCarts).First();

            userCartQuery.Quantity = userCart.Quantity;

            context.SaveChanges();
        }
    }
}