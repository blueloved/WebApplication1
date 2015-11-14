using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WebApplication1.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.Active == true);
        }

	    public IQueryable<Product> All(bool IsGetAll)
	    {
            if (IsGetAll)
	        {
	            return base.All();
	        }
	        else
            {
                return this.All();
            }
	    }

	    public IQueryable<Product> GetTheFirst10()
        {
            return this.All().Where(p => p.ProductId < 10);
        }

	    internal Product GetByID(int? id)
	    {
            return this.All().FirstOrDefault(p => p.ProductId == id.Value);
	    }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}