using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        enum Code
        {
            //SystemLanguageCodeLogic
            LanguageIDEmpty = 1000,
            LanguageNameEmpty = 1001,
            NativeNameEmpty = 1002
        }
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }


        public List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public SystemLanguageCodePoco Get(string languageID)
        {
            return _repository.GetSingle(c => c.LanguageID == languageID);
        }

        private void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (SystemLanguageCodePoco item in pocos)
            {
                if (string.IsNullOrEmpty(item.LanguageID))
                {
                    exceptions.Add(new ValidationException((int)Code.LanguageIDEmpty, "LanguageID can not be empty."));
                }
                if (string.IsNullOrEmpty(item.Name))
                {
                    exceptions.Add(new ValidationException((int)Code.LanguageNameEmpty, "LanguageName can not be empty."));
                }
                if (string.IsNullOrEmpty(item.NativeName))
                {
                    exceptions.Add(new ValidationException((int)Code.NativeNameEmpty, "NativeName can not be empty."));
                }
            }

            if (exceptions.Count > 0)
            {
             //   throw new AggregateException(exceptions);
            }
        }

        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}
