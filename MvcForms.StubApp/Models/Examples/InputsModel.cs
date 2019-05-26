using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcForms.StubApp.Models.Examples
{
    public class InputsModel
    {
        public InputsModel()
        {
            PostModel = new InputsModelPost();
        }

        public InputsModelPost PostModel;

        public IDictionary<string, string> StringOptions = new Dictionary<string, string>
        {
            { "Key1", "Value 1" },
            { "Key2", "Value 2" },
            { "Key3", "Value 3" },
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
    }

    public static class InputsModelExtensions
    {
        public static IEnumerable<KeyValuePair<TKey, string>> Optional<TKey>(this IEnumerable<KeyValuePair<TKey, string>> options)
        {
            return options.Prepend(KeyValuePair.Create(default(TKey), "<please select>"));
        }
    }
}