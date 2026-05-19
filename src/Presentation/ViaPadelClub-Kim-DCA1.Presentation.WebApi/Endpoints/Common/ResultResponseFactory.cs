using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

public static class ResultResponseFactory
{
    public static ErrorResponse FromErrors(IEnumerable<Error> errors)
    {
        return new ErrorResponse(errors
            .Select(error => new ApiError(error.Code, error.Message))
            .ToList());
    }
}
