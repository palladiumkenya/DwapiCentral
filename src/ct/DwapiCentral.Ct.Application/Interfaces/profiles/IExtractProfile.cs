using System;
using System.Collections.Generic;

namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IExtractProfile<T> : IProfile
    {
        //Guid ProfileId { get; }
        List<T> Extracts { get; set; }
        bool IsValid();
        bool HasData();
       
    }
}
