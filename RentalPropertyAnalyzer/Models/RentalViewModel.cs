using System.ComponentModel;

namespace RentalPropertyAnalyzer.Models
{
    public class RentalViewModel
    {
        public RentalViewModel() 
        {
            
            Zpid=string.Empty;
            StreetAddress=string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty; 
            PropertyType = string.Empty;
            Bedrooms = string.Empty;
            ImgSrc = string.Empty;
            Latitude = string.Empty;
            Longitude = string.Empty;   
            Bathrooms = string.Empty;
            County=string.Empty ;
        }
        public int ID { get; set; }
        public string Zpid { get; set; }

        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        [DisplayName("Property Type")]
        public string PropertyType { get; set; }
        public string Bathrooms { get; set; }
        public string Bedrooms { get; set; }

        [DisplayName("Image Source")]
        public string ImgSrc { get; set; }
        public double Price { get; set; }

        [DisplayName("Tax Assessment Value")]
        public double TaxAssessedValue { get; set; }

        [DisplayName("Estimated Rent")]
        public double EstimatedRent { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [DisplayName("Analysis Date")]
        public DateTime AnalysisDate { get; set; }
        public string County { get; set; }
        public double RentalPriceRatio { get; set; }

        
    }
}
