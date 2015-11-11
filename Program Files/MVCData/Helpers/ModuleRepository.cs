using MVCCore.Helpers;
using MVCCore.Repositories;
using MVCModel.Helpers;
using MVCModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCData.Helpers
{   
    public class ModuleRepository : IModuleRepository
    {
        private readonly CommonTableEntities commonTableEntities;

        public ModuleRepository(CommonTableEntities commonTableEntities)
        {
            this.commonTableEntities = commonTableEntities;
            this.commonTableEntities.Configuration.ProxyCreationEnabled = false;
        }

        public IQueryable<Module> GetAllModules()
        {
            return this.commonTableEntities.Modules.Where(w => w.InActive == 0);
        }

        public Module GetModuleByID(int moduleID)
        {
            var module = this.commonTableEntities.Modules.SingleOrDefault(x => x.ModuleID == moduleID);
            return module;
        }

  

        public void SaveChanges()
        {
            this.commonTableEntities.SaveChanges();
        }

        public void Add(Module module)
        {
            this.commonTableEntities.Modules.Add(module);
        }

        public void Delete(Module module)
        {
            this.commonTableEntities.Modules.Remove(module);
        }
    }
}
