using OWDARO.Helpers;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class FileUploadAdv : System.Web.UI.UserControl
    {
        private string fileName = Guid.NewGuid().ToString();

        private string filePath = LocalStorages.Storage;

        private string mappedPath;

        private float maxFileSizeBytes;

        private float maxFileSizeKB = 2048;

        private float maxFileSizeMB = 2;

        private string message;

        private bool success;

        private string url;

        public string Direction
        {
            set
            {
                FileUpload1.Style.Add("direction", value);

                DIV.Attributes.Remove("class");
                LABEL.Attributes.Remove("class");

                DIV.Attributes.Add("class", value);
                LABEL.Attributes.Add("class", value);
            }
        }

        /// <summary>
        /// Gets the content(Stream) of the uploaded file
        /// </summary>
        /// <remarks></remarks>
        public Stream FileContent
        {
            get
            {
                return FileUpload1.FileContent;
            }
        }

        /// <summary>
        /// Gets the file extension of the selected file
        /// </summary>
        /// <remarks></remarks>
        public string FileExtension
        {
            get
            {
                return Path.GetExtension(FileUpload1.FileName);
            }
        }

        /// <summary>
        /// Gets or sets the name of the file. If left blank a GUID filename will be generated
        /// </summary>
        /// <value>The name of the file</value>
        /// <remarks></remarks>
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    fileName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the relative path where file will be uploaded, excluding the filename
        /// </summary>
        /// <value>The relative file path</value>
        /// <remarks></remarks>
        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
            }
        }

        /// <summary>
        /// Refers to the FileUpload ASP.Net control inside the custom control
        /// </summary>
        public FileUpload FileUpload
        {
            get
            {
                return FileUpload1;
            }

            set
            {
                FileUpload1 = value;
            }
        }

        public string ContainerID
        {
            get
            {
                return Container.ClientID;
            }
        }

        public bool HasFile
        {
            get
            {
                if (!FileUpload1.HasFile)
                {
                    message = "No File Selected";
                }

                return FileUpload1.HasFile;
            }
        }

        public string InitialValue
        {
            get
            {
                return RequiredFieldValidator1.InitialValue;
            }

            set
            {
                RequiredFieldValidator1.InitialValue = value;
                RequiredFieldValidator1.Visible = true;
            }
        }

        public RegularExpressionValidator RegularExpressionValidator
        {
            get
            {
                return RegularExpressionValidator1;
            }
        }

        public RequiredFieldValidator RequiredFieldValidator
        {
            get
            {
                return RequiredFieldValidator1;
            }
        }

        public string LabelText
        {
            get
            {
                return Label1.Text;
            }

            set
            {
                Label1.Text = value;
            }
        }

        /// <summary>
        /// Gets the mapped(Absolute) path of the uploaded file after successful upload
        /// </summary>
        /// <remarks></remarks>
        public string MappedPath
        {
            get
            {
                return mappedPath;
            }
        }

        /// <summary>
        /// Sets the maximum allowed file size to be uploaded in KB
        /// </summary>
        /// <value>The max file size in KB</value>
        /// <remarks></remarks>
        public float MaxFileSizeKB
        {
            set
            {
                maxFileSizeKB = value;

                maxFileSizeBytes = maxFileSizeKB * 1024;
            }
        }

        /// <summary>
        /// Sets the maximum allowed file size to be uploaded in MB
        /// </summary>
        /// <value>The max file size in MB</value>
        /// <remarks></remarks>
        public float MaxFileSizeMB
        {
            set
            {
                maxFileSizeMB = value;

                maxFileSizeBytes = (maxFileSizeMB * 1024) * 1024;
            }
        }

        /// <summary>
        /// Gets the message containing the status of the file upload operation, like "File Too Large", "Upload Successful", "No File Selected"
        /// </summary>
        /// <remarks>This property is initialized after calling the Upload() method</remarks>
        public string Message
        {
            get
            {
                return message;
            }
        }

        public string RequiredErrorMessage
        {
            get
            {
                return RequiredFieldValidator1.ErrorMessage;
            }

            set
            {
                RequiredFieldValidator1.ErrorMessage = value;
                RequiredFieldValidator1.Visible = true;
                FileUpload1.CssClass += " required";
            }
        }

        public string SmallLabelText
        {
            get
            {
                return SmallLabel1.Text;
            }

            set
            {
                SmallLabel1.Text = value;
            }
        }

        /// <summary>
        /// Gets a Boolean value indicating whether the file upload process was successful or not
        /// </summary>
        /// <remarks>This property is initialized after calling the Upload() method</remarks>
        public bool Success
        {
            get
            {
                return success;
            }

            set
            {
                success = value;
            }
        }

        /// <summary>
        /// Gets the complete relative URL of the uploaded file including the filename
        /// </summary>
        /// <remarks></remarks>
        public string Url
        {
            get
            {
                return url;
            }
        }

        public string ValidationErrorMessage
        {
            set
            {
                RegularExpressionValidator1.Visible = true;
                RegularExpressionValidator1.ErrorMessage = value;
            }
        }

        public string ValidationExpression
        {
            set
            {
                RegularExpressionValidator1.Visible = true;
                RegularExpressionValidator1.ValidationExpression = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                RegularExpressionValidator1.ValidationGroup = value;
                RequiredFieldValidator1.ValidationGroup = value;
            }
        }

        public bool IsFileSizeOK()
        {
            if (FileUpload1.PostedFile.ContentLength < maxFileSizeBytes)
            {
                message = "File Size OK";
                return true;
            }
            else
            {
                message = "File Size Too Large";
                success = false;
                return false;
            }
        }

        /// <summary>
        /// Initiates the file upload process
        /// </summary>
        /// <remarks></remarks>
        public void Upload()
        {
            if (FileUpload1.HasFile)
            {
                if (IsFileSizeOK())
                {
                    UploadToServer();
                }
            }
            else
            {
                success = false;
                message = "No File Selected";
            }
        }

        public void CompressAndUpload(int quality, string outputPath)
        {
            if (FileUpload1.HasFile)
            {
                if (IsFileSizeOK())
                {
                    ImageHelper.Compress(quality, FileUpload1.FileContent, outputPath);

                    success = true;
                    message = "Compress And Upload Successful";
                }
            }
            else
            {
                success = false;
                message = "No File Selected";
            }
        }

        public void ResizeAndUpload(int width, string outputPath, bool highQuality)
        {
            if (FileUpload1.HasFile)
            {
                if (IsFileSizeOK())
                {
                    ImageHelper.Resize(width, FileUpload1.FileContent, highQuality, outputPath);

                    success = true;
                    message = "Resize And Upload Successful";
                }
            }
            else
            {
                success = false;
                message = "No File Selected";
            }
        }

        public void ResizeCompressAndUpload(int quality, int width, string outputPath, bool highQuality)
        {
            if (FileUpload1.HasFile)
            {
                if (IsFileSizeOK())
                {
                    Stream stream = ImageHelper.Resize(width, FileUpload1.FileContent, highQuality);
                    ImageHelper.Compress(quality, stream, outputPath);

                    success = true;
                    message = "Resize, Compress And Upload Successful";
                }
            }
            else
            {
                success = false;
                message = "No File Selected";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void UploadToServer()
        {
            url = Path.Combine(filePath, fileName) + FileExtension;

            mappedPath = Server.MapPath(url);

            try
            {
                FileUpload1.SaveAs(mappedPath);

                success = true;
                message = "Upload Successful";
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.InnerException.Message;
            }
        }

        public FieldWidth FieldWidth
        {
            set
            {
                switch (value)
                {
                    case FieldWidth.xxsmall:
                        FileUpload1.CssClass = "fileupload xxsmall";
                        break;

                    case FieldWidth.xsmall:
                        FileUpload1.CssClass = "fileupload xsmall";
                        break;

                    case FieldWidth.small:
                        FileUpload1.CssClass = "fileupload small";
                        break;

                    case FieldWidth.medium:
                        FileUpload1.CssClass = "fileupload medium";
                        break;

                    case FieldWidth.large:
                        FileUpload1.CssClass = "fileupload large";
                        break;

                    case FieldWidth.largeXL:
                        FileUpload1.CssClass = "fileupload largeXL";
                        break;

                    case FieldWidth.full:
                        FileUpload1.CssClass = "fileupload full";
                        break;

                    default:
                        FileUpload1.CssClass = "fileupload full";
                        break;
                }
            }
        }
    }
}