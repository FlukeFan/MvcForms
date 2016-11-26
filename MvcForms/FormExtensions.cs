using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcForms.Controls;

namespace MvcForms
{
    public static class FormExtensions
    {
        public static DisposableHtmlHelper<TPostModel> ForModel<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var viewData = new ViewDataDictionary(helper.ViewData);
            viewData.Model = postModel;
            var data = new ViewDataContainer { ViewData = viewData };
            var newHelper = new DisposableHtmlHelper<TPostModel>(helper.ViewContext, data);
            return newHelper;
        }

        public static Form<TPostModel> FormFor<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            return helper.FormFor(postModel, helper.BeginForm());

        }

        public static Form<TPostModel> FormFor<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel, MvcForm form)
        {
            var html = helper.ForModel(postModel);
            return new Form<TPostModel>(html, form);
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}
