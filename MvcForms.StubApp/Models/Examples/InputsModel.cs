using System.ComponentModel.DataAnnotations;

namespace MvcForms.StubApp.Models.Examples
{
    public class InputsModel
    {
        public InputsModel()
        {
            PostModel = new InputsModelPost();
        }

        public InputsModelPost PostModel;
    }

    public class InputsModelPost
    {
        public InputsModelPost()
        {
            InputTextString1 = "existing StringInput1 value";
        }

        public string       InputTextString1        { get; set; }

        [Required(ErrorMessage = "Please supply a value")]
        [MinLength(3, ErrorMessage = "Please supply more than 3 characters")]
        public string       InputTextString2        { get; set; }

        public int          InputNumberInt          { get; set; }
        public int          InputNumberNullableInt  { get; set; }
        public string       InputNumberString       { get; set; }

        public string       SelectString            { get; set; }
    }
}