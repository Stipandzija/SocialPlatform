namespace ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions
{
    public class ApplicationUser 
    {
        public Guid AppUserId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo? BasicInfo { get; private set; }
        public DateTime? DateOfCreation { get; private set; }
        public DateTime? LastModified { get; private set; }
        private ApplicationUser() { }
        public static ApplicationUser CreateUserProfile(string Id,BasicInfo? basicInfo)
        {
            // TO DO: validation
            var userProfile = new ApplicationUser
            {
                AppUserId = Guid.NewGuid(),
                IdentityId = Id,
                BasicInfo = basicInfo,
                DateOfCreation = DateTime.Now,
                LastModified = DateTime.Now
            };
            return userProfile;
        }
        public void UpdateUserBasicInfo(BasicInfo newBasicInfo)
        {
            BasicInfo = newBasicInfo;
            LastModified = DateTime.Now;
        }
    }
}
