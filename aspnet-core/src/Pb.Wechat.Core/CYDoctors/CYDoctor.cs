﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Pb.Wechat.CYDoctors
{
    public class CYDoctor : Entity<int>, IHasCreationTime
    {
        public string CYId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string LevelTitle { get; set; }
        public string Clinic { get; set; }
        public string ClinicNO { get; set; }
        public string Hospital { get; set; }
        public string HospitalGrand { get; set; }
        public string GoodAt { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
