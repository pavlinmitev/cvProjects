using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.Datasets.Dto
{
    [XmlType("Purchase")]
   public class PurchaseDto
    {
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("Key")]
        [RegularExpression("^[A-Z 0-9]{4}-[A-Z 0-9]{4}-[A-Z 0-9]{4}$")]
        public string Key { get; set; }
        [XmlElement("Card")]
        public string Card { get; set; }
        [XmlElement("Date")]
        public string Date { get; set; }
       
    }
}
