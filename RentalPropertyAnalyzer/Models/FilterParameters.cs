namespace RentalPropertyAnalyzer.Models
{
    public class FilterParameters
    {
        public string? PropertyType { get; set; }
        public string? State { get; set; }
        public string? County { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string? SortOrder { get; set; }
    }
}
