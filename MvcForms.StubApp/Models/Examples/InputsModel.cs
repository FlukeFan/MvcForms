using System;
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

        public int          Int                 { get; set; }
        public int?         NullableInt         { get; set; }

        public long         Long                { get; set; }
        public long?        NullableLong        { get; set; }

        public bool         Bool                { get; set; }
        public bool?        NullableBool        { get; set; }

        public EnumValues   Enum                { get; set; }
        public EnumValues?  NullableEnum        { get; set; }

        public DateTime     DateTime            { get; set; }
        public DateTime?    NullableDateTime    { get; set; }
    }
}