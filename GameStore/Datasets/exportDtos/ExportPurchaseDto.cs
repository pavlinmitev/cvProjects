using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models.enumers;

namespace VaporStore.Datasets.exportDtos
{
    [XmlType("Purchase")]
    public class ExportPurchaseDto
    {
        public string Card { get; set; }

        public string Cvc { get; set; }

        public string Date { get; set; }

        public PurchaseGameDto Game { get; set; }
    }
}
