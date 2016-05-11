using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class ProductsAdd : System.Web.UI.Page
    {
        private void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubCategoriesDropDownList.Visible = true;

            var categoryID = CategoriesDropDownList.GetSelectedValue();

            using (var context = new ShoppingCartEntities())
            {
                var subCategoriesQuery = (from subCategories in context.SC_SubCategories
                                          where subCategories.CategoryID == categoryID
                                          select subCategories);
                SubCategoriesDropDownList.DataTextField = "Title";
                SubCategoriesDropDownList.DataValueField = "SubCategoryID";
                SubCategoriesDropDownList.DataSource = subCategoriesQuery.ToList();
                SubCategoriesDropDownList.AddSelect();
            }
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var id = Save();

                if (id != null)
                {
                    Response.Redirect("ProductsManage.aspx?ProductID=" + id);
                }
            }
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Save();
            }
        }

        private void HasDiscountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DiscountFieldsDiv.Visible = HasDiscountCheckBox.Checked;
        }

        private int? Save()
        {
            var id = (int?)null;

            using (var context = new ShoppingCartEntities())
            {
                var productEntity = new SC_Products();
                productEntity.Title = TitleTextBox.Text;
                productEntity.CostPrice = 0;
                productEntity.IsDiscountRangeEnabled = false;
                productEntity.IsDownloadable = false;
                productEntity.IsPriceOnRequest = false;
                productEntity.MaxOQ = 999999;
                productEntity.PreOrderReleaseDate = null;
                productEntity.RemovePreOrderOnDate = false;
                productEntity.BrandID = BrandsDropDownList.GetNullableSelectedValue();
                productEntity.ItemNumber = ItemNumberTextBox.Text;
                productEntity.Description = DescriptionEditor.Text;
                productEntity.Price = DataParser.FloatParse(PriceTextBox.Text);
                productEntity.AvailableQuantity = DataParser.FloatParse(AvailableQuantityTextBox.Text);

                productEntity.HasDiscount = HasDiscountCheckBox.Checked;
                productEntity.DiscountAmount = null;
                productEntity.DiscountPercentage = null;
                if (HasDiscountCheckBox.Checked)
                {
                    if ((DataParser.FloatParse(DiscountPercentageTextBox.Text) > 0 && DataParser.FloatParse(DiscountAmountTextBox.Text) == 0) || (DataParser.FloatParse(DiscountPercentageTextBox.Text) == 0 && DataParser.FloatParse(DiscountAmountTextBox.Text) > 0))
                    {
                        productEntity.DiscountAmount = DataParser.FloatParse(DiscountAmountTextBox.Text);
                        productEntity.DiscountPercentage = DataParser.FloatParse(DiscountPercentageTextBox.Text);
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = "Both Discount Amount and Discount Percentage cannot be assigned. Set either one to zero (0).";
                        return null;
                    }
                }

                productEntity.UnitID = UnitsDropDownList.GetNullableSelectedValue();
                productEntity.ColorID = ColorsDropDownList.GetNullableSelectedValue();
                productEntity.SizeID = SizesDropDownList.GetNullableSelectedValue();
                productEntity.ProductModelID = ProductModelsDropDownList.GetNullableSelectedValue();
                productEntity.ProductTypeID = ProductTypesDropDownList.GetNullableSelectedValue();
                productEntity.SectionID = (int)SectionsDropDownList.GetNullableSelectedValue();
                productEntity.CategoryID = (int)CategoriesDropDownList.GetNullableSelectedValue();
                productEntity.SubCategoryID = (int)SubCategoriesDropDownList.GetNullableSelectedValue();
                productEntity.AvailabilityTypeID = AvailabilityTypesDropDownList.GetNullableSelectedValue();
                productEntity.Hide = false;
                productEntity.PreOderFlag = false;
                productEntity.ShowInCart = ShowInCartCheckBox.Checked;
                productEntity.MinOQ = 1;
                productEntity.SoldOutCount = 0;
                var imageURL = ProductImagesBL.GetUploadedImagePath(FileUpload1);
                var imageThumbURL = ProductImagesBL.GetUploadedImageThumbnailPath(FileUpload1);
                productEntity.UploadedOn = Utilities.DateTimeNow();

                if (FileUpload1.Success)
                {
                    try
                    {
                        ProductsBL.Add(productEntity, context);
                        id = productEntity.ProductID;

                        var prodImageEntity = new SC_ProductImages();
                        prodImageEntity.AlternateText = TitleTextBox.Text;
                        prodImageEntity.ImageThumbURL = imageThumbURL;
                        prodImageEntity.ImageURL = imageURL;
                        prodImageEntity.ProductID = (int)id;
                        ProductImagesBL.Add(prodImageEntity, context);

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    }
                }
                else
                {
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = FileUpload1.Message;
                }
            }

            return id;
        }

        private void SectionsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoriesDropDownList.Visible = true;

            var sectionID = SectionsDropDownList.GetSelectedValue();

            using (var context = new ShoppingCartEntities())
            {
                var query = (from categories in context.SC_Categories
                             where categories.SectionID == sectionID
                             select categories);
                CategoriesDropDownList.DataTextField = "Title";
                CategoriesDropDownList.DataValueField = "CategoryID";
                CategoriesDropDownList.DataSource = query.ToList();
                CategoriesDropDownList.AddSelect();
            }
        }

        private void SubCategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitsDropDownList.Visible = true;
            ColorsDropDownList.Visible = true;
            SizesDropDownList.Visible = true;
            ProductTypesDropDownList.Visible = true;
            ProductModelsDropDownList.Visible = true;

            using (var context = new ShoppingCartEntities())
            {
                var sectionID = SectionsDropDownList.GetSelectedValue();
                var categoryID = CategoriesDropDownList.GetSelectedValue();
                var subCategoryID = SubCategoriesDropDownList.GetSelectedValue();

                var colorsQuery = (from colors in context.SC_Colors
                                   select colors);
                ColorsDropDownList.DataTextField = "Title";
                ColorsDropDownList.DataValueField = "ColorID";
                ColorsDropDownList.DataSource = colorsQuery.ToList();
                ColorsDropDownList.AddSelect();

                var sizesQuery = (from sizes in context.SC_Sizes
                                  where sizes.SectionID == sectionID && sizes.CategoryID == categoryID && sizes.SubCategoryID == subCategoryID
                                  select sizes);
                SizesDropDownList.DataTextField = "Title";
                SizesDropDownList.DataValueField = "SizeID";
                SizesDropDownList.DataSource = sizesQuery.ToList();
                SizesDropDownList.AddSelect();

                var unitsQuery = (from units in context.SC_Units
                                  where units.SectionID == sectionID && units.CategoryID == categoryID && units.SubCategoryID == subCategoryID
                                  select units);
                UnitsDropDownList.DataTextField = "Title";
                UnitsDropDownList.DataValueField = "UnitID";
                UnitsDropDownList.DataSource = unitsQuery.ToList();
                UnitsDropDownList.AddSelect();

                var productTypesQuery = (from productTypes in context.SC_ProductTypes
                                         where productTypes.SectionID == sectionID && productTypes.CategoryID == categoryID && productTypes.SubCategoryID == subCategoryID
                                         select productTypes);
                ProductTypesDropDownList.DataTextField = "Title";
                ProductTypesDropDownList.DataValueField = "ProductTypeID";
                ProductTypesDropDownList.DataSource = productTypesQuery.ToList();
                ProductTypesDropDownList.AddSelect();

                var productModelsQuery = (from productModels in context.SC_ProductModels
                                          where productModels.SectionID == sectionID && productModels.CategoryID == categoryID && productModels.SubCategoryID == subCategoryID
                                          select productModels);
                ProductModelsDropDownList.DataTextField = "Title";
                ProductModelsDropDownList.DataValueField = "ProductModelID";
                ProductModelsDropDownList.DataSource = productModelsQuery.ToList();
                ProductModelsDropDownList.AddSelect();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileUpload1.ValidationErrorMessage = Validator.ImageValidationErrorMessage;
                FileUpload1.ValidationExpression = Validator.ImageValidationExpression;

                PriceTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;
                PriceTextBox.ValidationExpression = Validator.FloatValidationExpression;

                AvailableQuantityTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;
                AvailableQuantityTextBox.ValidationExpression = Validator.FloatValidationExpression;

                DiscountPercentageTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;
                DiscountPercentageTextBox.ValidationExpression = Validator.FloatValidationExpression;

                DiscountAmountTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;
                DiscountAmountTextBox.ValidationExpression = Validator.FloatValidationExpression;

                using (var context = new ShoppingCartEntities())
                {
                    var sectionsQuery = (from sections in context.SC_Sections
                                         select sections);
                    SectionsDropDownList.DataTextField = "Title";
                    SectionsDropDownList.DataValueField = "SectionID";
                    SectionsDropDownList.DataSource = sectionsQuery.ToList();
                    SectionsDropDownList.AddSelect();

                    var availabilityTypesQuery = (from availabilityTypes in context.SC_AvailabilityTypes
                                                  select availabilityTypes);
                    AvailabilityTypesDropDownList.DataTextField = "Title";
                    AvailabilityTypesDropDownList.DataValueField = "AvailabilityTypeID";
                    AvailabilityTypesDropDownList.DataSource = availabilityTypesQuery.ToList();
                    AvailabilityTypesDropDownList.AddSelect();

                    var brandsQuery = (from brands in context.SC_Brands
                                       where brands.Hide == false
                                       select brands);
                    BrandsDropDownList.DataTextField = "Title";
                    BrandsDropDownList.DataValueField = "BrandID";
                    BrandsDropDownList.DataSource = brandsQuery.ToList();
                    BrandsDropDownList.AddSelect();
                }
            }

            SectionsDropDownList.SelectedIndexChanged += SectionsDropDownList_SelectedIndexChanged;
            CategoriesDropDownList.SelectedIndexChanged += CategoriesDropDownList_SelectedIndexChanged;
            SubCategoriesDropDownList.SelectedIndexChanged += SubCategoriesDropDownList_SelectedIndexChanged;
            HasDiscountCheckBox.CheckedChanged += HasDiscountCheckBox_CheckedChanged;

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.CustomClick += FormToolbar1_CustomClick;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}