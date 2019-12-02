using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;

namespace Website.ViewModels {
    public class RegistrationViewModel : IValidatableObject {

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

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

            user = null;
            user = uow.GetRepo<UserRepository>().GetByEmailAddress(EmailAddress);

            if (user != null) {
                yield return new ValidationResult("Email address is not valid", new[] { "EmailAddress" });
            }
        }
    }
}
