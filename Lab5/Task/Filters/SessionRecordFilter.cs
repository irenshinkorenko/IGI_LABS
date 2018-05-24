using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Filters
{
    public class SessionRecordFilter : Attribute, IActionFilter
    {    

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.Parameters[0].ParameterType.Name.ToLower() == "string")
            {
                if (context.ActionArguments.ContainsKey("Name"))
                {
                    switch (context.HttpContext.Request.Path)
                    {
                        case "/Home/DepartmentName":
                            context.HttpContext.Session.Set(key: "Name", value: context.ActionArguments["Name"]);
                            return;
                           
                        case "/Doctor/DoctorName":
                            context.HttpContext.Session.Set(key: "DoctorName", value: context.ActionArguments["Name"]);
                            return;
                        case "/Patient/PatientName":
                            context.HttpContext.Session.Set(key: "PatientName", value: context.ActionArguments["Name"]);
                            return;
                    }
                }
            }
            if (context.ActionDescriptor.Parameters[0].ParameterType.Name.ToLower() == "int32")
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    switch(context.HttpContext.Request.Path)
                    {
                        case "/Home/GetByName":
                            context.HttpContext.Session.Set(key: "OptionOne", value: context.ActionArguments["key"]);
                            return;
                        case "/Home/GetByQuantity":
                            context.HttpContext.Session.Set(key: "OptionTwo", value: context.ActionArguments["key"]);
                            return;

                        case "/Doctor/GetByName":
                            context.HttpContext.Session.Set(key: "DoctorOptionOne", value: context.ActionArguments["key"]);
                            return;
                        case "/Doctor/GetByDepartment":
                            context.HttpContext.Session.Set(key: "DoctorOptionTwo", value: context.ActionArguments["key"]);
                            return;

                        case "/Patient/GetByName":
                            context.HttpContext.Session.Set(key: "PatientOptionOne", value: context.ActionArguments["key"]);
                            return;
                        case "/Patient/GetByDoctor":
                            context.HttpContext.Session.Set(key: "PatientOptionTwo", value: context.ActionArguments["key"]);
                            return;
                    }

                    
                }

            }
        }
    }
}
