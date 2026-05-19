using Microsoft.AspNetCore.Mvc;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

public static class ApiEndpoint
{
    public static class WithRequest<TRequest>
    {
        public abstract class WithResponse<TResponse> : ControllerBase
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest request);
        }
    }

    public static class WithoutRequest
    {
        public abstract class WithResponse<TResponse> : ControllerBase
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync();
        }
    }
}
