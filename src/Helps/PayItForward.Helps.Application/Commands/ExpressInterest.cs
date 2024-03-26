using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Application.Commands;

public record ExpressInterest(RequestForHelpId RequestForHelpId, Helper Helper) : Command;