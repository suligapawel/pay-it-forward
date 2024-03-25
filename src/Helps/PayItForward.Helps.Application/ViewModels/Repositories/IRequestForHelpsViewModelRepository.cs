namespace PayItForward.Helps.Application.ViewModels.Repositories;

public interface IRequestForHelpsViewModelRepository
{
    Task<IReadOnlyCollection<RequestForHelpViewModel>> Get(CancellationToken cancellationToken); // TODO: Pagination, Sorting
    Task<RequestForHelpViewModel> GetDetails(Guid id, CancellationToken cancellationToken);
    Task Add(RequestForHelpViewModel requestForHelp, CancellationToken cancellationToken);
}