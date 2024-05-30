"use strict";$((function(){var a=$(".datatables-ajax"),t=$(".dt-column-search"),e=$(".dt-advanced-search"),l=$(".dt-responsive"),n=$(".start_date"),o=$(".end_date"),s=$(".flatpickr-range");s.length&&s.flatpickr({mode:"range",dateFormat:"m/d/Y",locale:{format:"MM/DD/YYYY"},locale:"fa",orientation:isRtl?"auto right":"auto left",onClose:function(a,t,e){var l;new Date,null!=a[0]&&(l=moment(a[0]).format("MM/DD/YYYY"),n.val(l)),null!=a[1]&&(l=moment(a[1]).format("MM/DD/YYYY"),o.val(l)),$(s).trigger("change").trigger("keyup")}}),$.fn.dataTableExt.afnFiltering.length=0;var r,d=function(a){return(a=new Date(a)).getFullYear()+""+("0"+(a.getMonth()+1)).slice(-2)+("0"+a.getDate()).slice(-2)};a.length&&a.dataTable({processing:!0,ajax:assetsPath+"json/ajax.php",language:{sLengthMenu:" _MENU_",infoEmpty:"دیتایی برای نمایش وجود ندارد",sZeroRecords:"دیتایی برای نمایش وجود ندارد",infoFiltered:" - فیلتر از بین  _MAX_ رکورد",info:"نمایش _START_ از _END_ از _TOTAL_ رکورد",paginate:{last:"اخرین",first:"اولین",next:"بعدی",previous:"قبلی"},search:"",searchPlaceholder:"جستجوی "},dom:'<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>><"table-responsive"t><"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>'}),t.length&&($(".dt-column-search thead tr").clone(!0).appendTo(".dt-column-search thead"),$(".dt-column-search thead tr:eq(1) th").each((function(a){var t=$(this).text();$(this).html('<input type="text" class="form-control" placeholder="جستوجو'+t+'" />'),$("input",this).on("keyup change",(function(){r.column(a).search()!==this.value&&r.column(a).search(this.value).draw()}))})),r=t.DataTable({ajax:assetsPath+"json/table-datatable.json",columns:[{data:"full_name"},{data:"email"},{data:"post"},{data:"city"},{data:"start_date"},{data:"salary"}],orderCellsTop:!0,language:{sLengthMenu:" _MENU_",infoEmpty:"دیتایی برای نمایش وجود ندارد",sZeroRecords:"دیتایی برای نمایش وجود ندارد",infoFiltered:" - فیلتر از بین  _MAX_ رکورد",info:"نمایش _START_ از _END_ از _TOTAL_ رکورد",paginate:{last:"اخرین",first:"اولین",next:"بعدی",previous:"قبلی"},search:"",searchPlaceholder:"جستجوی "},dom:'<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>><"table-responsive"t><"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>'})),e.length&&e.DataTable({dom:"<'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-6'i><'col-sm-12 col-md-6 dataTables_pager'p>>",ajax:assetsPath+"json/table-datatable.json",columns:[{data:""},{data:"full_name"},{data:"email"},{data:"post"},{data:"city"},{data:"start_date"},{data:"salary"}],columnDefs:[{className:"control",orderable:!1,targets:0,render:function(a,t,e,l){return""}}],orderCellsTop:!0,language:{sLengthMenu:" _MENU_",infoEmpty:"دیتایی برای نمایش وجود ندارد",sZeroRecords:"دیتایی برای نمایش وجود ندارد",infoFiltered:" - فیلتر از بین  _MAX_ رکورد",info:"نمایش _START_ از _END_ از _TOTAL_ رکورد",paginate:{last:"اخرین",first:"اولین",next:"بعدی",previous:"قبلی"},search:"",searchPlaceholder:"جستجوی "},responsive:{details:{display:$.fn.dataTable.Responsive.display.modal({header:function(a){return"جزئیات "+a.data().full_name}}),type:"column",renderer:function(a,t,e){return e=$.map(e,(function(a,t){return""!==a.title?'<tr data-dt-row="'+a.rowIndex+'" data-dt-column="'+a.columnIndex+'"><td>'+a.title+":</td> <td>"+a.data+"</td></tr>":""})).join(""),!!e&&$('<table class="table"/><tbody />').append(e)}}}}),$("input.dt-input").on("keyup",(function(){!function(a,t){var l,s,r,c,i;5==a?(l=n.val(),s=o.val(),""!==l&&""!==s&&($.fn.dataTableExt.afnFiltering.length=0,e.dataTable().fnDraw(),r=a,c=l,i=s,$.fn.dataTableExt.afnFiltering.push((function(a,t,e){t=d(t[r]);var l=d(c),n=d(i);return l<=t&&t<=n||l<=t&&""===n&&""!==l||t<=n&&""===l&&""!==n}))),e.dataTable().fnDraw()):e.DataTable().column(a).search(t,!1,!0).draw()}($(this).attr("data-column"),$(this).val())})),l.length&&l.DataTable({ajax:assetsPath+"json/table-datatable.json",columns:[{data:""},{data:"full_name"},{data:"email"},{data:"post"},{data:"city"},{data:"start_date"},{data:"salary"},{data:"age"},{data:"experience"},{data:"status"}],columnDefs:[{className:"control",orderable:!1,targets:0,searchable:!1,render:function(a,t,e,l){return""}},{targets:-1,render:function(a,t,e,l){var n={1:{title:"کنونی",class:"bg-label-primary"},2:{title:"حرفه ای",class:" bg-label-success"},3:{title:"رد شده",class:" bg-label-danger"},4:{title:"استعفا داده",class:" bg-label-warning"},5:{title:"درخواست داده",class:" bg-label-info"}};return void 0===n[e=e.status]?a:'<span class="badge rounded-pill '+n[e].class+'">'+n[e].title+"</span>"}}],destroy:!0,language:{sLengthMenu:" _MENU_",infoEmpty:"دیتایی برای نمایش وجود ندارد",sZeroRecords:"دیتایی برای نمایش وجود ندارد",infoFiltered:" - فیلتر از بین  _MAX_ رکورد",info:"نمایش _START_ از _END_ از _TOTAL_ رکورد",paginate:{last:"اخرین",first:"اولین",next:"بعدی",previous:"قبلی"},search:"",searchPlaceholder:"جستجوی "},dom:'<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',responsive:{details:{display:$.fn.dataTable.Responsive.display.modal({header:function(a){return"جزئیات "+a.data().full_name}}),type:"column",renderer:function(a,t,e){return e=$.map(e,(function(a,t){return""!==a.title?'<tr data-dt-row="'+a.rowIndex+'" data-dt-column="'+a.columnIndex+'"><td>'+a.title+":</td> <td>"+a.data+"</td></tr>":""})).join(""),!!e&&$('<table class="table"/><tbody />').append(e)}}}}),setTimeout((()=>{$(".dataTables_filter .form-control").removeClass("form-control-sm"),$(".dataTables_length .form-select").removeClass("form-select-sm")}),200)}));