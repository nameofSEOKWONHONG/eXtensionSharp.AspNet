using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eXtensionSharp.AspNet
{
    public static class ControllerContextExtensions
    {
        public static string vGetControllerName(this ControllerContext context)
        {
            if (context.xIsEmpty()) return default;
            return context.ActionDescriptor.ControllerName;
        }

        public static string vGetControllerFullName(this ControllerContext context)
        {
            if (context.xIsEmpty()) return default;
            return context.ActionDescriptor.ControllerTypeInfo.FullName;
        }

        public static string vGetActionName(this ControllerContext context)
        {
            if (context.xIsEmpty()) return default;
            return context.ActionDescriptor.ActionName;
        }

        public static string vGetActionFullName(this ControllerContext context)
        {
            if (context.xIsEmpty()) return default;
            return context.ActionDescriptor.MethodInfo.Name;
        }
    }
}
