using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.UI.UserControls.Controls;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Utility
{
    public partial class ImageUploaderComponent : UserControl
    {
        private const string Anchor = "middlecenter";
        private const string Scale = "both";
        private const string Mode = "max";

        public event ImageUploadedEventHandler ImageUploaded;

        public FileUploadAdv FileUpload
        {
            get
            {
                return ImageURLFileUpload;
            }
        }

        public int? ImageID
        {
            get
            {
                return ImageIDHiddenField.Value.NullableIntParse();
            }

            set
            {
                ImageIDHiddenField.Value = value.ToString();

                using (var context = new OWDAROEntities())
                {
                    InitializeImage(context);
                }
            }
        }

        public string StoragePath
        {
            get
            {
                return StoragePathHiddenField.Value;
            }

            set
            {
                StoragePathHiddenField.Value = value;
            }
        }

        private bool Add(OWDAROEntities context, OW_Images entity)
        {
            try
            {
                context.OW_Images.Add(entity);
                context.SaveChanges();

                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;

                ImageID = entity.ImageID;

                OnImageUploaded(entity.ImageID);

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);

                if (ImageURLFileUpload.Success)
                {
                    entity.ImageURL.DeleteFile();
                }

                return false;
            }
        }

        private void AddNewImage(OWDAROEntities context)
        {
            var entity = new OW_Images();

            entity.ShowWebImage = ShowWebImageCheckBox.Checked;
            entity.Title = TitleTextBox.Text;

            entity.Quality = (int)ImageQualitySlider.Value;
            entity.ThumbQuality = (int)ImageQualitySlider.Value;

            entity.Anchor = Anchor;
            entity.Scale = Scale;
            entity.Mode = Mode;

            entity.ThumbAnchor = Anchor;
            entity.ThumbScale = Scale;
            entity.ThumbMode = Mode;

            entity.FocusPointX = 0;
            entity.FocusPointY = 0;

            if (ShowWebImageCheckBox.Checked)
            {
                #region WEBIMAGE

                entity.WebImageURL = WebImageURLTextBox.Text;
                entity.WebImageThumbURL = (string.IsNullOrWhiteSpace(WebImageThumbURLTextBox.Text)) ? WebImageURLTextBox.Text : WebImageThumbURLTextBox.Text;

                Size sz = ImageHelper.GetDimensionFromURL(entity.WebImageURL);

                entity.OriginalHeight = sz.Height;
                entity.OriginalWidth = sz.Width;

                entity.CropHeight = sz.Height;
                entity.CropWidth = sz.Width;
                entity.MaxHeight = sz.Height;
                entity.MaxWidth = sz.Width;
                entity.Height = sz.Height;
                entity.Width = sz.Width;
                entity.XUnit = sz.Width;
                entity.YUnit = sz.Height;
                entity.X1 = 0;
                entity.X2 = sz.Width;
                entity.Y1 = 0;
                entity.Y2 = sz.Height;

                entity.ThumbCropHeight = 0;
                entity.ThumbCropWidth = 0;
                entity.ThumbMaxHeight = 200;
                entity.ThumbMaxWidth = 200;
                entity.ThumbHeight = 200;
                entity.ThumbWidth = 200;
                entity.ThumbXUnit = 0;
                entity.ThumbYUnit = 0;
                entity.ThumbX1 = 0;
                entity.ThumbX2 = 0;
                entity.ThumbY1 = 0;
                entity.ThumbY2 = 0;

                if (Add(context, entity))
                {
                    InitializeImage(entity);
                }

                #endregion WEBIMAGE
            }
            else
            {
                #region UPLOADED IMAGE

                if (ImageURLFileUpload.HasFile)
                {
                    entity.ImageURL = GetUploadedImagePath(ImageURLFileUpload);

                    if (ImageURLFileUpload.Success)
                    {
                        entity.OriginalHeight = UploadedImageHeightHiddenField.Value.IntParse();
                        entity.OriginalWidth = UploadedImageWidthHiddenField.Value.IntParse();

                        entity.CropHeight = UploadedImageHeightHiddenField.Value.IntParse();
                        entity.CropWidth = UploadedImageWidthHiddenField.Value.IntParse();
                        entity.MaxHeight = UploadedImageHeightHiddenField.Value.IntParse();
                        entity.MaxWidth = UploadedImageWidthHiddenField.Value.IntParse();
                        entity.Height = UploadedImageHeightHiddenField.Value.IntParse();
                        entity.Width = UploadedImageWidthHiddenField.Value.IntParse();
                        entity.XUnit = UploadedImageWidthHiddenField.Value.IntParse();
                        entity.YUnit = UploadedImageHeightHiddenField.Value.IntParse();
                        entity.X1 = 0;
                        entity.X2 = UploadedImageWidthHiddenField.Value.IntParse();
                        entity.Y1 = 0;
                        entity.Y2 = UploadedImageHeightHiddenField.Value.IntParse();

                        entity.ThumbCropHeight = 0;
                        entity.ThumbCropWidth = 0;
                        entity.ThumbMaxHeight = 200;
                        entity.ThumbMaxWidth = 200;
                        entity.ThumbHeight = 200;
                        entity.ThumbWidth = 200;
                        entity.ThumbXUnit = 0;
                        entity.ThumbYUnit = 0;
                        entity.ThumbX1 = 0;
                        entity.ThumbX2 = 0;
                        entity.ThumbY1 = 0;
                        entity.ThumbY2 = 0;

                        if (Add(context, entity))
                        {
                            InitializeImage(entity);
                        }
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = ImageURLFileUpload.Message;
                    }
                }
                else
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = ImageURLFileUpload.Message;
                }

                #endregion UPLOADED IMAGE
            }
        }

        private void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new OWDAROEntities())
                {
                    AddNewImage(context);
                }
            }
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new OWDAROEntities())
                {
                    var imageQuery = new OW_Images();

                    if (ImageID != null)
                    {
                        imageQuery = (from set in context.OW_Images
                                      where set.ImageID == ImageID
                                      select set).FirstOrDefault();

                        if (imageQuery != null)
                        {
                            SaveImage(context, imageQuery);
                        }
                        else
                        {
                            AddNewImage(context);
                        }
                    }
                    else
                    {
                        AddNewImage(context);
                    }
                }
            }
        }

        private string GetUploadedImagePath(FileUploadAdv fileUpload)
        {
            if (!fileUpload.HasFile)
            {
                return string.Empty;
            }

            var fileName = Guid.NewGuid().ToString();
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;
            var relativeStoragePath = StoragePath;
            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.FilePath = relativeStoragePath;
            fileUpload.FileName = fileName;

            if ((int)ImageQualitySlider.Value != 100)
            {
                fileUpload.CompressAndUpload((int)ImageQualitySlider.Value, absoluteFullFilePath);
            }
            else
            {
                fileUpload.Upload();
            }

            SetNewHeightAndWidth(fileUpload.FileContent);

            return relativeFullFilePath;
        }

        private void InitializeCropEditor(OW_Images entity)
        {
            string imageURL = Utilities.GetImageURL(entity);

            if (!entity.ShowWebImage)
            {
                imageURL = Utilities.GetAbsoluteURL(Utilities.GetImageURL(entity));
            }

            ImageFocusPointHelperLinkLabel.Text = string.Format("<a href='{1}?ImageURL={0}' target='_blank' class='btn'>Click Me</a>",
                imageURL, ResolveClientUrl("~/UI/Pages/Helpers/FocusPointHelper/index.html"));

            var bigImageCropXUnit = 500;

            if (entity.OriginalWidth > 500)
            {
                bigImageCropXUnit = 500;
            }
            else
            {
                bigImageCropXUnit = entity.OriginalWidth;
            }

            var styleAttrValue = "width:{0}px; max-width:{1}px;";

            styleAttrValue = string.Format(styleAttrValue, bigImageCropXUnit, bigImageCropXUnit);

            BigImageDisplayPanel.Attributes.Add("style", styleAttrValue);
            UploadedBigImageEdit.ImageUrl = Utilities.GetImageURL(entity, bigImageCropXUnit, null);
            EditorLaunchToolbar.Visible = true;

            var aspectRatio = (Convert.ToDecimal(entity.CropWidth) / Convert.ToDecimal(entity.CropHeight));

            var bigImageCropYUnit = (int)(bigImageCropXUnit / aspectRatio);

            BigImageCropXUnit.Value = bigImageCropXUnit.ToString();
            BigImageCropYUnit.Value = bigImageCropYUnit.ToString();

            BigImageOriginalAspectRatioHiddenField.Value = BigImageCropAspectRatioHiddenField.Value = aspectRatio.ToString("#.#");

            BigImageCropHeight.Value = entity.OriginalHeight.ToString();
            BigImageCropWidth.Value = entity.OriginalWidth.ToString();

            BigImageAspectRatioDisableCheckBox.Checked = false;
        }

        private void InitializeImage(OWDAROEntities context)
        {
            UploadedBigImageEdit.ImageUrl = AppConfig.NoImage;

            FancyBoxLiteral.Text = Utilities.GetFancyBoxHTML(null, string.Empty, false, string.Empty);

            var imageQuery = new OW_Images();

            if (ImageID != null)
            {
                imageQuery = (from set in context.OW_Images
                              where set.ImageID == ImageID
                              select set).FirstOrDefault();

                if (imageQuery != null)
                {
                    InitializeImage(imageQuery);
                }
            }
        }

        private void InitializeImage(OW_Images entity)
        {
            ShowWebImageCheckBox.Checked = entity.ShowWebImage;
            TitleTextBox.Text = entity.Title;

            FocusPointXTextBox.Text = entity.FocusPointX.ToString();
            FocusPointYTextBox.Text = entity.FocusPointY.ToString();
            EditHeightTextBox.Text = entity.Height.ToString();
            EditWidthTextBox.Text = entity.Width.ToString();
            EditMaxHeightTextBox.Text = entity.MaxHeight.ToString();
            EditMaxWidthTextBox.Text = entity.MaxWidth.ToString();
            EditQualitySlider.Value = entity.Quality.ToString().DoubleParse();

            EditThumbHeightTextBox.Text = entity.ThumbHeight.ToString();
            EditThumbWidthTextBox.Text = entity.ThumbWidth.ToString();
            EditThumbMaxHeightTextBox.Text = entity.ThumbMaxHeight.ToString();
            EditThumbMaxWidthTextBox.Text = entity.ThumbMaxWidth.ToString();
            EditThumbQualitySlider.Value = entity.ThumbQuality.ToString().DoubleParse();

            FancyBoxLiteral.Text = Utilities.GetFancyBoxHTML(entity, string.Empty, false, string.Empty, string.Empty);

            ToggleWebToLocalFields(entity.ShowWebImage);

            //   EditorLaunchToolbar.Visible = !entity.ShowWebImage;

            if (entity.ShowWebImage)
            {
                WebImageURLTextBox.Text = entity.WebImageURL;
                WebImageThumbURLTextBox.Text = entity.WebImageThumbURL;
            }

            InitializeCropEditor(entity);
        }

        private void OnImageUploaded(int imageID)
        {
            if (ImageUploaded != null)
            {
                ImageUploaded(this, new ImageUploadedEventArgs(imageID));
            }
        }

        private bool Save(OW_Images imageQuery, OWDAROEntities context)
        {
            try
            {
                context.SaveChanges();

                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                OnImageUploaded(imageQuery.ImageID);

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);

                return false;
            }
        }

        private void SaveImage(OWDAROEntities context, OW_Images entity)
        {
            entity.Title = TitleTextBox.Text;
            entity.ShowWebImage = ShowWebImageCheckBox.Checked;

            entity.FocusPointX = 0;
            entity.FocusPointY = 0;

            entity.Quality = (int)ImageQualitySlider.Value;
            entity.ThumbQuality = (int)ImageQualitySlider.Value;

            entity.Anchor = Anchor;
            entity.Scale = Scale;
            entity.Mode = Mode;

            entity.ThumbAnchor = Anchor;
            entity.ThumbScale = Scale;
            entity.ThumbMode = Mode;

            if (ShowWebImageCheckBox.Checked)
            {
                #region WEB IMAGE

                entity.WebImageURL = WebImageURLTextBox.Text;
                entity.WebImageThumbURL = (string.IsNullOrWhiteSpace(WebImageThumbURLTextBox.Text)) ? WebImageURLTextBox.Text : WebImageThumbURLTextBox.Text;

                Size sz = ImageHelper.GetDimensionFromURL(entity.WebImageURL);

                entity.OriginalHeight = sz.Height;
                entity.OriginalWidth = sz.Width;

                entity.CropHeight = sz.Height;
                entity.CropWidth = sz.Width;
                entity.MaxHeight = sz.Height;
                entity.MaxWidth = sz.Width;
                entity.Height = sz.Height;
                entity.Width = sz.Width;
                entity.XUnit = sz.Width;
                entity.YUnit = sz.Height;
                entity.X1 = 0;
                entity.X2 = sz.Width;
                entity.Y1 = 0;
                entity.Y2 = sz.Height;

                entity.ThumbCropHeight = 0;
                entity.ThumbCropWidth = 0;
                entity.ThumbMaxHeight = 200;
                entity.ThumbMaxWidth = 200;
                entity.ThumbHeight = 200;
                entity.ThumbWidth = 200;
                entity.ThumbXUnit = 0;
                entity.ThumbYUnit = 0;
                entity.ThumbX1 = 0;
                entity.ThumbX2 = 0;
                entity.ThumbY1 = 0;
                entity.ThumbY2 = 0;

                if (Save(entity, context))
                {
                    InitializeImage(entity);
                }

                #endregion WEB IMAGE
            }
            else
            {
                #region UPLOADED IMAGE

                if (ImageURLFileUpload.HasFile)
                {
                    var newImageURL = GetUploadedImagePath(ImageURLFileUpload);
                    var oldImageURL = entity.ImageURL;

                    if (ImageURLFileUpload.Success)
                    {
                        entity.ImageURL = newImageURL;

                        entity.OriginalHeight = UploadedImageHeightHiddenField.Value.IntParse();
                        entity.OriginalWidth = UploadedImageWidthHiddenField.Value.IntParse();

                        entity.CropHeight = UploadedImageHeightHiddenField.Value.IntParse();
                        entity.CropWidth = UploadedImageWidthHiddenField.Value.IntParse();
                        entity.MaxHeight = entity.OriginalHeight;
                        entity.MaxWidth = entity.OriginalWidth;
                        entity.Height = entity.OriginalHeight;
                        entity.Width = entity.OriginalWidth;
                        entity.XUnit = entity.OriginalWidth;
                        entity.YUnit = entity.OriginalHeight;
                        entity.X1 = 0;
                        entity.X2 = entity.OriginalWidth;
                        entity.Y1 = 0;
                        entity.Y2 = entity.OriginalHeight;

                        entity.ThumbCropHeight = 0;
                        entity.ThumbCropWidth = 0;
                        entity.ThumbMaxHeight = 200;
                        entity.ThumbMaxWidth = 200;
                        entity.ThumbHeight = 200;
                        entity.ThumbWidth = 200;
                        entity.ThumbXUnit = 0;
                        entity.ThumbYUnit = 0;
                        entity.ThumbX1 = 0;
                        entity.ThumbX2 = 0;
                        entity.ThumbY1 = 0;
                        entity.ThumbY2 = 0;

                        if (Save(entity, context))
                        {
                            InitializeImage(entity);

                            oldImageURL.DeleteFile();
                        }
                        else
                        {
                            newImageURL.DeleteFile();
                        }
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = ImageURLFileUpload.Message;
                    }
                }
                else
                {
                    if (Save(entity, context))
                    {
                        InitializeImage(entity);
                    }
                }

                #endregion UPLOADED IMAGE
            }
        }

        private void SaveToolbar_CustomClick(object sender, EventArgs e)
        {
            if (Page.IsValid && ImageID != null)
            {
                using (var context = new OWDAROEntities())
                {
                    var entity = (from set in context.OW_Images
                                  where set.ImageID == ImageID
                                  select set).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.FocusPointX = FocusPointXTextBox.Text.DoubleParse();
                        entity.FocusPointY = FocusPointYTextBox.Text.DoubleParse();

                        try
                        {
                            context.SaveChanges();

                            StatusMessage2.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                            StatusMessage2.MessageType = StatusMessageType.Success;
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);

                            StatusMessage2.Message = ExceptionHelper.GetExceptionMessage(ex);
                            StatusMessage2.MessageType = StatusMessageType.Error;
                        }
                    }
                }
            }
        }

        private void SaveToolbar_Save(object sender, EventArgs e)
        {
            //SAVE CROP
            if (Page.IsValid && ImageID != null)
            {
                using (var context = new OWDAROEntities())
                {
                    var entity = (from set in context.OW_Images
                                  where set.ImageID == ImageID
                                  select set).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.CropHeight = BigImageCropHeight.Value.IntParse();
                        entity.CropWidth = BigImageCropWidth.Value.IntParse();
                        entity.XUnit = BigImageCropXUnit.Value.IntParse();
                        entity.YUnit = BigImageCropYUnit.Value.IntParse();
                        entity.X1 = BigImageCropX1.Value.IntParse();
                        entity.X2 = BigImageCropX2.Value.IntParse();
                        entity.Y1 = BigImageCropY1.Value.IntParse();
                        entity.Y2 = BigImageCropY2.Value.IntParse();

                        try
                        {
                            context.SaveChanges();

                            StatusMessage2.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                            StatusMessage2.MessageType = StatusMessageType.Success;

                            OnImageUploaded(entity.ImageID);

                            InitializeCropEditor(entity);

                            BigImageCropX1.Value = "0";
                            BigImageCropY1.Value = "0";
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);

                            StatusMessage2.Message = ExceptionHelper.GetExceptionMessage(ex);
                            StatusMessage2.MessageType = StatusMessageType.Error;
                        }
                    }
                }
            }
        }

        private void SetNewHeightAndWidth(Stream stream)
        {
            var width = 0;
            var height = 0;

            ImageHelper.GetWidthAndHeight(stream, out width, out height);

            UploadedImageWidthHiddenField.Value = width.ToString();
            UploadedImageHeightHiddenField.Value = height.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageURLFileUpload.ValidationErrorMessage = Validator.ImageValidationErrorMessage;
            ImageURLFileUpload.ValidationExpression = Validator.ImageValidationExpression;

            ShowWebImageCheckBox.CheckedChanged += ShowWebImageCheckBox_CheckedChanged;
            FormToolbar1.CustomClick += FormToolbar1_CustomClick;
            FormToolbar1.Save += FormToolbar1_Save;
            SaveToolbar.Save += SaveToolbar_Save;
            SaveToolbar.CustomClick += SaveToolbar_CustomClick;
            SaveToolbar.Update += SaveToolbar_Update;
            SaveToolbar.CustomPopupClick += SaveToolbar_CustomPopupClick;
            SaveToolbar.CustomPopupCancelButtonText = "Cancel";
            SaveToolbar.CustomPopupMessage = "Are you sure you want to reset the image to its original values?";
            SaveToolbar.CustomPopupOKButtonText = "Yes";
        }

        private void SaveToolbar_Update(object sender, EventArgs e)
        {
            //UPDATE DIMENSIONS

            if (Page.IsValid && ImageID != null)
            {
                using (var context = new OWDAROEntities())
                {
                    var entity = (from set in context.OW_Images
                                  where set.ImageID == ImageID
                                  select set).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.Quality = (int)EditQualitySlider.Value;
                        entity.ThumbQuality = (int)EditThumbQualitySlider.Value;

                        entity.MaxHeight = EditMaxHeightTextBox.Text.IntParse();
                        entity.MaxWidth = EditMaxWidthTextBox.Text.IntParse();
                        entity.Height = EditHeightTextBox.Text.IntParse();
                        entity.Width = EditWidthTextBox.Text.IntParse();

                        entity.ThumbMaxHeight = EditThumbMaxHeightTextBox.Text.IntParse();
                        entity.ThumbMaxWidth = EditThumbMaxWidthTextBox.Text.IntParse();
                        entity.ThumbHeight = EditThumbHeightTextBox.Text.IntParse();
                        entity.ThumbWidth = EditThumbWidthTextBox.Text.IntParse();

                        try
                        {
                            context.SaveChanges();

                            StatusMessage2.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                            StatusMessage2.MessageType = StatusMessageType.Success;

                            OnImageUploaded(entity.ImageID);

                            InitializeCropEditor(entity);

                            BigImageCropX1.Value = "0";
                            BigImageCropY1.Value = "0";
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);

                            StatusMessage2.Message = ExceptionHelper.GetExceptionMessage(ex);
                            StatusMessage2.MessageType = StatusMessageType.Error;
                        }
                    }
                }
            }
        }

        private void SaveToolbar_CustomPopupClick(object sender, EventArgs e)
        {
            //RESET

            using (var context = new OWDAROEntities())
            {
                var entity = (from set in context.OW_Images
                              where set.ImageID == ImageID
                              select set).FirstOrDefault();

                if (entity != null)
                {
                    entity.Quality = 70;
                    entity.ThumbQuality = 70;

                    entity.FocusPointX = 0;
                    entity.FocusPointY = 0;

                    entity.CropHeight = entity.OriginalHeight;
                    entity.CropWidth = entity.OriginalWidth;
                    entity.MaxHeight = entity.OriginalHeight;
                    entity.MaxWidth = entity.OriginalWidth;
                    entity.Height = entity.OriginalHeight;
                    entity.Width = entity.OriginalWidth;
                    entity.XUnit = entity.OriginalWidth;
                    entity.YUnit = entity.OriginalHeight;
                    entity.X1 = 0;
                    entity.X2 = entity.OriginalWidth;
                    entity.Y1 = 0;
                    entity.Y2 = entity.OriginalHeight;

                    entity.ThumbCropHeight = 0;
                    entity.ThumbCropWidth = 0;
                    entity.ThumbMaxHeight = 200;
                    entity.ThumbMaxWidth = 200;
                    entity.ThumbHeight = 200;
                    entity.ThumbWidth = 200;
                    entity.ThumbXUnit = 0;
                    entity.ThumbYUnit = 0;
                    entity.ThumbX1 = 0;
                    entity.ThumbX2 = 0;
                    entity.ThumbY1 = 0;
                    entity.ThumbY2 = 0;

                    try
                    {
                        context.SaveChanges();

                        StatusMessage2.MessageType = StatusMessageType.Success;
                        StatusMessage2.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                        OnImageUploaded(entity.ImageID);

                        InitializeImage(entity);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage2.MessageType = StatusMessageType.Error;
                        StatusMessage2.Message = ExceptionHelper.GetExceptionMessage(ex);
                    }
                }
            }
        }

        protected void ShowWebImageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleWebToLocalFields(ShowWebImageCheckBox.Checked);
        }

        private void ToggleWebToLocalFields(bool showWeb)
        {
            if (showWeb)
            {
                WebImageThumbURLTextBox.Visible = true;
                WebImageURLTextBox.Visible = true;
                ImageURLFileUpload.Visible = false;
            }
            else
            {
                WebImageThumbURLTextBox.Visible = false;
                WebImageURLTextBox.Visible = false;
                ImageURLFileUpload.Visible = true;
            }
        }
    }
}