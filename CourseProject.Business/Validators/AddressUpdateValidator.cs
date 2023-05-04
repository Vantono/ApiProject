using FluentValidation;
using CourseProject.Common.DTOs.Address;
using CourseProject.Common.DTOs;

namespace CourseProject.Business.Validators;

public class AddressUpdateValidator : AbstractValidator <AddressUpdate>
{
    public AddressUpdateValidator()
    {
        RuleFor(addressUpdate => addressUpdate.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(addressUpdate => addressUpdate.City).NotEmpty().MaximumLength(100);
        RuleFor(addressUpdate => addressUpdate.Zipcode).NotEmpty().MaximumLength(16);
        RuleFor(addressUpdate => addressUpdate.Phone).MaximumLength(10);
    }
}
