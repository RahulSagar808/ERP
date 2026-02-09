namespace ERP.ApplicationCore.Models
{
    public class UniversityModel
    {
        public string UniversityName { get; set; }
        public string ContactPerson { get; set; }
        public string OfficialPhone { get; set; }
        public string OfficialMobile { get; set; }
        public string OfficialEmail { get; set; }
        public string OfficialLogo { get; set; }
    }

    public class UniversityEditModel : UniversityModel
    {
        public string Id { get; set; }
    }
}
