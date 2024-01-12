using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagement.Entity
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime DateFiled { get; set; }
        public double ClaimAmount { get; set; }
        public string Status { get; set; }
        public Policy AssociatedPolicy { get; set; }
        public Client AssociatedClient { get; set; }
    }
}
