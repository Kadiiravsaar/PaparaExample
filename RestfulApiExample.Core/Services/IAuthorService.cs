using RestfulApiExample.Core.Models;

namespace RestfulApiExample.Core.Services
{
	public interface IAuthorService : IService<Author>
	{
		Task ConditionRemoveAuthor(int authorId);	
	}
}
