using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance_Management_System.entities
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public double PremiumAmount { get; set; }
        public string CoverageAmount { get; set; }

        public Policy() { }

        public Policy(int policyId, string policyName, double premiumAmount, string coverage)
        {
            PolicyId = policyId;
            PolicyName = policyName;
            PremiumAmount = premiumAmount;
            CoverageAmount = coverage;
        }

        public override string ToString()
        {
            return $"Policy [PolicyId={PolicyId}, PolicyName={PolicyName}, PremiumAmount={PremiumAmount}, Coverage={CoverageAmount}]";
        }
    }

}
