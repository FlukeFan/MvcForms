using System.Web.Mvc;

namespace MvcForms
{
    public static class FormExtensions
    {
        public static HtmlHelper<TPostModel> ForModel<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var viewData = new ViewDataDictionary(helper.ViewData);
            viewData.Model = postModel;
            var data = new ViewDataContainer { ViewData = viewData };
            var newHelper = new HtmlHelper<TPostModel>(helper.ViewContext, data);
            return newHelper;
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}
