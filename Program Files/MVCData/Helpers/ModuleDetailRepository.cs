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
    public class ModuleDetailRepository : IModuleDetailRepository
    {
        private readonly CommonTableEntities commonTableEntities;

        public ModuleDetailRepository(CommonTableEntities commonTableEntities)
        {
            this.commonTableEntities = commonTableEntities;
        }

        public IQueryable<ModuleDetail> GetAllModuleDetails()
        {
            return this.commonTableEntities.ModuleDetails;
        }

        public IQueryable<ModuleDetail> GetModuleDetailByModuleID(int moduleID)
        {
            return this.commonTableEntities.ModuleDetails.Where(x => x.ModuleID == moduleID && x.InActive == 0);
        }

        public ModuleDetail GetModuleDetailByID(int taskID)
        {
            return this.commonTableEntities.ModuleDetails.SingleOrDefault(x => x.TaskID == taskID);
        }

        public void AddModuleDetail(ModuleDetail moduleDetail)
        {
            this.commonTableEntities.ModuleDetails.Add(moduleDetail);
        }

        public void Add(ModuleDetail moduleDetail)
        {
            this.commonTableEntities.ModuleDetails.Add(moduleDetail);
        }

        public void Remove(ModuleDetail moduleDetail)
        {
            this.commonTableEntities.ModuleDetails.Remove(moduleDetail);
        }

        public void SaveChanges()
        {
            this.commonTableEntities.SaveChanges();
        }

    }
}
