using Common.AspNetCore.DataTableConfig;
using Microsoft.AspNetCore.Mvc;

namespace Administrator.Infrastructure.DataTableHelper
{
    public static class InitTableView
    {
        public static IUrlHelper UrlHelper { get; set; }

        static InitTableView()
        {

        }
        public static DataTableModel UserDataTable()
        {
            var table = new DataTableModel
            {
                Name = "user",
                ClassNames = "table border-top",
                UrlRead = new DataUrl("/UserManager/GetListPaging", UrlHelper),
                EnableExport = true,
                Dom = """
                "dom": '<"card-header flex-column flex-md-row"<"dt-action-buttons text-end pt-3 pt-md-0"B>><"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
                """,
                ButtonHeaders = new List<ButtonHeader>()
                {
                    new ButtonHeader("<i class=\"mdi mdi-plus me-sm-1\"></i> <span class=\"d-none d-sm-inline-block\"> رکورد جدید</span>","create-new btn btn-primary",new DataUrl("/UserManager/RenderCreate",UrlHelper),true),
                },
                ColumnCollection = new List<ColumnProperty>()
                {
                    new()
                    {
                        AutoWidth = false,
                        Data="avatar",
                        Name="avatar",
                        Render=new RenderPicture("","50px","50px"),
                        Title="پروفایل",
                        Visible=true,
                        Orderable=false,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="firstName",
                        Name="firstName",
                        Render=new RenderDefault(),
                        Title="نام",
                        Visible=true,
                        Orderable=false,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="lastName",
                        Name="lastName",
                        Render=new RenderDefault(),
                        Title="نام خانوادگی",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="phoneNumber",
                        Name="phoneNumber",
                        Render=new RenderDefault(),
                        Title="شماره موبایل",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="gender",
                        Name="gender",
                        Render=new RenderDefault(),
                        Title="جنسیت",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="isActive",
                        Name="isActive",
                        Render=new RenderBoolean(),
                        Title="کاربر فعال",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="userId",
                        Name="userId",
                        Render=new RenderButtonOperation(new RenderButtonRemove(new DataUrl("/UserManager/RenderRemove/", UrlHelper),"حــذف",true),new RenderButtonEdit(new DataUrl("/UserManager/Edit/", UrlHelper),"ویــرایش"),new RenderButtonDetails(new DataUrl("/UserManager/ManageUser/", UrlHelper),"مدیریت")),
                        Title="عملیات",
                        Visible=true,
                        Orderable=false,
                    }


                }
            };


            return table;
        }
        public static DataTableModel RoleDataTable()
        {
            var table = new DataTableModel
            {
                Name = "roleTable",
                ClassNames = "table border-top",
                UrlRead = new DataUrl("/RoleManager/GetListPaging", UrlHelper),
                EnableExport = true,
                Dom = """
                dom: '<"card-header flex-column flex-md-row"<"dt-action-buttons text-end pt-3 pt-md-0"B>><"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
                """,
                ButtonHeaders = new List<ButtonHeader>()
                {
                    new ButtonHeader("<i class=\"mdi mdi-plus me-sm-1\"></i> <span class=\"d-none d-sm-inline-block\"> رکورد جدید</span>","create-new btn btn-primary",new DataUrl("/RoleManager/RenderRole/", UrlHelper),true),
                },
                ColumnCollection = new List<ColumnProperty>()
                {
                    new ()
                    {
                        AutoWidth = false,
                        Data="name",
                        Name="name",
                        Render=new RenderDefault(),
                        Title="نام نقش",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="description",
                        Name="description",
                        Render=new RenderDefault(),
                        Title="توضیحات نقش",
                        Visible=true,
                        Orderable=true,
                    },

                    new ()
                    {
                        AutoWidth = false,
                        Data="roleId",
                        Name="roleId",
                        Render=new RenderButtonOperation(new RenderButtonRemove(new DataUrl("/RoleManager/RenderRemove/", UrlHelper),"حــذف",true),new RenderButtonEdit(new DataUrl("/RoleManager/RenderRole/", UrlHelper),"ویــرایش",true),new RenderButtonDetails(new DataUrl("/DynamicAccessManagement/Index/", UrlHelper),"مدیریت مجوز ها")),
                        Title="عملیات",
                        Visible=true,
                        Orderable=false,
                    }


                }
            };
            return table;
        }
        public static DataTableModel SupportTicketDataTable()
        {
            var table = new DataTableModel
            {
                Name = "SupportTicketTables",
                ClassNames = "table border-top",
                UrlRead = new DataUrl("/TicketManager/GetListPaging", UrlHelper),
                EnableExport = true,
                Dom = """
                dom: '<"card-header flex-column flex-md-row"<"dt-action-buttons text-end pt-3 pt-md-0"B>><"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
                """,
                ButtonHeaders = new List<ButtonHeader>()
                {
                    new ButtonHeader("<i class=\"mdi mdi-plus me-sm-1\"></i> <span class=\"d-none d-sm-inline-block\"> رکورد جدید</span>","create-new btn btn-primary",new DataUrl("/TicketManager/RenderTicket/", UrlHelper),true),
                },
                ColumnCollection = new List<ColumnProperty>()
                {
                    new ()
                    {
                        AutoWidth = false,
                        Data="id",
                        Name="id",
                        Render=new RenderDefault(),
                        Title="شناسه",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="title",
                        Name="title",
                        Render=new RenderDefault(),
                        Title="عنوان",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="state",
                        Name="state",
                        Render=new RenderDefault(),
                        Title="وضعیت",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="id",
                        Name="id",
                        Render=new RenderButtonOperation(new RenderButtonRemove(new DataUrl("/TicketManager/RenderRemove/", UrlHelper),"حــذف",true),new RenderButtonEdit(new DataUrl("/TicketManager/RenderTicket/", UrlHelper),"ویــرایش",true),new RenderButtonDetails(new DataUrl("/TicketManager/Detaile/", UrlHelper),"پیام ها")
                            ,new RenderButtonCustome(new DataUrl("/TicketManager/CloseTicket/", UrlHelper),"بستن تیکت",true)),
                        Title="عملیات",
                        Visible=true,
                        Orderable=false,
                    }


                }
            };
            return table;
        }
        public static DataTableModel MonitoringHttpDataTable()
        {
            var customsButton = new RenderButtonCustome(new DataUrl("/MonitoringManagement/ChangeIsPause/", UrlHelper), "", "btn w-100 btn-primary me-1", true)
            {

                FieldName = "isPause",
                IfEqualResultContext = new Dictionary<object, string> { { true, "فعال کردن" } },
                IfNotEqualResultContext = new Dictionary<object, string> { { false, "معلق" } },
                WorkDataOperation = true,
            };

            var selectInput = new InputHeader()
            {
                ClassNames = "select2",
                Id = "FilterIsPause",
                InputType = InputType.SelectList,
                Title = "مانیتور های فعال یا معلق",
                ParameterName = "FilterIsPause",
                TempDataKey = "IsPauseMonitorSelectListKey",

            };

            var table = new DataTableModel
            {
                Name = "MonitoringHttpTables",
                ClassNames = "table border-top",
                UrlRead = new DataUrl("/MonitoringManagement/GetListHttpPaging", UrlHelper),
                EnableExport = false,
                Dom = """
                dom: '<"card-header flex-column flex-md-row"<"dt-action-buttons text-end pt-3 pt-md-0"B>><"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
                """,
                ButtonHeaders = new List<ButtonHeader>()
                {
                    new ButtonHeader("<i class=\"mdi mdi-plus me-sm-1\"></i> <span class=\"d-none d-sm-inline-block\"> رکورد جدید</span>", "create-new btn btn-primary", new DataUrl("/MonitoringManagement/CreateHttp", UrlHelper), false),
                },
                InputHeaders = new List<InputHeader>()
                {
                    selectInput
                },
                ColumnCollection = new List<ColumnProperty>()
                {
                    new ()
                    {
                        AutoWidth = false,
                        Data="id",
                        Name="id",
                        Render=new RenderDefault(),
                        Title="شناسه",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="name",
                        Name="name",
                        Render=new RenderDefault(),
                        Title="عنوان",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="ownerFullName",
                        Name="ownerFullName",
                        Render=new RenderDefault(),
                        Title="سازنده",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="isPause",
                        Name="isPause",
                        Render=new RenderBoolean(),
                        Title="معلق",
                        Visible=true,
                        Orderable=true,
                    },
                     new ()
                    {
                        AutoWidth = false,
                        Data="id",
                        Name="id",
                        Render=customsButton,
                        Title="معلق | فعال کردن",
                        Visible=true,
                        Orderable=true,
                    },
                     new ()
                    {
                        AutoWidth = false,
                        Data="id",
                        Name="id",
                        Render=new RenderLink(new DataUrl("/EventManager/Index?monitorId=",UrlHelper),"رویداد","_blank","btn w-100 rounded-pill btn-secondary waves-effect waves-light"),
                        Title="رویداد ها",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="id",
                        Name="id",
                        Render=new RenderButtonOperation(
                        new RenderButtonEdit(new DataUrl("/MonitoringManagement/EditHttp/", UrlHelper),"ویــرایش"),
                        new RenderButtonDetails(new DataUrl("/MonitoringManagement/ResponsTimeDetails/", UrlHelper),"مشاهده سرکشی ها")),
                        Title="عملیات",
                        Visible=true,
                        Orderable=false,
                    }


                }
            };
            return table;
        }
        public static DataTableModel EventMonitoringDataTable()
        {

            var table = new DataTableModel
            {
                Name = "MonitoringHttpTables",
                ClassNames = "table border-top",
                UrlRead = new DataUrl("/EventManager/GetListEventPaging", UrlHelper),
                EnableExport = false,
                Dom = """
                dom: '<"card-header flex-column flex-md-row"<"dt-action-buttons text-end pt-3 pt-md-0"B>><"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
                """,

                ColumnCollection = new List<ColumnProperty>()
                {
                    new ()
                    {
                        AutoWidth = false,
                        Data="id",
                        Name="id",
                        Render=new RenderDefault(),
                        Title="شناسه",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="name",
                        Name="name",
                        Render=new RenderDefault(),
                        Title="عنوان",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="dateTime",
                        Name="dateTime",
                        Render=new RenderDefault(),
                        Title="تاریخ",
                        Visible=true,
                        Orderable=true,
                    },
                    new ()
                    {
                        AutoWidth = false,
                        Data="reason",
                        Name="reason",
                        Render=new RenderBoolean(),
                        Title="دلیل",
                        Visible=true,
                        Orderable=true,
                    },

                    new ()
                    {
                        AutoWidth = false,
                        Data="id",
                        Name="id",
                        Render=new RenderButtonOperation(
                        new RenderButtonEdit(new DataUrl("/MonitoringManagement/EditHttp/", UrlHelper),"ویــرایش"),
                        new RenderButtonDetails(new DataUrl("/MonitoringManagement/ResponsTimeDetails/", UrlHelper),"مشاهده سرکشی ها")),
                        Title="عملیات",
                        Visible=true,
                        Orderable=false,
                    }


                }
            };
            return table;
        }
    }
}
