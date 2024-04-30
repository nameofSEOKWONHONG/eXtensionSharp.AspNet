using Microsoft.AspNetCore.Mvc;

namespace eXtensionSharp.AspNet
{
    public static class ControllerContextExtensions
    {
        public static string xGetControllerName(this ControllerContext context)
        {
            if (context.xIsEmpty()) return default;
            return context.ActionDescriptor.ControllerName;
        }

        public static string xGetControllerFullName(this ControllerContext context)
        {
            if (context.xIsEmpty()) return default;
            return context.ActionDescriptor.ControllerTypeInfo.FullName;
        }

        public static string xGetActionName(this ControllerContext context)
        {
            if (context.xIsEmpty()) return default;
            return context.ActionDescriptor.ActionName;
        }

        public static string xGetActionFullName(this ControllerContext context)
        {
            if (context.xIsEmpty()) return default;
            return context.ActionDescriptor.MethodInfo.Name;
        }
    }
}
