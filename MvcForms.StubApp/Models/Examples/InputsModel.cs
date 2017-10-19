using System.ComponentModel.DataAnnotations;

namespace MvcForms.StubApp.Models.Examples
{
    public class InputsModel
    {
        public InputsModel()
        {
            PostModel = new FormInputsModel();
        }

        public FormInputsModel PostModel;
    }

    public class FormInputsModel
    {
        public FormInputsModel()
        {
            StringInput1 = "existing StringInput1 value";
        }

        public string StringInput1 { get; set; }

        [Required(ErrorMessage = "Please supply a value")]
        [MinLength(3, ErrorMessage = "Please supply more than 3 characters")]
        public string StringInput2 { get; set; }

        public FormInputsModel[] InputsArray;
    }
}