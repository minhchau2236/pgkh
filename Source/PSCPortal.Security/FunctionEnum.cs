using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Security
{
    public enum FUNCTIONS
    {
        ArticleTrash_Restore = 1,
        ArticleTrash_Delete = 2,

        DocumentTrash_Restore = 3,
        DocumentTrash_Delete = 4,

        Module_Add = 5,
        Module_Edit = 6,
        Module_Delete = 7,

        Portlet_Add = 8,
        Portlet_Edit = 9,
        Portlet_Delete = 10,

        User_Add = 11,
        User_Edit = 12,
        User_EditRole = 13,
        User_Delete = 14,
        User_ChangePassword = 15,

        Role_Add = 16,
        Role_Edit = 17,
        Role_EditUser = 18,
        Role_Delete = 19,

        Topic_Add = 20,
        Topic_Edit = 21,
        Topic_Copy = 22,
        Topic_MakeMenu = 23,
        Topic_Delete = 24,
        Topic_Permission = 25,

        DocumentType_Add = 26,
        DocumentType_Edit = 27,
        DocumentType_Delete = 28,
        DocumentType_Permission = 29,

        ClipNews_Add = 30,
        ClipNews_Edit = 31,
        ClipNews_Delete = 32,
        ClipNews_Public = 33,
        ClipNews_UnPublic = 34,

        Department_Add = 35,
        Department_Edit = 36,
        Department_EditArticle = 37,
        Department_Delete = 38,

        Branch_Add = 39,
        Branch_Edit = 40,
        Branch_Delete = 41,

        Student_Edit = 42,
        Student_Delete = 43,

        Album_Add = 44,
        Album_Edit = 45,
        Album_Delete = 46,

        Photo_Add = 47,
        Photo_Edit = 48,
        Photo_Delete = 49,

        Layout_Add = 62,
        Layout_Edit = 63,
        Layout_Delete = 64,

        Page_Add = 65,
        Page_Edit = 66,
        Page_Copy = 67,
        Page_ChangeStruct = 68,
        Page_Delete = 69,

        MenuMaster_Add = 70,
        MenuMaster_Edit = 71,
        MenuMaster_Copy = 72,
        MenuMaster_Delete = 73,
        MenuMaster_ManageMenu = 74,
        MenuMaster_MakeTopic = 75

    }
}
