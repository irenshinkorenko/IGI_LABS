using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace Lab5.Extensions
{
    public class NumericTagHelper : TagHelper
    {
        public string Page { get; set; }        
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.SetAttribute("type", "submit");
            output.Attributes.SetAttribute("value", Page);
            output.Attributes.SetAttribute("class", "pageButtons");
            //output.Attributes.SetAttribute("onClick", "GetPage(this)");
            output.Attributes.SetAttribute("id", "pager");            
        }
        // доделать tag-helper
    }
}
