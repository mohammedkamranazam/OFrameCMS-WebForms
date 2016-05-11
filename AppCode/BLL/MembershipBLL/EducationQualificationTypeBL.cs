using OWDARO.UI.UserControls.Controls;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.MembershipBLL
{
    public static class EducationQualificationTypeBL
    {
        public static void PopulateEducationQualificationTypesList(DropDownListAdv EducationQualificationTypeDropDownList)
        {
            using (var context = new MembershipEntities())
            {
                PopulateEducationQualificationTypesList(EducationQualificationTypeDropDownList, context);
            }
        }

        public static void PopulateEducationQualificationTypesList(DropDownListAdv EducationQualificationTypeDropDownList, MembershipEntities context)
        {
            var query = (from types in context.MS_EducationQualificationTypes
                         select types);

            EducationQualificationTypeDropDownList.DataTextField = "Title";
            EducationQualificationTypeDropDownList.DataValueField = "EducationQualificationTypeID";
            EducationQualificationTypeDropDownList.DataSource = query.ToList();
            EducationQualificationTypeDropDownList.AddSelect();
        }

        public static async Task PopulateEducationQualificationTypesListAsync(DropDownListAdv EducationQualificationTypeDropDownList)
        {
            using (var context = new MembershipEntities())
            {
                await PopulateEducationQualificationTypesListAsync(EducationQualificationTypeDropDownList, context);
            }
        }

        public static async Task PopulateEducationQualificationTypesListAsync(DropDownListAdv EducationQualificationTypeDropDownList, MembershipEntities context)
        {
            var query = await (from types in context.MS_EducationQualificationTypes
                               select types).ToListAsync();

            EducationQualificationTypeDropDownList.DataTextField = "Title";
            EducationQualificationTypeDropDownList.DataValueField = "EducationQualificationTypeID";
            EducationQualificationTypeDropDownList.DataSource = query;
            EducationQualificationTypeDropDownList.AddSelect();
        }
    }
}