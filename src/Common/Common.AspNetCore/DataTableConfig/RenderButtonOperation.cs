using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AspNetCore.DataTableConfig
{
    public class RenderButtonOperation : IRender
    {
        #region Constructor
        public RenderButtonOperation(RenderButtonRemove remove, RenderButtonEdit edit, RenderButtonDetails details,
           string className)
        {
            ClassName = className;
            Remove = remove;
            Edit = edit;
            Details = details;

        }
        public RenderButtonOperation(RenderButtonCustome custome, RenderButtonEdit edit, RenderButtonDetails details,
         string className)
        {
            ClassName = className;
            Custome = custome;
            Edit = edit;
            Details = details;

        }
        public RenderButtonOperation(RenderButtonCustome custome, RenderButtonRemove remove, RenderButtonEdit edit, RenderButtonDetails details,
            string className)
        {
            ClassName = className;
            Remove = remove;
            Edit = edit;
            Details = details;
            Custome = custome;
        }
        public RenderButtonOperation(RenderButtonRemove remove, RenderButtonEdit edit, RenderButtonDetails details
        )
        {

            Remove = remove;
            Edit = edit;
            Details = details;

        }
        public RenderButtonOperation(RenderButtonRemove remove, RenderButtonEdit edit, RenderButtonDetails details,
            string className, RenderButtonCustome custome)
        {
            ClassName = className;
            Remove = remove;
            Edit = edit;
            Details = details;
            Custome = custome;
        }
        public RenderButtonOperation(RenderButtonRemove remove, RenderButtonEdit edit, RenderButtonDetails details
            , RenderButtonCustome custome)
        {


            Remove = remove;
            Edit = edit;
            Details = details;
            Custome = custome;
        }

        public RenderButtonOperation(RenderButtonCustome renderButtonCustome, RenderButtonEdit renderButtonEdit, RenderButtonDetails renderButtonDetails)
        {
            Custome = renderButtonCustome;
            Edit = renderButtonEdit;
            Details = renderButtonDetails;
        }

        public RenderButtonOperation(RenderButtonEdit renderButtonEdit, RenderButtonDetails renderButtonDetails)
        {
            Edit = renderButtonEdit;
            Details = renderButtonDetails;
        }



        #endregion


        public RenderButtonRemove Remove { get; set; }
        public RenderButtonEdit Edit { get; set; }
        public RenderButtonDetails Details { get; set; }
        public RenderButtonCustome Custome { get; set; }
        /// <summary>
        /// Gets or sets button class name
        /// </summary>
        public string ClassName { get; set; }
      
    }
}
