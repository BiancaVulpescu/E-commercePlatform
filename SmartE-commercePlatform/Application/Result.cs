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
    public bool IsErr { get { return !isOk; } }
    public bool IsOkAnd(Func<T, bool> predicate)
    {
        return isOk && predicate(value!);
    }
    public bool IsErrAnd(Func<E, bool> predicate)
    {
        return !isOk && predicate(error!);
    }
    public Result<U, E> Map<U>(Func<T, U> map)
    {
        return isOk ? new Result<U, E>(true, map(value!), default) : new Result<U, E>(false, default, error!);
    }
    public U MapOr<U>(U defaultValue, Func<T, U> mapSuccess)
    {
        return isOk ? mapSuccess(value!) : defaultValue;
    }
    public U MapOrElse<U>(Func<T, U> mapSuccess, Func<E, U> mapFailure)
    {
        return isOk ? mapSuccess(value!) : mapFailure(error!);
    }
    public Result<T, F> MapErr<F>(Func<E, F> mapErr)
    {
        return !isOk ? new Result<T, F>(false, default, mapErr(error!)) : new Result<T, F>(true, value!, default);
    }
    public Result<T, E> Inspect(Action<T> inspect)
    {
        if (isOk) 
        {
            inspect(value!);
        }
        return this;
    }
    public Result<T, E> InspectErr(Action<E> inspectErr)
    {
        if (!isOk)
        {
            inspectErr(error!);
        }
        return this;
    }
    public T Unwrap()
    {
        if (!isOk)
        {
            throw new InvalidCastException("Result is not ok!");
        }
        return value!;
    }
    public T? UnwrapOrDefault() 
    {
        return isOk ? value! : default;
    }
    public T? UnwrapOr(T? defaultValue)
    {
        return isOk ? value! : defaultValue;
    }
    public T? UnwrapOrElse(Func<E, T?> op)
    {
        return isOk ? value! : op(error!);
    }
    public Result<U, E> And<U>(Result<U, E> res)
    {
        return isOk ? res : new Result<U, E>(false, default, error!);
    }
    public Result<U, E> AndThen<U>(Func<T, Result<U, E>> op)
    {
        return isOk ? op(value!) : new Result<U, E>(false, default, error!);
    }
    public Result<T, F> Or<F>(Result<T, F> res)
    {
        return !isOk ? res : new Result<T, F>(true, value!, default);
    }
    public Result<T, F> OrElse<F>(Func<E, Result<T, F>> op)
    {
        return !isOk ? op(error!) : new Result<T, F>(true, value!, default);
    }
    public E UnwrapErr()
    {
        if (isOk)
        {
            throw new InvalidCastException("Result is not err!");
        }
        return error!;
    }
}