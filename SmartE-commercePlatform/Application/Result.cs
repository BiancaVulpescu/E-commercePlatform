namespace Application;
public class Result<T, E>
{
    private readonly bool isOk;
    private readonly T? value;
    private readonly E? error;

    protected Result(bool isOk, T? value, E? error)
    {
        this.isOk = isOk;
        this.value = value;
        this.error = error;
    }
    public static Result<T, E> Ok(T value)
    {
        return new Result<T, E>(true, value, default);
    }
    public static Result<T, E> Err(E error)
    {
        return new Result<T, E>(false, default, error);
    }
    public bool IsOk { get { return isOk; } }
    public T Unwrap()
    {
        if (!isOk)
        {
            throw new InvalidCastException("Result is not ok!");
        }
        return value!;
    }
    public E UnwrapErr()
    {
        if (isOk)
        {
            throw new InvalidCastException("Result is not err!");
        }
        return error!;
    }
    public U MapOrElse<U>(Func<T, U> onSuccess, Func<E, U> onFailure)
    {
        return isOk ? onSuccess(value!) : onFailure(error!);
    }
}