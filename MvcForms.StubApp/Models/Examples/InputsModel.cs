using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MvcForms.Forms;

namespace MvcForms.StubApp.Models.Examples
{
    public class InputsModel
    {
        public InputsModel()
        {
            PostModel = new InputsModelPost();
        }

        public InputsModelPost PostModel;

        public IEnumerable<Option> StringOptions = new []
        {
            Option.Value("Key1", "Value 1"),
            Option.Value("Key2", "Value 2"),
            Option.Value("Key3", "Value 3"),
        };

        public IEnumerable<Option> GroupOptions = new []
        {
            Option.Group("Group 1", new []
            {
                Option.Value("G1K1", "Key 1 (Group 1)"),
                Option.Value("G1K2", "Key 2 (Group 1)"),
            }),
            Option.Group("Group 2", new []
            {
                Option.Value("G2K1", "Key 1 (Group 2)"),
                Option.Value("G2K2", "Key 2 (Group 2)"),
            }),
        };
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

        [Required(ErrorMessage = "Please select an item")]
        public string       SelectString            { get; set; }

        [Required(ErrorMessage = "Please select an item")]
        public string       SelectGroup             { get; set; }

        public string       SelectSized             { get; set; }
    }
}