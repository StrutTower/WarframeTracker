using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;

namespace Website.ViewModels {
    public class RegistrationViewModel : IValidatableObject {
        [Required, RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "This username contains invalid characters.")]
        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress), Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            UnitOfWork uow = (UnitOfWork)validationContext.GetService(typeof(UnitOfWork));

            if (Password != ConfirmPassword) {
                yield return new ValidationResult("Passwords do not match", new[] { "Password", "ConfirmPassword" });
            }

            User user = uow.GetRepo<UserRepository>().GetByUsername(Username);

            if (user != null) {
                yield return new ValidationResult("Username is not valid", new[] { "Username" });
            }

            User user2 = uow.GetRepo<UserRepository>().GetByEmailAddress(EmailAddress);

            if (user2 != null) {
                yield return new ValidationResult("Email address is not valid", new[] { "EmailAddress" });
            }
        }
    }
}
