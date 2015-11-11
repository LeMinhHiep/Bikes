using RequireJsNet;

using MVCCore.Services;


namespace MVCClient.Controllers
{
    public abstract class BaseController : CoreController
    {
        private readonly IBaseService baseService;
        public BaseController(IBaseService baseService)
        { this.baseService = baseService;}


        public IBaseService BaseService { get { return this.baseService; } }


        
        public virtual void AddRequireJsOptions()
        {
            RequireJsOptions.Add("LocationID", this.baseService.LocationID, RequireJsOptionsScope.Page);
        }

    }
}