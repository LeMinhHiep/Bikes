﻿using MVCClient.Api.SessionTasks;
using MVCClient.Converters;
using MVCClient.ViewModels.Menus;
using MVCCore.Helpers;
using MVCModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Api.Menus
{
    //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class MenuApiController : Controller
    {
        private readonly IModuleRepository moduleRepository;
        private readonly IModuleDetailRepository moduleDetailRepository;
        private readonly IConverter<ModuleViewModel, Module> moduleViewModelConverter;
        private readonly IConverter<ModuleDetailViewModel, ModuleDetail> moduleDetailViewModelConverter;

        public MenuApiController(IModuleRepository moduleRepository, 
                IModuleDetailRepository moduleDetailRepository,
                IConverter<ModuleViewModel, Module> moduleViewModelConverter,
                IConverter<ModuleDetailViewModel, ModuleDetail> moduleDetailViewModelConverter
            )
        {
            this.moduleRepository = moduleRepository;
            this.moduleDetailRepository = moduleDetailRepository;
            this.moduleViewModelConverter = moduleViewModelConverter;
            this.moduleDetailViewModelConverter = moduleDetailViewModelConverter;
        }

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskMenu(int? moduleID)
        {            
            if (moduleID == null)
            {
                moduleID = MenuSession.GetModuleID(this.HttpContext);                   
            }
            else
            {
                MenuSession.SetModuleID(this.HttpContext, (int)moduleID);                
            }

           
            int taskID = MenuSession.GetTaskID(this.HttpContext);
           

            ViewBag.TaskID = taskID;
         

            //var moduleDetail = moduleDetailRepository.GetModuleDetailByID((int)moduleID);
            var moduleDetail = moduleDetailRepository.GetAllModuleDetails().ToList().Where(w => w.ModuleID == moduleID && w.InActive == 0).OrderBy(o => o.SerialID);
            return PartialView(moduleDetail);
        }

        [ChildActionOnly]
        //[OutputCache(NoStore = true, Location = OutputCacheLocation.Server, Duration = 100)]
        public ActionResult MainMenu()
        {
            string moduleName = MenuSession.GetModuleName(this.HttpContext);
            string taskName = MenuSession.GetTaskName(this.HttpContext);
            string taskController = MenuSession.GetTaskController(this.HttpContext);
            ViewBag.ModuleName = moduleName;
            ViewBag.TaskName = taskName;
            ViewBag.TaskController = taskController;
           
            var moduleMaster = moduleRepository.GetAllModules();
             
            return PartialView(moduleMaster);
        }

        public ActionResult SetTask(int? taskID, string taskName, string taskController)
        {            
            if (taskID == null)
            {
                return Json(new { Success = 0 });
            }

            int moduleID = MenuSession.GetModuleID(this.HttpContext); 
            Module module = moduleRepository.GetModuleByID((int)moduleID);

            MenuSession.SetModuleName(this.HttpContext, module.Description);

            MenuSession.SetTaskID(this.HttpContext, (int)taskID);
            MenuSession.SetTaskName(this.HttpContext, taskName);
            MenuSession.SetTaskController(this.HttpContext, taskController);

            return Json(new { Success = 1 });
        }     
    }
}