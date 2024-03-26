using SyspotecDomain.Dtos.Generic;

namespace SyspotecDomain.Dtos.Contract
{
    public class ContractDto
    {
        public string Identifier { get; set; }  

        public CompanyDto Company { get; set; }

        public StateDto State { get; set; }

        public TypeFileDto TypeFile { get; set; }

        public string Name { get; set; }

        public string Descripcion { get; set; }

        public string Url { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
