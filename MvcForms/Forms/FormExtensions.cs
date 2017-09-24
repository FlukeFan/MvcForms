using System.Web.Mvc;

namespace MvcForms.Forms
{
    public static class FormExtensions
    {
        public static ScopedHtmlHelper<TPostModel> ForModelScope<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var newHelper = helper.ForModel(postModel);
            return new ScopedHtmlHelper<TPostModel>(newHelper);
        }

        public static HtmlHelper<TPostModel> ForModel<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var viewData = new ViewDataDictionary(helper.ViewData);
            viewData.Model = postModel;
            var data = new ViewDataContainer { ViewData = viewData };
            var newHelper = new HtmlHelper<TPostModel>(helper.ViewContext, data);
            return newHelper;
        }

        public static Form<TViewModel> Form<TViewModel>(this HtmlHelper<TViewModel> helper)
        {
            return helper.FormFor(helper.ViewData.Model);
        }

        public static Form<TPostModel> FormFor<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var newHelper = helper.ForModel(postModel);
            return new Form<TPostModel>(newHelper);
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}
