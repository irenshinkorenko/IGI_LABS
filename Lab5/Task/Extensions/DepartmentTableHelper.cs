using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Extensions
{
    public static class DepartmentTableHelper 
    {      
        public static HtmlString CreateDepartmentTable(this IHtmlHelper html, IEnumerable<Application.Controllers.SomeData> model)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<tbody>");
            foreach(var item in model)
            {
                result.Append( "<tr>\n");
                result.Append("<td>"+item.Data2+"</td>\n");
                result.Append("<td>" + item.Data3 + "</td>\n");
                result.Append("<td id=\"hiddenID\" style=\"display:none\">" + item.Data1 + "</td>\n");
                result.Append("<td><input id=\"check\" type=\"checkbox\" value=\"on\"></td>\n");
                result.Append("<td><input name=\"kek\" id=\"radio\" type=\"radio\" value=\"on\"></td>\n");
                result.Append("</tr>\n");              
            }
            result.Append("</tbody>");
            return new HtmlString(result.ToString());
        }        
    }
}
