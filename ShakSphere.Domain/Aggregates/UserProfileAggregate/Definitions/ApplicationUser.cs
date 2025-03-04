using System;
using System.Collections.Generic;

namespace ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions
{
    public class ApplicationUser
    {
        public Guid AppUserId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo? BasicInfo { get; private set; }
        public DateTime? DateOfCreation { get; private set; }
        public DateTime? LastModified { get; private set; }

        private readonly List<ApplicationUser> _friends = new();

        public IReadOnlyList<ApplicationUser> Friends => _friends.AsReadOnly();

        private ApplicationUser()
        {
            _friends = new List<ApplicationUser>();
        }

        public static ApplicationUser CreateUserProfile(string id, BasicInfo? basicInfo)
        {
            return new ApplicationUser
            {
                AppUserId = Guid.NewGuid(),
                IdentityId = id,
                BasicInfo = basicInfo,
                DateOfCreation = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }

        public void AddFriend(ApplicationUser friend)
        {
            if (friend == null) throw new ArgumentNullException(nameof(friend));
            if (_friends.Contains(friend))
                throw new InvalidOperationException("Korisnik je već prijatelj.");

            _friends.Add(friend);
            LastModified = DateTime.Now;
        }

        public void UpdateBasicInfo(BasicInfo newBasicInfo)
        {
            BasicInfo = newBasicInfo;
            LastModified = DateTime.Now;
        }

        public void RemoveFriend(ApplicationUser friend)
        {
            if (friend == null) throw new ArgumentNullException(nameof(friend));
            if (!_friends.Contains(friend))
                throw new InvalidOperationException("Korisnik nije prijatelj.");

            _friends.Remove(friend);
            LastModified = DateTime.Now;
            friend.LastModified = DateTime.Now;
        }
    }
}