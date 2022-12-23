using FastNotes.Api.DataTransferModels;
using FluentValidation;

namespace FastNotes.Api.Validators
{
    public class UserValidator: AbstractValidator<UserDto>
    {
        //RuleFor(transaction => transaction.Id).NotNull().NotEqual(0);
        public UserValidator()
        {
            RuleFor(login => login.Email).NotNull().NotEmpty().Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
        }
    }
}
