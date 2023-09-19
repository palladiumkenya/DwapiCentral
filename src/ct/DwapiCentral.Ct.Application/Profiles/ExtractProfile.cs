using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Application.Profiles
{
    public abstract class ExtractProfile<T> : IExtractProfile<T>
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public PatientExtract PatientInfo { get; set; }

        public List<T> Extracts { get; set; }

        public virtual bool IsValid()
        {
            if (HasData())
                return
                    Facility.IsValid();

            return false;
        }

        public virtual bool HasData()
        {
            return null != Facility && null != Demographic;
        }

        public virtual void GenerateRecords(string recordUUID)
        {
            PatientInfo.RecordUUID = recordUUID;
            Extracts = new List<T>();
        }
    }
}