﻿namespace MvcForms.StubApp.Models.Examples
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
        public string StringInput2 { get; set; }

        public FormInputsModel[] InputsArray;
    }
}