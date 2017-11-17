using Microsoft.AspNetCore.Mvc;

public class HomeController
{
	public IActionResult Index()
	{
		return new OkResult("rr");
	}
}