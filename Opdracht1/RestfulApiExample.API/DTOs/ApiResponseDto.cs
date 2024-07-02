namespace RestfulApiExample.API.DTOs
{
	public class ApiResponseDto<T>
	{
		public T Data { get; set; }
		public string Error { get; set; }
		public bool IsSuccess { get; set; }



		public ApiResponseDto(T data)
		{
			Data = data;
			Error = string.Empty;
			IsSuccess = true;
		}

		public ApiResponseDto(string message)
		{
			Error = message;
			IsSuccess = false;
		}

		public ApiResponseDto(T data, string message, bool isSuccess)
		{
			Data = data;
			Error = message;
			IsSuccess = isSuccess;
		}
	}
}
