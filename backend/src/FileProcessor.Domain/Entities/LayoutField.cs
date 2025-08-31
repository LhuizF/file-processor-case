namespace FileProcessor.Domain.Entities
{
    public class LayoutField
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int InitPosition { get; set; }
        public int Length { get; set; }
        public string DataType { get; set; }

        public Guid LayoutId { get; set; }
        public Layout Layout { get; set; }
    }
}
