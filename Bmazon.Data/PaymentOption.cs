using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Data
{
    public class PaymentOption
    {
        public int ID { get; set; }

        public string Owner { get; set; }

        public string HolderName { get; set; }

        public string CardNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-yy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset ExpDate { get; set; }
    }
}
