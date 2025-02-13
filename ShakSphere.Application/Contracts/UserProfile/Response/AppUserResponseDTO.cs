﻿namespace ShakSphere.Application.Contracts.UserProfile.Response
{
    public record AppUserResponseDTO
    {
        public Guid AppUserId { get; set; }
        public BasicInfoResponseDTO BasicInfo { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime LastModified { get; set; }
    }
}
