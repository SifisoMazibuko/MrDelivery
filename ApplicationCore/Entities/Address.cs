using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Address : BaseEntity
    {
        public string StreetNo { get;  set; }

        public string BuildingType { get;  set; }

        public string UnitNo { get;  set; }

        public string ComplexName { get;  set; }
        
        //private Address() { }

        //public Address(string streetno, string buildingtype, string unitno, string complexname )
        //{
        //    StreetNo = streetno;
        //    BuildingType = buildingtype;
        //    UnitNo = unitno;
        //    ComplexName = complexname;
        //}
    }
}
