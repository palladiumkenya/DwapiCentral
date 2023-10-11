using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
    public  interface IMotherBabyPair : IExtract
    {
        int BabyPatientPK { get; set; }
        int MotherPatientPK { get; set; }
        string? BabyPatientMncHeiID { get; set; }
        string? MotherPatientMncHeiID { get; set; }
        string? PatientIDCCC { get; set; }
        
    }
}
