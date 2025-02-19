namespace ShakSphere.Domain.Aggregates.UserProfileAggregate
{
    public class BasicInfo
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Email { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string? CurrentCity { get; private set; }
        private BasicInfo() { }
        public static BasicInfo CreateBasicInfo(string firstName, string lastName, DateTime dateOfBirth, string currentCity, string email)
        {
            // TO DO, validation
            var basicInfo = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity,
                Email = email
            };

            return basicInfo;
        }
        public static BasicInfo UpdateBasicInfo(string firstName, string lastName, string currentCity,string email)
        {
            // TO DO, validation
            var basicInfo = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                CurrentCity = currentCity,
                Email = email
            };

            return basicInfo;
        }

    }
}
