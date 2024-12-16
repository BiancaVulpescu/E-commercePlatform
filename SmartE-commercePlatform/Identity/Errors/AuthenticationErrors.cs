using ErrorOr;

namespace Identity.Errors
{
    public static class AuthenticationErrors
    {
        public static Error EmailAlreadyExists { get; } = Error.Conflict(
            code: "User.EmailAlreadyExists",
            description: "This email is already registered with another account.");
        public static Error InvalidCredentials { get; } = Error.Validation(
            code: "User.InvalidCredentials",
            description: "The provided credentials are invalid.");
        public static Error Cancelled { get; } = Error.Failure("User.OperationCancelled", "Operation was cancelled");
        public static Error Unknown(Exception ex) => Error.Failure(
            "User.Unknown",
            "Other error, see exception for cause",
            new Dictionary<string, object> { ["exception"] = ex });
    }
}
