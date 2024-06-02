using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers;

public class MainController : ControllerBase
{
    protected ILogger _logger;

    public MainController(ILogger logger)
    {
        _logger = logger;
    }

    public ILogger Logger => _logger;
}
