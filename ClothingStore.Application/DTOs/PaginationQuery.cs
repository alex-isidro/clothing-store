namespace ClothingStore.Application.DTOs;

public sealed class PaginationQuery
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;

    public int Page { get; set; } = DefaultPage;
    public int PageSize { get; set; } = DefaultPageSize;

    public bool TryGetError(out string? message)
    {
        if (Page < 1)
        {
            message = "page deve ser maior ou igual a 1.";
            return true;
        }

        if (PageSize is < 1 or > MaxPageSize)
        {
            message = $"pageSize deve estar entre 1 e {MaxPageSize}.";
            return true;
        }

        message = null;
        return false;
    }
}
