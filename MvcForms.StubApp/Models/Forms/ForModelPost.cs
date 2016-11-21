using System.ComponentModel.DataAnnotations;

namespace MvcForms.StubApp.Models.Forms
{
    public class ForModelPost
    {
        [MinLength(4, ErrorMessage = "validationErrorMessage")]
        public string BasicValue { get; set; }
    }
}