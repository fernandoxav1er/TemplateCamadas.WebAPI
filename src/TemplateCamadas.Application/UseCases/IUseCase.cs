namespace TemplateCamadas.Application.UseCases;

public interface IUseCase<TReturn, TParameters>
{
    Task<TReturn> Process(TParameters parameters);
}

public interface IUseCase<TParameters>
{
    Task Process(TParameters parameters);
}

public interface IUseCase
{
    Task Process();
}
