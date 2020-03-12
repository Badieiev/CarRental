using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
	// https://dotnettutorials.net/lesson/generic-repository-pattern-csharp-mvc/
	public class Repository<T> : IDisposable where T : class
	{
		private bool disposed = false;
		private DbContext context = null;

		protected DbSet<T> DbSet
		{
			get; set;
		}

		public Repository(DbContext context)
		{
			this.context = context;
			DbSet = context.Set<T>();
		}
		public List<T> GetAll()
		{
			return DbSet.ToList();
		}

		public T Get(int id)
		{
			return DbSet.Find(id);
		}

		public void Add(T entity)
		{
			DbSet.Add(entity);
		}

		public virtual void Update(T entity)
		{
			context.Entry<T>(entity).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			DbSet.Remove(DbSet.Find(id));
		}

		public void SaveChanges()
		{
			context.SaveChanges();
		}

		public void Dispose()
		{
			if (!disposed)
			{
				context.Dispose();
				disposed = true;
			}
		}
	}
}