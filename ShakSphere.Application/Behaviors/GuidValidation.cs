using System.ComponentModel.DataAnnotations;

namespace ShakSphere.Application.Behaviors.Behaviors
{
    public class GuidValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object Id)
        {
            if (Id == null) 
                return false;
            return Guid.TryParse(Id.ToString(), out _);
        }
    }

}
