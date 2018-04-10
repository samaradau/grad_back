using DemoLab.Models.Validators;
using FluentValidation.Attributes;

namespace DemoLab.Models.Security
{
    /// <summary>
    /// Represents a model that can be used by a user for sending a request to reset his or her password.
    /// </summary>
    [Validator(typeof(ResetPasswordRequestValidator))]
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Gets or sets a user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a confirm password.
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
