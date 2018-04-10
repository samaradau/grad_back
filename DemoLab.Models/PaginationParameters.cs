using DemoLab.Models.Validators;
using FluentValidation.Attributes;

namespace DemoLab.Models
{
    [Validator(typeof(PaginationParametersValidator))]
    public class PaginationParameters
    {
        public PaginationParameters()
        {
            Page = 1;
            Amount = 25;
        }

        public int Page { get; set; }

        public int Amount { get; set; }
    }
}
