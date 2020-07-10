using NPOI.SS.Formula.Functions;
using SqlSugar;

namespace project_manage_api.Model
{
    [SugarTable("role_module")]
    public class RoleModule
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }

        public RoleModule()
        {
            Id = 0;
            RoleId = 0;
            ModuleId = 0;
        }
    }
}