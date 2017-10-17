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
        public string StringInput1 { get; set; }
    }
}