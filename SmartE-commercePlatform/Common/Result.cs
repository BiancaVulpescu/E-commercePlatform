using System.Diagnostics.CodeAnalysis;

namespace Common;

[ExcludeFromCodeCoverage]
public sealed class Result<T, E>
{
    private readonly bool isOk;
    private readonly T? value;
    private readonly E? error;

    private Result(bool isOk, T? value, E? error)
    {
        this.isOk = isOk;
        this.value = value;
        this.error = error;
    }
    public static Result<T, E> Ok(T value) => new(true, value, default);
    public static Result<T, E> Err(E error) => new(false, default, error);
    public bool IsOk { get { return isOk; } }
    public bool IsErr { get { return !isOk; } }
    public bool IsOkAnd(Func<T, bool> predicate) => isOk && predicate(value!);
    public bool IsErrAnd(Func<E, bool> predicate) => !isOk && predicate(error!);
    public Result<U, E> Map<U>(Func<T, U> map) => new(true, isOk ? map(value!) : default, isOk ? default : error!);
    public U MapOr<U>(U defaultValue, Func<T, U> mapSuccess) => isOk ? mapSuccess(value!) : defaultValue;
    public U MapOrElse<U>(Func<T, U> mapSuccess, Func<E, U> mapFailure) => isOk ? mapSuccess(value!) : mapFailure(error!);
    public Result<T, F> MapErr<F>(Func<E, F> mapErr) => new(true, isOk ? value! : default, isOk ? default : mapErr(error!));
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
    public T? UnwrapOrDefault() => isOk ? value! : default;
    public T? UnwrapOr(T? defaultValue) => isOk ? value! : defaultValue;
    public T? UnwrapOrElse(Func<E, T?> op) => isOk ? value! : op(error!);
    public Result<U, E> And<U>(Result<U, E> res) => isOk ? res : new(false, default, error!);
    public Result<U, E> AndThen<U>(Func<T?, Result<U, E>> op) => isOk ? op(value!) : new(false, default, error!);
    public Result<T, F> Or<F>(Result<T, F> res) => isOk ? new(true, value!, default) : res;
    public Result<T, F> OrElse<F>(Func<E, Result<T, F>> op) => isOk ? new(true, value!, default) : op(error!);
    public E UnwrapErr()
    {
        if (isOk)
        {
            throw new InvalidCastException("Result is not err!");
        }
        return error!;
    }
}