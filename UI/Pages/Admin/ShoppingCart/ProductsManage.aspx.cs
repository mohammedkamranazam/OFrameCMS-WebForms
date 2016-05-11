using OWDARO;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class ProductsManage : System.Web.UI.Page
    {
        private void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            Response.Redirect("ProductsList.aspx");
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var productID = DataParser.IntParse(Request.QueryString["ProductID"]);

                var productEntity = new SC_Products();
                productEntity.ProductID = productID;
                productEntity.AvailabilityTypeID = AvailabilityTypesDropDownList.GetNullableSelectedValue();
                productEntity.Title = TitleTextBox.Text;
                productEntity.Description = DescriptionEditor.Text;
                productEntity.Tags = TagsTextBox.Text;
                productEntity.AvailableQuantity = DataParser.DoubleParse(AvailableQuantityTextBox.Text);
                productEntity.BrandID = BrandsDropDownList.GetNullableSelectedValue();
                productEntity.CategoryID = (int)CategoriesDropDownList.GetNullableSelectedValue();
                productEntity.Details = DetailsTextBox.Text;

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
                        return;
                    }
                }

                productEntity.Hide = HideCheckBox.Checked;
                productEntity.ShowInCart = ShowInCartCheckBox.Checked;
                productEntity.ItemNumber = ItemNumberTextBox.Text;
                productEntity.Manufacturer = ManufacturerTextBox.Text;
                productEntity.Model = ModelTextBox.Text;
                productEntity.MinOQ = DataParser.DoubleParse(MinOQTextBox.Text);
                productEntity.MaxOQ = DataParser.DoubleParse(MaxOQTextBox.Text);
                productEntity.PreOderFlag = PreOrderFlagCheckBox.Checked;
                productEntity.PreOrderDescription = PreOrderDescriptionTextBox.Text;
                productEntity.Price = DataParser.DoubleParse(PriceTextBox.Text);
                productEntity.PriceDescription = PriceDescriptionTextBox.Text;
                productEntity.SectionID = (int)SectionsDropDownList.GetNullableSelectedValue();
                productEntity.SpecialOffer = SpecialOfferTextBox.Text;
                productEntity.SubCategoryID = (int)SubCategoriesDropDownList.GetNullableSelectedValue();
                productEntity.SubTitle = SubTitleTextBox.Text;
                productEntity.UnitID = UnitsDropDownList.GetNullableSelectedValue();
                productEntity.ColorID = ColorsDropDownList.GetNullableSelectedValue();
                productEntity.SizeID = SizesDropDownList.GetNullableSelectedValue();
                productEntity.ProductModelID = ProductModelsDropDownList.GetNullableSelectedValue();
                productEntity.ProductTypeID = ProductTypesDropDownList.GetNullableSelectedValue();

                try
                {
                    ProductsBL.Save(productEntity);

                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        private void HasDiscountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DiscountFieldsDiv.Visible = HasDiscountCheckBox.Checked;
        }

        private void ImageUploadFormToolBar_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var productID = DataParser.IntParse(Request.QueryString["ProductID"]);

                using (var context = new ShoppingCartEntities())
                {
                    if (ProductsBL.Exists(productID, context))
                    {
                        var productImagesEntity = new SC_ProductImages();

                        productImagesEntity.ImageURL = ProductImagesBL.GetUploadedImagePath(FileUpload1);
                        productImagesEntity.ImageThumbURL = ProductImagesBL.GetUploadedImageThumbnailPath(FileUpload1);
                        productImagesEntity.AlternateText = AlternateTextTextBox.Text;
                        productImagesEntity.ProductID = productID;

                        if (FileUpload1.Success)
                        {
                            try
                            {
                                ProductImagesBL.Add(productImagesEntity, context);

                                GridView1.DataBind();
                                CoverImage.ImageUrl = ProductImagesBL.GetProductFirstImageThumb(productID, context);

                                StatusMessage2.MessageType = StatusMessageType.Success;
                                StatusMessage2.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError(ex);
                                StatusMessage2.MessageType = StatusMessageType.Error;
                                StatusMessage2.Message = ExceptionHelper.GetExceptionMessage(ex);
                            }
                        }
                        else
                        {
                            StatusMessage2.MessageType = StatusMessageType.Error;
                            StatusMessage2.Message = FileUpload1.Message;
                        }
                    }
                    else
                    {
                        StatusMessage2.MessageType = StatusMessageType.Info;
                        StatusMessage2.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                    }
                }
            }
        }

        private void SectionsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                if (string.IsNullOrWhiteSpace(Request.QueryString["ProductID"]))
                {
                    Response.Redirect("ProductsList.aspx");
                }

                using (var context = new ShoppingCartEntities())
                {
                    var productID = DataParser.IntParse(Request.QueryString["ProductID"]);

                    var productQuery = ProductsBL.GetObjectByID(productID, context);

                    if (productQuery == null)
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                        return;
                    }

                    if (productQuery.SC_Sections.Hide == true || productQuery.SC_Categories.Hide == true || productQuery.SC_SubCategories.Hide == true)
                    {
                        Response.Redirect("ProductsList.aspx");
                    }

                    var sectionsQuery = (from sections in context.SC_Sections
                                         where sections.Hide == false
                                         select sections);
                    SectionsDropDownList.DataTextField = "Title";
                    SectionsDropDownList.DataValueField = "SectionID";
                    SectionsDropDownList.DataSource = sectionsQuery.ToList();
                    SectionsDropDownList.AddSelect();
                    SectionIDHiddenField.Value = productQuery.SectionID.ToString();
                    SectionsDropDownList.SelectedValue = productQuery.SectionID.ToString();
                    var sectionID = (int)SectionsDropDownList.GetNullableSelectedValue();

                    var categoriesQuery = (from categories in context.SC_Categories
                                           where categories.Hide == false && categories.SectionID == sectionID
                                           select categories);
                    CategoriesDropDownList.DataTextField = "Title";
                    CategoriesDropDownList.DataValueField = "CategoryID";
                    CategoriesDropDownList.DataSource = categoriesQuery.ToList();
                    CategoriesDropDownList.AddSelect();
                    CategoryIDHiddenField.Value = productQuery.CategoryID.ToString();
                    CategoriesDropDownList.SelectedValue = productQuery.CategoryID.ToString();
                    var categoryID = (int)CategoriesDropDownList.GetNullableSelectedValue();

                    var subCategoriesQuery = (from subCategories in context.SC_SubCategories
                                              where subCategories.Hide == false && subCategories.CategoryID == categoryID
                                              select subCategories);
                    SubCategoriesDropDownList.DataTextField = "Title";
                    SubCategoriesDropDownList.DataValueField = "SubCategoryID";
                    SubCategoriesDropDownList.DataSource = subCategoriesQuery.ToList();
                    SubCategoriesDropDownList.AddSelect();
                    SubCategoryIDHiddenField.Value = productQuery.SubCategoryID.ToString();
                    SubCategoriesDropDownList.SelectedValue = productQuery.SubCategoryID.ToString();
                    var subCategoryID = (int)SubCategoriesDropDownList.GetNullableSelectedValue();

                    var brandsQuery = (from brands in context.SC_Brands
                                       where brands.Hide == false
                                       select brands);
                    BrandsDropDownList.DataTextField = "Title";
                    BrandsDropDownList.DataValueField = "BrandID";
                    BrandsDropDownList.DataSource = brandsQuery.ToList();
                    BrandsDropDownList.AddSelect();
                    BrandsDropDownList.SelectedValue = productQuery.BrandID.ToString();

                    var availabilityTypesQuery = (from availabilityTypes in context.SC_AvailabilityTypes
                                                  where availabilityTypes.Hide == false
                                                  select availabilityTypes);
                    AvailabilityTypesDropDownList.DataTextField = "Title";
                    AvailabilityTypesDropDownList.DataValueField = "AvailabilityTypeID";
                    AvailabilityTypesDropDownList.DataSource = availabilityTypesQuery.ToList();
                    AvailabilityTypesDropDownList.AddSelect();
                    AvailabilityTypesDropDownList.SelectedValue = productQuery.AvailabilityTypeID.ToString();

                    CoverImage.ImageUrl = ProductImagesBL.GetProductFirstImageThumb(productID, context);
                    TitleTextBox.Text = productQuery.Title;
                    SubTitleTextBox.Text = productQuery.SubTitle;
                    DescriptionEditor.Text = productQuery.Description;
                    PriceTextBox.Text = productQuery.Price.ToString();
                    AvailableQuantityTextBox.Text = productQuery.AvailableQuantity.ToString();
                    DiscountFieldsDiv.Visible = HasDiscountCheckBox.Checked = productQuery.HasDiscount;
                    DiscountPercentageTextBox.Text = productQuery.DiscountPercentage.ToString();
                    DiscountAmountTextBox.Text = productQuery.DiscountAmount.ToString();
                    ModelTextBox.Text = productQuery.Model;
                    ManufacturerTextBox.Text = productQuery.Manufacturer;
                    ItemNumberHiddenField.Value = ItemNumberTextBox.Text = productQuery.ItemNumber;
                    ShowInCartCheckBox.Checked = productQuery.ShowInCart;
                    MinOQTextBox.Text = productQuery.MinOQ.ToString();
                    MaxOQTextBox.Text = productQuery.MaxOQ.ToString();
                    TagsTextBox.Text = productQuery.Tags;
                    PriceDescriptionTextBox.Text = productQuery.PriceDescription;
                    SpecialOfferTextBox.Text = productQuery.SpecialOffer;
                    DetailsTextBox.Text = productQuery.Details;
                    PreOrderFlagCheckBox.Checked = productQuery.PreOderFlag;
                    PreOrderDescriptionTextBox.Text = productQuery.PreOrderDescription;
                    HideCheckBox.Checked = productQuery.Hide;

                    var colorsQuery = (from colors in context.SC_Colors
                                       select colors);
                    ColorsDropDownList.DataTextField = "Title";
                    ColorsDropDownList.DataValueField = "ColorID";
                    ColorsDropDownList.DataSource = colorsQuery.ToList();
                    ColorsDropDownList.AddSelect();
                    ColorsDropDownList.SelectedValue = productQuery.ColorID.ToString();

                    var sizesQuery = (from sizes in context.SC_Sizes
                                      where sizes.SectionID == sectionID && sizes.CategoryID == categoryID && sizes.SubCategoryID == subCategoryID
                                      select sizes);
                    SizesDropDownList.DataTextField = "Title";
                    SizesDropDownList.DataValueField = "SizeID";
                    SizesDropDownList.DataSource = sizesQuery.ToList();
                    SizesDropDownList.AddSelect();
                    SizesDropDownList.SelectedValue = productQuery.SizeID.ToString();

                    var unitsQuery = (from units in context.SC_Units
                                      where units.SectionID == sectionID && units.CategoryID == categoryID && units.SubCategoryID == subCategoryID
                                      select units);
                    UnitsDropDownList.DataTextField = "Title";
                    UnitsDropDownList.DataValueField = "UnitID";
                    UnitsDropDownList.DataSource = unitsQuery.ToList();
                    UnitsDropDownList.AddSelect();
                    UnitsDropDownList.SelectedValue = productQuery.UnitID.ToString();

                    var productTypesQuery = (from productTypes in context.SC_ProductTypes
                                             where productTypes.SectionID == sectionID && productTypes.CategoryID == categoryID && productTypes.SubCategoryID == subCategoryID
                                             select productTypes);
                    ProductTypesDropDownList.DataTextField = "Title";
                    ProductTypesDropDownList.DataValueField = "ProductTypeID";
                    ProductTypesDropDownList.DataSource = productTypesQuery.ToList();
                    ProductTypesDropDownList.AddSelect();
                    ProductTypesDropDownList.SelectedValue = productQuery.ProductTypeID.ToString();

                    var productModelsQuery = (from productModels in context.SC_ProductModels
                                              where productModels.SectionID == sectionID && productModels.CategoryID == categoryID && productModels.SubCategoryID == subCategoryID
                                              select productModels);
                    ProductModelsDropDownList.DataTextField = "Title";
                    ProductModelsDropDownList.DataValueField = "ProductModelID";
                    ProductModelsDropDownList.DataSource = productModelsQuery.ToList();
                    ProductModelsDropDownList.AddSelect();
                    ProductModelsDropDownList.SelectedValue = productQuery.ProductModelID.ToString();
                }
            }

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

            MinOQTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;
            MinOQTextBox.ValidationExpression = Validator.FloatValidationExpression;

            MaxOQTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;
            MaxOQTextBox.ValidationExpression = Validator.FloatValidationExpression;

            ImageUploadFormToolBar.Save += ImageUploadFormToolBar_Save;
            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            SectionsDropDownList.SelectedIndexChanged += SectionsDropDownList_SelectedIndexChanged;
            CategoriesDropDownList.SelectedIndexChanged += CategoriesDropDownList_SelectedIndexChanged;
            SubCategoriesDropDownList.SelectedIndexChanged += SubCategoriesDropDownList_SelectedIndexChanged;
            HasDiscountCheckBox.CheckedChanged += HasDiscountCheckBox_CheckedChanged;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void ProductImagesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var productImageID = e.CommandArgument.ToString();

            var imageID = DataParser.LongParse(productImageID);

            using (var context = new ShoppingCartEntities())
            {
                var productImageQuery = ProductImagesBL.GetObjectByID(imageID, context);

                var imageURL = productImageQuery.ImageURL;
                var imageThumbURL = productImageQuery.ImageThumbURL;
                var productID = productImageQuery.ProductID;

                try
                {
                    ProductImagesBL.Delete(productImageQuery, context);

                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;

                    imageURL.DeleteFile();
                    imageThumbURL.DeleteFile();

                    CoverImage.ImageUrl = ProductImagesBL.GetProductFirstImageThumb(productID, context);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }
    }
}