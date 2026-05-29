namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

public static class ExceptionResponseFactory
{
    public static string GetDetail(Exception exception)
    {
        Exception rootCause = exception.GetBaseException();

        if (rootCause == exception)
            return exception.Message;

        return $"{exception.Message} Root cause: {rootCause.Message}";
    }
}
