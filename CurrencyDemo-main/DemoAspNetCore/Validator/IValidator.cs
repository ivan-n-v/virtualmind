using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAspNetCore.Validator
{
    public interface IValidator<T>
    {
        bool IsValid(T param);
        string GetMessage();
    }
}
