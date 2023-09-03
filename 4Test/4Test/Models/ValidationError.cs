namespace Models
{
	public class ValidationError
	{
		public List<string> Errors {get; set; }

		public ValidationError()
		{
			Errors = new List <string>();
		}

	}
}
