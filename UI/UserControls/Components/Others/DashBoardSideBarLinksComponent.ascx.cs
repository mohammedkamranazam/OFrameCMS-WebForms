using OWDARO.BLL.MembershipBLL;
using System;
using System.Web.Security;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class DashBoardSideBarLinksComponent : System.Web.UI.UserControl
    {
        private void InitializeInactivatedUserRoleMenu(ProjectJKL.AppCode.DAL.MembershipModel.MS_Users userEntity)
        {
            if (userEntity.UserRole == UserRoles.InactivatedRole)
            {
                AddUserCategoryList.Visible = false;
                ListUserCategoriesList.Visible = false;
                AddRoleList.Visible = false;
                ManageRolesList.Visible = false;
                AccessManagerList.Visible = false;
                FileEditorList.Visible = false;

                OW_MembershipList.Visible = false;
                OW_ToolsList.Visible = false;
                OW_MenuList.Visible = false;

                SC_AvailabilityTypesList.Visible = false;
                SC_BrandsList.Visible = false;
                SC_CategoriesList.Visible = false;
                SC_ColorsList.Visible = false;
                SC_IconsList.Visible = false;
                SC_ProductModelsList.Visible = false;
                SC_ProductsList.Visible = false;
                SC_ProductTypesList.Visible = false;
                SC_SectionsList.Visible = false;
                SC_SizesList.Visible = false;
                SC_SubCategoriesList.Visible = false;
                SC_UnitsList.Visible = false;
                SC_OrdersList.Visible = false;

                ME_PostCategoriesList.Visible = false;
                ME_PostsList.Visible = false;
                ME_SubscriptionsList.Visible = false;
                ME_ClientList.Visible = false;
                ME_PortfolioList.Visible = false;
                ME_ProjectCategoriesList.Visible = false;
                ME_TestimonialList.Visible = false;

                GY_AlbumList.Visible = false;
                GY_EventsList.Visible = false;
                GY_AudioCategoriesList.Visible = false;
                GY_AudioSetList.Visible = false;
                GY_AudiosList.Visible = false;
                GY_VideoCategoriesList.Visible = false;
                GY_VideoSetList.Visible = false;
                GY_VideosList.Visible = false;
                GY_DriveList.Visible = false;
                GY_FolderList.Visible = false;
                GY_FileList.Visible = false;
                GY_FileTypeList.Visible = false;
                GY_SlideList.Visible = false;
            }
        }

        private void InitializeMasterUserRoleMenu(ProjectJKL.AppCode.DAL.MembershipModel.MS_Users userEntity)
        {
            if (userEntity.UserRole == UserRoles.AdminRole)
            {
                AddUserCategoryList.Visible = false;
                ListUserCategoriesList.Visible = false;
                AddRoleList.Visible = false;
                ManageRolesList.Visible = false;
                AccessManagerList.Visible = false;
                FileEditorList.Visible = false;

                if (!AppConfig.ShoppingCartEnabled)
                {
                    SC_AvailabilityTypesList.Visible = false;
                    SC_BrandsList.Visible = false;
                    SC_CategoriesList.Visible = false;
                    SC_ColorsList.Visible = false;
                    SC_IconsList.Visible = false;
                    SC_ProductModelsList.Visible = false;
                    SC_ProductsList.Visible = false;
                    SC_ProductTypesList.Visible = false;
                    SC_SectionsList.Visible = false;
                    SC_SizesList.Visible = false;
                    SC_SubCategoriesList.Visible = false;
                    SC_UnitsList.Visible = false;
                    SC_OrdersList.Visible = false;
                }

                if (!AppConfig.MediaEnabled)
                {
                    ME_PostCategoriesList.Visible = false;
                    ME_PostsList.Visible = false;
                    ME_SubscriptionsList.Visible = false;
                    ME_ClientList.Visible = false;
                    ME_PortfolioList.Visible = false;
                    ME_ProjectCategoriesList.Visible = false;
                    ME_TestimonialList.Visible = false;
                }

                if (!AppConfig.GalleryEnabled)
                {
                    GY_AlbumList.Visible = false;
                    GY_EventsList.Visible = false;
                    GY_AudioCategoriesList.Visible = false;
                    GY_AudioSetList.Visible = false;
                    GY_AudiosList.Visible = false;
                    GY_VideoCategoriesList.Visible = false;
                    GY_VideoSetList.Visible = false;
                    GY_VideosList.Visible = false;
                    GY_DriveList.Visible = false;
                    GY_FolderList.Visible = false;
                    GY_FileList.Visible = false;
                    GY_FileTypeList.Visible = false;
                    GY_SlideList.Visible = false;
                }
            }
        }

        private void InitializeUserUserRoleMenu(ProjectJKL.AppCode.DAL.MembershipModel.MS_Users userEntity)
        {
            if (userEntity.UserRole == UserRoles.UserRole)
            {
                AddUserCategoryList.Visible = false;
                ListUserCategoriesList.Visible = false;
                AddRoleList.Visible = false;
                ManageRolesList.Visible = false;
                AccessManagerList.Visible = false;
                FileEditorList.Visible = false;

                OW_MembershipList.Visible = false;
                OW_ToolsList.Visible = false;
                OW_MenuList.Visible = false;

                SC_AvailabilityTypesList.Visible = false;
                SC_BrandsList.Visible = false;
                SC_CategoriesList.Visible = false;
                SC_ColorsList.Visible = false;
                SC_IconsList.Visible = false;
                SC_ProductModelsList.Visible = false;
                SC_ProductsList.Visible = false;
                SC_ProductTypesList.Visible = false;
                SC_SectionsList.Visible = false;
                SC_SizesList.Visible = false;
                SC_SubCategoriesList.Visible = false;
                SC_UnitsList.Visible = false;
                SC_OrdersList.Visible = false;

                ME_PostCategoriesList.Visible = false;
                ME_PostsList.Visible = false;
                ME_SubscriptionsList.Visible = false;
                ME_ClientList.Visible = false;
                ME_PortfolioList.Visible = false;
                ME_ProjectCategoriesList.Visible = false;
                ME_TestimonialList.Visible = false;

                GY_AlbumList.Visible = false;
                GY_EventsList.Visible = false;
                GY_AudioCategoriesList.Visible = false;
                GY_AudioSetList.Visible = false;
                GY_AudiosList.Visible = false;
                GY_VideoCategoriesList.Visible = false;
                GY_VideoSetList.Visible = false;
                GY_VideosList.Visible = false;
                GY_DriveList.Visible = false;
                GY_FolderList.Visible = false;
                GY_FileList.Visible = false;
                GY_FileTypeList.Visible = false;
                GY_SlideList.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var user = Membership.GetUser();

                if (user != null)
                {
                    var userEntity = UserBL.GetUserByUsername(user.UserName);

                    DashBoardHyperLink.NavigateUrl = UserBL.GetRootFolder(user.UserName);

                    InitializeInactivatedUserRoleMenu(userEntity);

                    InitializeUserUserRoleMenu(userEntity);

                    InitializeMasterUserRoleMenu(userEntity);
                }
            }
        }
    }
}