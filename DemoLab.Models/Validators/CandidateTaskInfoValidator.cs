using System.Linq;
using DemoLab.Models.ExerciseManagement;
using FluentValidation;

namespace DemoLab.Models.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="CandidateTaskInfo"/>.
    /// </summary>
    public class CandidateTaskInfoValidator : AbstractValidator<CandidateTaskInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateTaskInfoValidator"/> class.
        /// </summary>
        public CandidateTaskInfoValidator()
        {
            RuleFor(taskInfo => taskInfo.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Id must be greater than or equal to 1.");

            RuleFor(taskInfo => taskInfo.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(taskInfo => taskInfo.Subject)
                .NotEmpty()
                .WithMessage("Subject is required.");

            RuleFor(taskInfo => taskInfo.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(taskInfo => taskInfo.MaximumScore)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaximumScore must be greater than or equal to 0.");

            RuleFor(taskInfo => taskInfo.TimeSeconds)
                .GreaterThanOrEqualTo(5)
                .WithMessage("TimeSeconds must be greater than or equal to 5.");

            RuleFor(taskInfo => taskInfo.CodeTemplate)
                .NotEmpty()
                .WithMessage("CodeTemplate is required.");

            RuleFor(taskInfo => taskInfo.TestClassName)
                .NotEmpty()
                .WithMessage("TestClassName is required.");
        }
    }
}
