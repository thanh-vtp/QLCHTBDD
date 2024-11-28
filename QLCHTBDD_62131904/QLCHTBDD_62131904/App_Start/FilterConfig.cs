using System.Web;
using System.Web.Mvc;

namespace QLCHTBDD_62131904
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
