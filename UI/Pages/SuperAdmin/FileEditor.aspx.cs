using OWDARO;
using OWDARO.Helpers;
using OWDARO.Util;
using System;
using System.IO;

namespace ProjectJKL.UI.Pages.SuperAdmin
{
    public partial class FileEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utilities.BuildTree(TreeView1, string.Empty, AppConfig.FileEditorSearchPatterns.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

                TreeView1.Nodes[0].Select();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            using (var writer = new StreamWriter(FilePathHiddenField.Value))
            {
                try
                {
                    writer.WriteLine(FileContentTextBox.Text);

                    StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Success;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                }
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            var path = TreeView1.SelectedNode.Value;

            var fileInfo = new FileInfo(path);

            FileContentTextBox.Text = File.ReadAllText(fileInfo.FullName);

            FilePathHiddenField.Value = fileInfo.FullName;
        }
    }
}