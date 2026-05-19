namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

public sealed record ErrorResponse(IReadOnlyList<ApiError> Errors);

public sealed record ApiError(string Code, string Message);
