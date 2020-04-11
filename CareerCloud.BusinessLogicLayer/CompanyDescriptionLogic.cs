using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyDescriptionPoco item in pocos)
            {
                if (item.CompanyDescription!=null && item.CompanyDescription.ToCharArray().Length <= 2)
                {
                    exceptions.Add(new ValidationException((int)Code.CompanyDescriptionMustBeGraterThan2Char
                        , "Company Description must be greater than 2 characters"));
                }
                if (!string.IsNullOrEmpty(item.CompanyName)&& item.CompanyName.ToCharArray().Length<=2)
                {
                    exceptions.Add(new ValidationException((int)Code.CompanyNameMustBeGraterThan2Char
                       , "Company Name must be grater than 2 character"));
                }
            }
            if (exceptions.Count > 0)
            {
              //  throw new AggregateException(exceptions);
            }
        }
    }
}
