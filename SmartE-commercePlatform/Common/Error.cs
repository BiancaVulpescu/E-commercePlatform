namespace Common;

public class Error
{
    private readonly string code;
    private readonly string? description;
    protected Error(string code, string? description = null)
    {
        this.code = code;
        this.description = description;
    }

    public string Code { get { return code; } }
    public string? Description { get { return description; } }

    public static readonly Error None = new(string.Empty);
    public override string ToString()
    {
        return $"Error {code} occured. Details: {description}";
    }
}