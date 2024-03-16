using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Implementations.Exceptions;

namespace PayItForward.Helps.Application.Exceptions;

public class TheNeedyCannotIncurDebt : AppException
{
    public TheNeedyCannotIncurDebt(Needy needy) 
        : base($"The needy with id {needy.Id} cannot incur more debt.")
    {
    }
}