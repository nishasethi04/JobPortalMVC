using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {

        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantSkillPoco item in pocos)
            {
                if (item.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException((int)Code.StartMonthCannotGreaterthan12
                        , "StartMonth cannot be greater than 12"));
                }
                if (item.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException((int)Code.EndMonthCannotGreaterthan12, "EndMonth cannot be greater than 12"));
                }
                if (item.StartYear<1900)
                {
                    exceptions.Add(new ValidationException((int)Code.StartYearCannotLessthan1990, "StartYear Cannot Less than 1990 "));
                }
                if (item.EndYear <item.StartYear)
                {
                    exceptions.Add(new ValidationException((int)Code.EndYearCannotBeLessThenStartYear, "EndYear cannot be less then StartYear"));
                }
            }
            if (exceptions.Count > 0)
            {
                //throw new AggregateException(exceptions);
            }
        }
    }
}
