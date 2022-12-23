using FastNotes.Mvc.Models;
using FluentValidation;

namespace FastNotes.Mvc.Validators
{
    public class LoginValidator: AbstractValidator<UserDto>
    {
        //RuleFor(transaction => transaction.Id).NotNull().NotEqual(0);
        public LoginValidator()
        {
            RuleFor(login => login.Email).NotNull().NotEmpty().Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
        }
    }
}
