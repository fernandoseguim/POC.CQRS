namespace POC.CQRS.Shared.Commands
{
	public enum StatusCodeResult
	{
		Success = 200,
		BadRequest = 400,
		NotFound = 404,
		Conflict = 409,
		InternalServerError = 500
	}
}
