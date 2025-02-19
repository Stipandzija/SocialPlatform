namespace ShakSphere.Application.Contracts.UserProfile.Request
{
    public record CreateUserProfileRequestDTO
    {
        //TO DO: nadopunit request
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CurrentCity { get; set; }
    }
}
