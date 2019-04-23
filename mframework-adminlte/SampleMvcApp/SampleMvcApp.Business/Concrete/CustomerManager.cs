using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Core.DataAccess;
using SampleMvcApp.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace SampleMvcApp.Business.Concrete
{
    public class CustomerManager : EntityManager<Customer>
    {
        public CustomerManager(IEntityManager<Customer> customerManager) :base(customerManager)
        {

        }

        public override List<Customer> List()
        {
            // Customer spesific rules..

            return base.List();
        }

        public void Check()
        {
            throw new NotImplementedException();
        }
    }
}
