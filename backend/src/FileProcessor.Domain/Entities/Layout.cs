namespace FileProcessor.Domain.Entities
{
    public class Layout
    {
        public Guid Id { get; set; }
        public string AcquirerName { get; set; }
        public char Identifier { get; set; }
        public List<LayoutField> Fields { get; set; } = new();
    }
}
