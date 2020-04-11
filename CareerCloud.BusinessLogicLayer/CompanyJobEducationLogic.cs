using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
        {

        }

        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyJobEducationPoco item in pocos)
            {
                if (item.Major!=null && item.Major.ToCharArray().Length<2)
                {
                    exceptions.Add(new ValidationException((int)Code.MajorAtLeast2Char
                        , "Major must be at least 2 characters"));
                }
                if (item.Importance<1)
                {
                    exceptions.Add(new ValidationException((int)Code.ImportancelessThan0
                        , "Importance cannot be less than 0"));
                }
            }

            if (exceptions.Count > 0)
            {//
               // throw new AggregateException(exceptions);
            }
        }
    }
}
