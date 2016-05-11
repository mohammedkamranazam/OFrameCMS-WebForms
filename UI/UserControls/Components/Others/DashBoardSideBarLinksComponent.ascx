<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashBoardSideBarLinksComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.DashBoardSideBarLinksComponent" %>
<div id="left_menu">
    <ul id="main_menu" class="main_menu">
        <li class="limenu" runat="server" id="DashBoardList">
            <asp:HyperLink ID="DashBoardHyperLink" runat="server">
                <span class="ico gray shadow home"></span>
                <b>Dashboard</b>
            </asp:HyperLink>
        </li>
        <li class="limenu" runat="server" id="OW_MembershipList">
            <asp:HyperLink ID="HyperLink1" runat="server">
                <span class="ico gray shadow group"></span>
                <b>Membership</b>
            </asp:HyperLink>
            <ul>
                <li runat="server" id="AddUserList">
                    <asp:HyperLink ID="AddUserHyperLink" runat="server" NavigateUrl="~/UI/Pages/Common/UserAdd.aspx">Add User</asp:HyperLink>
                </li>
                <li runat="server" id="ListUsersList">
                    <asp:HyperLink ID="ListUsersHyperLink" runat="server" NavigateUrl="~/UI/Pages/Common/UserList.aspx">List Users</asp:HyperLink>
                </li>
                <li runat="server" id="AddUserCategoryList">
                    <asp:HyperLink ID="AddUserCategoryHyperLink" runat="server" NavigateUrl="~/UI/Pages/SuperAdmin/UserCategoryAdd.aspx">Add User's Category</asp:HyperLink>
                </li>
                <li runat="server" id="ListUserCategoriesList">
                    <asp:HyperLink ID="ListUserCategoriesHyperLink" runat="server" NavigateUrl="~/UI/Pages/SuperAdmin/UserCategoryList.aspx">List User Categories</asp:HyperLink>
                </li>
                <li runat="server" id="AddRoleList">
                    <asp:HyperLink ID="AddRoleHyperLink" runat="server" NavigateUrl="~/UI/Pages/SuperAdmin/RoleAdd.aspx">Add Role</asp:HyperLink>
                </li>
                <li runat="server" id="ManageRolesList">
                    <asp:HyperLink ID="ManageRolesHyperLink" runat="server" NavigateUrl="~/UI/Pages/SuperAdmin/RoleManage.aspx">Manage Roles</asp:HyperLink>
                </li>
                <li runat="server" id="AccessManagerList">
                    <asp:HyperLink ID="AccessManagerHyperLink" runat="server" NavigateUrl="~/UI/Pages/SuperAdmin/AccessManager.aspx">Access Manager</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="OW_ToolsList">
            <asp:HyperLink ID="HyperLink117" runat="server">
                <span class="ico gray shadow screwdriver"></span>
                <b>Tools</b>
            </asp:HyperLink>
            <ul>
                <li runat="server" id="KeywordsSettingsList">
                    <asp:HyperLink ID="HyperLink101" runat="server" NavigateUrl="~/UI/Pages/Admin/OFrame/Settings.aspx">Key Word Settings</asp:HyperLink>
                </li>
                <li runat="server" id="FileEditorList">
                    <asp:HyperLink ID="FileEditorHyperLink" runat="server" NavigateUrl="~/UI/Pages/SuperAdmin/FileEditor.aspx">File Editor</asp:HyperLink>
                </li>
                <li runat="server" id="ActivityLogList">
                    <asp:HyperLink ID="HyperLink104" runat="server" NavigateUrl="~/UI/Pages/Admin/OFrame/ActivityList.aspx">Activity Log</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="OW_MenuList">
            <asp:HyperLink ID="HyperLink56" runat="server">
                <span class="ico gray shadow list"></span>
                <b>Menu</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink57" runat="server" NavigateUrl="~/UI/Pages/Admin/OFrame/MenuAdd.aspx">Add Menu</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink58" runat="server" NavigateUrl="~/UI/Pages/Admin/OFrame/MenuList.aspx">List Menu</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="GY_SlideList">
            <asp:HyperLink ID="HyperLink98" runat="server">
                <span class="ico gray shadow dial"></span>
                <b>Slide</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink99" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/SlideAdd.aspx">Add Slide</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink100" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/SlideList.aspx">List Slides</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="GY_AlbumList">
            <asp:HyperLink ID="HyperLink10" runat="server">
                <span class="ico gray shadow photo_album"></span>
                <b>Albums</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/AlbumAdd.aspx">Add Album</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/AlbumList.aspx">List Albums</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="GY_VideoCategoriesList">
            <asp:HyperLink ID="HyperLink71" runat="server">
                <span class="ico gray shadow list"></span>
                <b>Video Categories</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink72" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoCategoriesAdd.aspx">Add Video Categories</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink73" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoCategoriesList.aspx">List Video Categories</asp:HyperLink>
                </li>
            </ul>
        </li>

        <li class="limenu" runat="server" id="GY_VideoSetList">
            <asp:HyperLink ID="HyperLink74" runat="server">
                <span class="ico gray shadow paste"></span>
                <b>Video Set</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink75" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoSetAdd.aspx">Add Video Set</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink76" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoSetList.aspx">List Video Set</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="GY_VideosList">
            <asp:HyperLink ID="HyperLink62" runat="server">
                <span class="ico gray shadow play"></span>
                <b>Videos</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink63" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoAdd.aspx">Add Video</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink64" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoList.aspx">List Videos</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="GY_AudioCategoriesList">
            <asp:HyperLink ID="HyperLink80" runat="server">
                <span class="ico gray shadow list"></span>
                <b>Audio Categories</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink81" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioCategoriesAdd.aspx">Add Audio Categories</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink82" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioCategoriesList.aspx">List Audio Categories</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="GY_AudioSetList">
            <asp:HyperLink ID="HyperLink83" runat="server">
                <span class="ico gray shadow paste"></span>
                <b>Audio Set</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink84" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioSetAdd.aspx">Add Audio Set</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink85" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioSetList.aspx">List Audio Set</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="GY_AudiosList">
            <asp:HyperLink ID="HyperLink65" runat="server">
                <span class="ico gray shadow music"></span>
                <b>Audios</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink66" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioAdd.aspx">Add Audio</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink67" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioList.aspx">List Audios</asp:HyperLink>
                </li>
            </ul>
        </li>

        <li class="limenu" runat="server" id="GY_DriveList">
            <asp:HyperLink ID="HyperLink86" runat="server">
                <span class="ico gray shadow hard_disk"></span>
                <b>Drives</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink87" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/DriveAdd.aspx">Add Drive</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink88" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/DriveList.aspx">List Drives</asp:HyperLink>
                </li>
            </ul>
        </li>

        <li class="limenu" runat="server" id="GY_FolderList">
            <asp:HyperLink ID="HyperLink89" runat="server">
                <span class="ico gray shadow folder"></span>
                <b>Folders</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink90" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/FolderAdd.aspx">Add Folder</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink91" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/FolderList.aspx">List Folders</asp:HyperLink>
                </li>
            </ul>
        </li>

        <li class="limenu" runat="server" id="GY_FileTypeList">
            <asp:HyperLink ID="HyperLink95" runat="server">
                <span class="ico gray shadow file"></span>
                <b>File Types</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink96" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/FileTypeAdd.aspx">Add File Type</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink97" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/FileTypeList.aspx">List Files Type</asp:HyperLink>
                </li>
            </ul>
        </li>

        <li class="limenu" runat="server" id="GY_FileList">
            <asp:HyperLink ID="HyperLink92" runat="server">
                <span class="ico gray shadow file"></span>
                <b>Files</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink93" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/FileAdd.aspx">Add File</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink94" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/FileList.aspx">List Files</asp:HyperLink>
                </li>
            </ul>
        </li>

        <li class="limenu" runat="server" id="GY_EventsList">
            <asp:HyperLink ID="HyperLink2" runat="server">
                <span class="ico gray shadow thunder"></span>
                <b>Events</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink49" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/EventAdd.aspx">Add Event</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink50" runat="server" NavigateUrl="~/UI/Pages/Admin/Gallery/EventList.aspx">List Events</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="ME_SubscriptionsList">
            <asp:HyperLink ID="HyperLink102" runat="server">
                <span class="ico gray shadow mail"></span>
                <b>Subscriptions</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink103" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/SubscriptionsList.aspx">List Subscriptions</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="ME_PostCategoriesList">
            <asp:HyperLink ID="HyperLink3" runat="server">
                <span class="ico gray shadow list"></span>
                <b>Post Categories</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/PostCategoriesAdd.aspx">Add Post Category</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/PostCategoriesList.aspx">List Post Castegories</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="ME_PostsList">
            <asp:HyperLink ID="HyperLink6" runat="server">
                <span class="ico gray shadow document"></span>
                <b>Posts</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/PostsAdd.aspx">Add Post</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/PostsList.aspx">List Posts</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="ME_ClientList">
            <asp:HyperLink ID="HyperLink105" runat="server">
                <span class="ico gray shadow administrator"></span>
                <b>Clients</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink106" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/ClientAdd.aspx">Add Client</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink107" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/ClientList.aspx">List Clients</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="ME_PortfolioList">
            <asp:HyperLink ID="HyperLink108" runat="server">
                <span class="ico gray shadow history"></span>
                <b>Portfolios</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink109" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/PortfolioAdd.aspx">Add Portfolio</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink110" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/PortfolioList.aspx">List Portfolios</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="ME_ProjectCategoriesList">
            <asp:HyperLink ID="HyperLink111" runat="server">
                <span class="ico gray shadow ruler_square"></span>
                <b>Project Categories</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink112" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/ProjectCategoriesAdd.aspx">Add Project category</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink113" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/ProjectCategoriesList.aspx">List Project Categories</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="ME_TestimonialList">
            <asp:HyperLink ID="HyperLink114" runat="server">
                <span class="ico gray shadow hand_thumbsup"></span>
                <b>Testimonials</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink115" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/TestimonialAdd.aspx">Add Testimonial</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink116" runat="server" NavigateUrl="~/UI/Pages/Admin/Media/TestimonialList.aspx">List Testimonials</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_OrdersList">
            <asp:HyperLink ID="HyperLink59" runat="server">
                <span class="ico gray shadow diary"></span>
                <b>Orders</b>
            </asp:HyperLink>
            <ul>
                <li style="display: none;">
                    <asp:HyperLink ID="HyperLink60" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/OrdersAdd.aspx">Add Order</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink61" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/OrdersList.aspx">List Orders</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_SectionsList">
            <asp:HyperLink ID="HyperLink13" runat="server">
                <span class="ico gray shadow door"></span>
                <b>Sections</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SectionsAdd.aspx">Add Section</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SectionsList.aspx">List Sections</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_CategoriesList">
            <asp:HyperLink ID="HyperLink16" runat="server">
                <span class="ico gray shadow list"></span>
                <b>Categories</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/CategoriesAdd.aspx">Add Category</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/CategoriesList.aspx">List Categories</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_SubCategoriesList">
            <asp:HyperLink ID="HyperLink19" runat="server">
                <span class="ico gray shadow paste"></span>
                <b>Sub Categories</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink20" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SubCategoriesAdd.aspx">Add Sub Category</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink21" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SubCategoriesList.aspx">List Sub Categories</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_ProductsList">
            <asp:HyperLink ID="HyperLink22" runat="server">
                <span class="ico gray shadow shopping_basket"></span>
                <b>Products</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink23" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductsAdd.aspx">Add Product</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink24" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductsList.aspx">List Products</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_BrandsList">
            <asp:HyperLink ID="HyperLink25" runat="server">
                <span class="ico gray shadow bold"></span>
                <b>Brands</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink26" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/BrandsAdd.aspx">Add Brand</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink27" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/BrandsList.aspx">List Brands</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_SizesList">
            <asp:HyperLink ID="HyperLink28" runat="server">
                <span class="ico gray shadow dimensions"></span>
                <b>Sizes</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink29" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SizesAdd.aspx">Add Size</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink30" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SizesList.aspx">List Sizes</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_ColorsList">
            <asp:HyperLink ID="HyperLink31" runat="server">
                <span class="ico gray shadow brush"></span>
                <b>Colors</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink32" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ColorsAdd.aspx">Add Color</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink33" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ColorsList.aspx">List Colors</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_AvailabilityTypesList">
            <asp:HyperLink ID="HyperLink34" runat="server">
                <span class="ico gray shadow hand_point"></span>
                <b>Availability Types</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink35" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/AvailabilityTypesAdd.aspx">Add Availability Type</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink36" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/AvailabilityTypesList.aspx">List Availability Types</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_IconsList">
            <asp:HyperLink ID="HyperLink37" runat="server">
                <span class="ico gray shadow emoticon_confused"></span>
                <b>Icons</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink38" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/IconsAdd.aspx">Add Icon</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink39" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/IconsList.aspx">List Icons</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_ProductModelsList">
            <asp:HyperLink ID="HyperLink40" runat="server">
                <span class="ico gray shadow moon"></span>
                <b>Product Models</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink41" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductModelsAdd.aspx">Add Product Model</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink42" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductModelsList.aspx">List Product Models</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_ProductTypesList">
            <asp:HyperLink ID="HyperLink43" runat="server">
                <span class="ico gray shadow contrast"></span>
                <b>Product Types</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink44" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductTypesAdd.aspx">Add Product Type</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink45" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductTypesList.aspx">List Product Types</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li class="limenu" runat="server" id="SC_UnitsList">
            <asp:HyperLink ID="HyperLink46" runat="server">
                <span class="ico gray shadow underlined"></span>
                <b>Units</b>
            </asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink47" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/UnitsAdd.aspx">Add Unit</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink48" runat="server" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/UnitsList.aspx">List Units</asp:HyperLink>
                </li>
            </ul>
        </li>
    </ul>
</div>