using FluentValidation;
using ProcessPayment.Domain.DTO;
using System;
using System.Text.RegularExpressions;

namespace ProcessPayment.API.Validators
{
    public class CreatePaymentRequestValidator : AbstractValidator<PaymentRequestDto>
    {
        public CreatePaymentRequestValidator()
        {
            RuleFor(c => c.CreditCardNumber)
                .NotEmpty()
                .CreditCard();
            RuleFor(c => c.CardHolder)
                .NotEmpty();
            RuleFor(c => c.ExpirationDate)
                .Must(BeAValidDate).WithMessage("Payment process has expired");
            RuleFor(c => c.SecurityCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 3).WithMessage("SecurityCode must be 3 characters long")
                .Must(BeANumber).WithMessage("Only digits are allowed for a SecurityCode");
            RuleFor(c => c.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(0);
        }
        private static bool BeAValidDate(DateTime dateTime)
        {
            return DateTime.Compare(DateTime.Now, dateTime) == -1;
        }

        private static bool BeANumber(string securityCode)
        {
            return Regex.Match(securityCode, "\\d+").Length == securityCode.Length;

        }
    }
}
