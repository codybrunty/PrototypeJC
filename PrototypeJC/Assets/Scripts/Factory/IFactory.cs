public interface IFactory<TInput, TOutput> {
    TOutput Create(TInput input);
}