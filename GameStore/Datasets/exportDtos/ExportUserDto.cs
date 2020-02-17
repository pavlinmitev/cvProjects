using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Datasets.Dto;

namespace VaporStore.Datasets.exportDtos
{
   
	[XmlType("User")]
    public class ExportUserDto
    {
        [XmlAttribute("username")]
        public string UserName { get; set; }

        public ExportPurchaseDto[] Purchases { get; set; }

        public decimal TotalSpent { get; set; }
    }
}

