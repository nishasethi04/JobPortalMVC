using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {

        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            string[] requiredextenction = new string[] { ".ca", ".com", ".biz"};
            foreach (CompanyProfilePoco item in pocos)
            {
                if (item.CompanyWebsite != null && !requiredextenction.Any(t => item.CompanyWebsite.Contains(t)))
                {
                    exceptions.Add(new ValidationException((int)Code.CompanyWebsiteFormat
                        , "Incorrect CompanyWebsite Format"));
                }

                if ((item.ContactPhone == null)|| (item.ContactPhone!=null && !Regex.IsMatch(item.ContactPhone, @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", RegexOptions.IgnoreCase)))
                {
                    exceptions.Add(new ValidationException((int)Code.ContactPhoneFormat
                        , "Incorrect ContactPhoneFormat"));
                }
            }

            if (exceptions.Count > 0)
            {
              //  throw new AggregateException(exceptions);
            }

        }
    }
}
