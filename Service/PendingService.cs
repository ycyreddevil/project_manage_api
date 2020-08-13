using Microsoft.AspNetCore.Http;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.RequestModel;
using SqlSugar;

namespace project_manage_api.Service
{
    public class PendingService : SugarDBContext<TaskRecord>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheContext _cacheContext;
        private UserAuthSession user;
        
        public PendingService(IHttpContextAccessor httpContextAccessor, ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheContext = cacheContext;

            string token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            user = _cacheContext.Get<UserAuthSession>(token);
        }

        /// <summary>
        /// 获取待我审批
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string getToBeApprovedList(QueryPendingRequest request)
        {
            var query = Db.Queryable<TaskRecord, ApproveApprover>((tr, aa) => new object[]
            {
                JoinType.Left, tr.Id == aa.DocId
            });
            return null;
        }
        
        /// <summary>
        /// 获取我已审批
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string getHaveApprovedList(QueryPendingRequest request)
        {
            return null;
        }
        
        /// <summary>
        /// 获取我已提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string getMySubmitList(QueryPendingRequest request)
        {
            return null;
        }
    }
}