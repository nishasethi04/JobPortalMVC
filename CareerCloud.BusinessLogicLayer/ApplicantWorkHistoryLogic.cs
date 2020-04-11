using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
        {

        }

        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantWorkHistoryPoco item in pocos)
            {
                if (item.CompanyName.ToCharArray().Length <= 2)
                {
                    exceptions.Add(new ValidationException((int)Code.CompanyNameMustBeGraterThan2Character
                        , "CompanyName must be grater than 2 character"));
                }
            }
            if (exceptions.Count > 0)
            {
               // throw new AggregateException(exceptions);
            }
        }
    }
}
