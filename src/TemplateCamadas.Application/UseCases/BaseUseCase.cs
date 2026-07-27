using TemplateCamadas.Domain.Interfaces;
using TemplateCamadas.Domain.Services;

namespace TemplateCamadas.Application.UseCases;

public abstract class BaseUseCase<TReturn, TParameters> : ValidationService, IUseCase<TReturn, TParameters>
{
    protected BaseUseCase(INotificationService notificationService) : base(notificationService) { }

    public abstract Task<TReturn> Process(TParameters parameters);
}

public abstract class BaseUseCase<TParameters> : ValidationService, IUseCase<TParameters>
{
    protected BaseUseCase(INotificationService notificationService) : base(notificationService) { }

    public abstract Task Process(TParameters parameters);
}

public abstract class BaseUseCase : ValidationService, IUseCase
{
    protected BaseUseCase(INotificationService notificationService) : base(notificationService) { }

    public abstract Task Process();
}
