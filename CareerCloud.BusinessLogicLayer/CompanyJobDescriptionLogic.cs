using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{ 
   public class CompanyJobDescriptionLogic:BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
        {

        }

        public override void Add(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyJobDescriptionPoco item in pocos)
            {
                if (string.IsNullOrEmpty(item.JobName))
                {
                    exceptions.Add(new ValidationException((int)Code.JobNameEmpty
                        , "Job Name can not be empty"));
                }
                if(string.IsNullOrEmpty(item.JobDescriptions))
                {
                    exceptions.Add(new ValidationException((int)Code.JobDescriptionsEmpty
                        , "Job Descriptions  can not be empty"));
                } 
            }

            if (exceptions.Count > 0)
            {
                //throw new AggregateException(exceptions);
            }
        }
    }
}
