﻿using DwapiCentral.Ct.Application.Interfaces;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs.Source
{
    public class SourceBag<T> : ISourceBag<T>
    {
        public string JobId { get; set; }
        public EmrSetup EmrSetup { get; set; }
        public UploadMode Mode { get; set; }
        public string DwapiVersion { get; set; }
        public int SiteCode { get; set; }
        public string Facility { get; set; }
        public Guid? ManifestId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid? FacilityId { get; set; }
        public string Tag { get; set; }
        public int MinPk { get; set; }
        public int MaxPk { get; set; }
        public List<T> Extracts { get; set; } = new List<T>();

        public bool HasJobId => !string.IsNullOrWhiteSpace(JobId);

        public virtual void SetFacility(List<FacilityCacheDto> facilityCacheDtos)
        {
            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : FacilityId.Value;
        }

        public string JobInfo()
        {
            string fac = Facility;
            dynamic e = Extracts.FirstOrDefault();

            if (null == e)
                return ToPrint();

            if (string.IsNullOrWhiteSpace(fac))
            {
                Type typeOfDynamic = e.GetType();
                bool exist = typeOfDynamic.GetProperties().Any(p => p.Name.Equals("FacilityName"));
                fac = exist ? e.FacilityName : Facility;
            }

            return $"Site:{e.SiteCode}-{fac} {typeof(T).Name.Replace($"{nameof(SourceDto)}", "")} [{Extracts.Count}]";
        }

        public string ToPrint()
        {
            return $"{SiteCode}-{Facility} [{Extracts.Count}]-[{MinPk}-{MaxPk}] {typeof(T).Name.Replace($"{nameof(SourceDto)}", "")}";
        }

        public override string ToString()
        {
            return JobInfo();
        }
    }
}
