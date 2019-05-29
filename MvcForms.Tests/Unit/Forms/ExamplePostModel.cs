namespace MvcForms.Tests.Unit.Forms
{
    public class ExamplePostModel
    {
        public enum Values
        {
            Key1,
            Key2,
            Key3,
        }

        public string   String          { get; set; }
        public string   String2         { get; set; }
        public string[] Strings         { get; set; }
        public long     Long            { get; set; }
        public int      Int             { get; set; }
        public int?     NullableInt     { get; set; }
        public bool     Bool            { get; set; }
        public bool?    NullableBool    { get; set; }
        public Values   Enum            { get; set; }
        public Values?  NullableEnum    { get; set; }

        public ExamplePostModel[] InputsArray;
    }
}
