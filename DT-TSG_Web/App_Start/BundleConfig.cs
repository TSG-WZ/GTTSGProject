using System.Web;
using System.Web.Optimization;

namespace DTTSG_Web
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            //登录首页css
            bundles.Add(new StyleBundle("~/Login_Css").Include(
                      "~/lib/layui-v2.5.5/css/layui.css",
                      "~/Content/Own/css/login.css"));

            /// 登录后首页css
            bundles.Add(new StyleBundle("~/Index_Css").Include(
                      "~/Content/Own/css/index.css"));
            bundles.Add(new StyleBundle("~/Default_Css").Include(
                      "~/Content/Own/css/default.css"));

            //图书首页css
            bundles.Add(new StyleBundle("~/BookList_Css").Include(
                        "~/lib/layui-v2.5.5/css/layui.css",
                      "~/Content/Own/css/booklist.css"));

            //列表css
            bundles.Add(new StyleBundle("~/List_Css").Include(
                     "~/lib/layui-v2.5.5/css/layui.css",
                     "~/css/public.css"));

            //图书管理首页css
            bundles.Add(new StyleBundle("~/libDefault_Css").Include(
                      "~/Content/Own/css/libdefault.css"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。

            //登录首页js
            bundles.Add(new ScriptBundle("~/Login_Js").Include(
                        "~/lib/jquery-3.4.1/jquery-3.4.1.min.js",
                        "~/lib/layui-v2.5.5/layui.js",
                        "~/lib/jq-module/jquery.particleground.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Content/Own/js/login.js"));
            //列表
            bundles.Add(new ScriptBundle("~/ListInfo_Js").Include(
                        "~/lib/jquery-3.4.1/jquery-3.4.1.min.js",
                        "~/lib/layui-v2.5.5/layui.js",
                        "~/Content/Own/js/date_format.js"));
            //图书列表页js
            bundles.Add(new ScriptBundle("~/BookList_Js").Include(
                       "~/Content/Own/js/booklist.js"));
            //图书编辑页js
            bundles.Add(new ScriptBundle("~/List_Js").Include(
                       "~/lib/layui-v2.5.5/layui.js",
                       "~/Content/Own/js/date_format.js"));
        }
    }
}
