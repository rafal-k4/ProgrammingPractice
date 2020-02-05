using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPractice.DAL.Entities
{
    [Table("Customers")]
    public class Customer
    {
        public Guid Id { get; set; }

        [Column("PA_NAME")]
        public string Lastname { get; set; }
        [Column("PA_NAMVOR")]
        public string FirstName { get; set; }
    }
}
