using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CareerCloud.BusinessLogicLayer
{
    public abstract class BaseLogic<TPoco>
         where TPoco : IPoco
    {
        public enum Code
        {
            //ApplicantEducationLogic
            MajorEmptyOrLessThan3 = 107,
            StartDateGreaterThanToday = 108,
            CompletionDateCannotBeEarlierThanStartDate = 109,
            //ApplicantJobApplicationLogic
            ApplicationDateCannotBeGreaterThanToday = 110,
            //ApplicantProfileLogic
            CurrentSalaryNegative = 111,
            CurrentRateNegative = 112,
            //ApplicantResumeLogic
            ResumeEmpty = 113,
            //ApplicantSkillLogic
            StartMonthCannotGreaterthan12 = 101,
            EndMonthCannotGreaterthan12 = 102,
            StartYearCannotLessthan1990 = 103,
            EndYearCannotBeLessThenStartYear = 104,
            //ApplicantWorkHistoryLogic
            CompanyNameMustBeGraterThan2Character = 105,
            //CompanyDescriptionLogic
            CompanyDescriptionMustBeGraterThan2Char = 107,
            CompanyNameMustBeGraterThan2Char = 106,
            //CompanyJobEducationLogic
            MajorAtLeast2Char = 200,
            ImportancelessThan0 = 201,
            //CompanyJobDescriptionLogic
            JobNameEmpty = 300,
            JobDescriptionsEmpty = 301,
            //CompanyJobSkillLogic
            ImportanceCannotBeLessThan0 = 400,
            //CompanyLocationLogic
            CountryCodeEmpty = 500,
            ProvinceEmpty = 501,
            StreetEmpty = 502,
            CityEmpty = 503,
            PostalCodeEmpty = 504,
            //SecurityLoginLogic
            PasswordLength10orGreater = 700,
            PasswordSpecialChar = 701,
            //CompanyProfileLogic
            CompanyWebsiteFormat = 600,
            ContactPhoneFormat = 601,
            //SecurityLoginLogic
            PasswordMustBe10ORGreater = 700,
            PasswordCointainoneOfFollowing = 701,
            //SecurityLoginLogic
            PhoneNumberEmpty = 702,
            PhoneNumberPattern = 703,
            EmailAddressValidation = 704,
            FullNameEmpty = 705,
            //SecurityRoleLogic
            RoleEmpty=800
            
           
        }

        protected IDataRepository<TPoco> _repository;
        public BaseLogic(IDataRepository<TPoco> repository)
        {
            _repository = repository;
        }

        protected virtual void Verify(TPoco[] pocos)
        {
            return;
        }

        public virtual TPoco Get(Guid id)
        {
            return _repository.GetSingle(c => c.Id == id);
        }

        public virtual List<TPoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public virtual void Add(TPoco[] pocos)
        {
            foreach (TPoco poco in pocos)
            {
                if (poco.Id == Guid.Empty)
                {
                    poco.Id = Guid.NewGuid();
                }
            }

            _repository.Add(pocos);
        }

        public virtual void Update(TPoco[] pocos)
        {
            _repository.Update(pocos);
        }

        public void Delete(TPoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}