using ErrorOr;

namespace Identity.Errors
{
    public static class RepositoryErrors
    {
        public static Error EmailAlreadyExists => Error.Conflict(
            code: "User.EmailAlreadyExists",
            description: "This email is already registered with another account.");
        public static Error InvalidCredentials => Error.Validation(
            code: "User.InvalidCredentials",
            description: "The provided credentials are invalid.");
    }
}
