using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    enum Code
    {
        //SystemCountryCodeLogic
        CodeEmpty = 900,
        CountryNameEmpty = 901
    }
    public class SystemCountryCodeLogic
    {
        protected IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
            _repository = repository;
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        public List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public SystemCountryCodePoco Get(string code)
        {
            return _repository.GetSingle(c => c.Code == code);
        }

        private void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (SystemCountryCodePoco item in pocos)
            {
                if(string.IsNullOrEmpty(item.Code))
                {
                    exceptions.Add(new ValidationException((int)Code.CodeEmpty, "Code can not be empty."));
                }
                if (string.IsNullOrEmpty(item.Name))
                {
                    exceptions.Add(new ValidationException((int)Code.CountryNameEmpty, "Name can not be empty."));
                }
            }

            if (exceptions.Count > 0)
            {
               // throw new AggregateException(exceptions);
            }
        }
       
        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

    }

}
