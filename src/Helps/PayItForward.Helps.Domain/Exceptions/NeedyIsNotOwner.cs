using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class NeedyIsNotOwner(Needy needy) 
    : DomainException($"The needy with id {needy.Id} is not an owner of the request for help.");