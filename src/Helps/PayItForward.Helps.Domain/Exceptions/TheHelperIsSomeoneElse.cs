using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class TheHelperIsSomeoneElse(Helper helper)
    : DomainException($"Given helper with id {helper.Id} is not an helper for the active help.")
{
}