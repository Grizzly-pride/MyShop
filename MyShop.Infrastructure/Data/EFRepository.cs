using Microsoft.EntityFrameworkCore;
using MyShop.ApplicationCore.Interfaces;

namespace MyShop.Infrastructure.Data
{
	public class EFRepository<T> : IRepository<T> where T : class
	{
		private readonly CatalogContext _dbContext;

		public EFRepository(CatalogContext dbContext) => _dbContext = dbContext;

		public List<T> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public T? GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
