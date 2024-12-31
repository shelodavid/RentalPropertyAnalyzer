using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentalPropertyAnalyzer.Models.DBEntites
{
    public class RentalListings
    {
        public RentalListings()
        {
            StreetAddress = string.Empty; // Initialize with an empty string or a default value
            City= string.Empty;
            ZipCode= string.Empty;
            Zpid= string.Empty;
            State= string.Empty;
            PropertyType= string.Empty;
            Bathrooms= string.Empty;
            Bedrooms= string.Empty;
            ImgSrc= string.Empty;
            Latitude= string.Empty;
            Longitude= string.Empty;
        }

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {  get; set; }
		public string Zpid { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State {  get; set; }
		public string ZipCode { get; set; }
        public string PropertyType { get; set; }
        public string Bathrooms { get; set; }
        public string Bedrooms { get; set; }
        public string ImgSrc { get; set; }
        public double Price { get; set; }
        public double TaxAssessedValue { get; set; }
        public double EstimatedRent { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime AnalysisDate { get; set; }
        public string County { get; internal set; }
    }


}
