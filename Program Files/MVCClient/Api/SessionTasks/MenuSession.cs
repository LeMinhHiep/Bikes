using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClient.Api.SessionTasks
{
    public class MenuSession
    {
        public static int GetModuleID(HttpContextBase context)
        {
            if (context.Session["ModuleID"] == null)
                return 0;
            else
                return (int)context.Session["ModuleID"];
        }

        public static void SetModuleID(HttpContextBase context, int moduleID)
        {
            context.Session["ModuleID"] = moduleID;
        }

        public static int GetTaskID(HttpContextBase context)
        {
            if (context.Session["TaskID"] == null)
                context.Session["TaskID"] = 0;

            return (int)context.Session["TaskID"];
        }

        public static void SetTaskID(HttpContextBase context, int taskID)
        {
            context.Session["TaskID"] = taskID;
        }

        public static string GetModuleName(HttpContextBase context)
        {
            if (context.Session["ModuleName"] == null)
                context.Session["ModuleName"] = "";

            return (string)context.Session["ModuleName"];
        }

        public static void SetModuleName(HttpContextBase context, string moduleName)
        {
            if (!string.IsNullOrWhiteSpace(moduleName))
            {
                context.Session["ModuleName"] = moduleName;
            }
            else
                context.Session["ModuleName"] = "";
        }

        public static string GetTaskName(HttpContextBase context)
        {
            if (context.Session["TaskName"] == null)
                context.Session["TaskName"] = "";

            return (string)context.Session["TaskName"];
        }

        public static void SetTaskName(HttpContextBase context, string taskName)
        {
            if (!string.IsNullOrWhiteSpace(taskName))
            {
                //context.Session["TaskName"] = "\\ " + taskName;
                context.Session["TaskName"] = taskName;
            }
            else
                context.Session["TaskName"] = "";
        }

        public static string GetTaskController(HttpContextBase context)
        {
            if (context.Session["TaskController"] == null)
                context.Session["TaskController"] = "";

            return (string)context.Session["TaskController"];
        }

        public static void SetTaskController(HttpContextBase context, string taskController)
        {
            if (!string.IsNullOrWhiteSpace(taskController))
            {
                context.Session["TaskController"] =  taskController;
            }
            else
                context.Session["TaskController"] = "";
        }
    }
}