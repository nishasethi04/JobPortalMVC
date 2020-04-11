using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {

        }
        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos); 
            base.Add(pocos);
        }
        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos); 
            base.Update(pocos);
        }
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyLocationPoco item in pocos)
            {
                if (string.IsNullOrEmpty(item.CountryCode))
                {
                    exceptions.Add(new ValidationException((int)Code.CountryCodeEmpty
                        , "Country codecan not be empty"));
                }
                if (string.IsNullOrEmpty(item.Province))
                {
                    exceptions.Add(new ValidationException((int)Code.ProvinceEmpty
                        , "Provience can not be empty"));
                }
                if (string.IsNullOrEmpty(item.Street))
                {
                    exceptions.Add(new ValidationException((int)Code.StreetEmpty
                        , "Street can not be empty"));
                }
                if (string.IsNullOrEmpty(item.City))
                {
                    exceptions.Add(new ValidationException((int)Code.CityEmpty
                        , "City can not be empty"));
                }
                if (string.IsNullOrEmpty(item.PostalCode))
                {
                    exceptions.Add(new ValidationException((int)Code.PostalCodeEmpty
                        , "PostalCode can not be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
               // throw new AggregateException(exceptions);
            }
        }
    }
}
