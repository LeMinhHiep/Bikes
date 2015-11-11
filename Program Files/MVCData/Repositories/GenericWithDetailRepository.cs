using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using MVCModel.Models;
using MVCCore.Repositories;



using MVCModel; //for Loading (09/07/2015) - let review and optimize Loading laster




namespace MVCData.Repositories
{
    public class GenericWithDetailRepository<TEntity, TEntityDetail> : GenericRepository<TEntity>, IGenericWithDetailRepository<TEntity, TEntityDetail>
        where TEntity : class, IAccessControlAttribute //IAccessControlAttribute: for Loading (09/07/2015) - let review and optimize Loading laster
        where TEntityDetail : class
    {
        private DbSet<TEntityDetail> modelDetailDbSet = null;

        public GenericWithDetailRepository(TotalBikePortalsEntities totalBikePortalsEntities)
            : this(totalBikePortalsEntities, null) { }

        public GenericWithDetailRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable)
            : this(totalBikePortalsEntities, functionNameEditable, null) { }

        public GenericWithDetailRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable, string functionNameDeletable)
            : this(totalBikePortalsEntities, functionNameEditable, functionNameDeletable, null) { }

        public GenericWithDetailRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable, string functionNameDeletable, string functionNameApprovable)
            : base(totalBikePortalsEntities, functionNameEditable, functionNameDeletable, functionNameApprovable)
        {
            modelDetailDbSet = this.TotalBikePortalsEntities.Set<TEntityDetail>();
        }


        public virtual TEntityDetail RemoveDetail(TEntityDetail entityDetail)
        {
            return this.modelDetailDbSet.Remove(entityDetail);
        }

        public virtual IEnumerable<TEntityDetail> RemoveRangeDetail(IEnumerable<TEntityDetail> entityDetails)
        {
            return this.modelDetailDbSet.RemoveRange(entityDetails);
        }
    }
}
