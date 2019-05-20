namespace MvcForms.Tests.Unit.Forms
{
    public class ExamplePostModel
    {
        public string   String      { get; set; }
        public string   String2     { get; set; }
        public long     Long        { get; set; }
        public int      Int         { get; set; }
        public int?     NullableInt { get; set; }

        public ExamplePostModel[] InputsArray;
    }
}
