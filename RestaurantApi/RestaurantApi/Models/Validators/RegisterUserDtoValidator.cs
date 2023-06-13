using FluentValidation;
using RestaurantAPI.Entities;
using System.Linq;

namespace RestaurantAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password); //sprawdzenie czy confirm jest równe password 

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUs = dbContext.Users.Any(r => r.Email == value);
                if (emailInUs)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            }); 
            
        }

    }
}
