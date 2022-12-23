using FastNotes.Api.DataTransferModels;
using FluentValidation;

namespace FastNotes.Api.Validators
{
    public class LoginValidator: AbstractValidator<LoginDto>
    {
        //RuleFor(transaction => transaction.Id).NotNull().NotEqual(0);
        public LoginValidator()
        {
            RuleFor(login => login.Email).NotNull().NotEmpty().Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
        }
    }
}
