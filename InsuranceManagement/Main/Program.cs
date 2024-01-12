using InsuranceManagement.DAO;
using InsuranceManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceManagement.Entity;

namespace InsuranceManagement.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = DBPropertyUtil.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Invalid connection string. Exiting...");
                return;
            }
            IPolicyService policyService = new PolicyServiceImpl(connectionString);

            while (true)
            {
                Console.WriteLine("Insurance Management System");
                Console.WriteLine("1. Create Policy");
                Console.WriteLine("2. Get Policy by ID");
                Console.WriteLine("3. Get All Policies");
                Console.WriteLine("4. Update Policy");
                Console.WriteLine("5. Delete Policy");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreatePolicy(policyService);
                        break;
                    case "2":
                        GetPolicyById(policyService);
                        break;
                    case "3":
                        GetAllPolicies(policyService);
                        break;
                    case "4":
                        UpdatePolicy(policyService);
                        break;
                    case "5":
                        DeletePolicy(policyService);
                        break;
                    case "6":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }

                Console.WriteLine();
            }

        }

        private static void CreatePolicy(IPolicyService policyService)
        {
            Console.WriteLine("Creating Policy...");
            Console.Write("Enter Policy Id: ");
            int policyId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Policy Number: ");
            string policyNumber = Console.ReadLine();

            Console.Write("Enter Coverage Details: ");
            string coverageDetails = Console.ReadLine();

           
            Policy newPolicy = new Policy
            {
                PolicyId = policyId,
                PolicyNumber = policyNumber,
                CoverageDetails = coverageDetails
            };

            bool result = policyService.CreatePolicy(newPolicy);

            if (result)
            {
                Console.WriteLine("Policy created successfully!");
            }
            else
            {
                Console.WriteLine("Error creating policy.");
            }
        }
        private static void GetPolicyById(IPolicyService policyService)
        {
            Console.WriteLine("Fetching Policy by ID...");
            Console.Write("Enter Policy ID: ");
            int policyId;
            if (int.TryParse(Console.ReadLine(), out policyId))
            { 
                try
                {
                    Policy policy = policyService.GetPolicy(policyId);

                    if (policy != null)
                    {
                        Console.WriteLine($"Policy details:Found");
                    }
                    else
                    {
                        Console.WriteLine($"Policy with ID {policyId} not found.");
                    }
                }
                catch(SystemException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Policy ID. Please enter a valid integer.");
            }
        }
        private static void GetAllPolicies(IPolicyService policyService)
        {
            Console.WriteLine("Fetching All Policies...");
            IEnumerable<Policy> policies = policyService.GetAllPolicies();

            if (policies != null)
            {
                Console.WriteLine("All Policies:");

                foreach (var policy in policies)
                {
                    Console.WriteLine(policy);
                }
            }
            else
            {
                Console.WriteLine("Error fetching policies.");
            }
        }
        private static void UpdatePolicy(IPolicyService policyService)
        {
            Console.WriteLine("Updating Policy...");

            // Input Policy ID
            Console.Write("Enter Policy ID: ");
            int policyId;
            if (int.TryParse(Console.ReadLine(), out policyId))
            {
                // Call the service method to get policy by ID
                try
                {
                    Policy existingPolicy = policyService.GetPolicy(policyId);

                    if (existingPolicy != null)
                    {
                        
                        Console.Write("Enter new Policy Number: ");
                        string newPolicyNumber = Console.ReadLine();

                        Console.Write("Enter new Coverage Details: ");
                        string newCoverageDetails = Console.ReadLine();

                        
                        existingPolicy.PolicyNumber = newPolicyNumber;
                        existingPolicy.CoverageDetails = newCoverageDetails;

                        
                        bool result = policyService.UpdatePolicy(existingPolicy);

                        if (result)
                        {
                            Console.WriteLine("Policy updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Error updating policy.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Policy with ID {policyId} not found.");
                    }
                }
                catch (SystemException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Policy ID. Please enter a valid integer.");
            }
        }
        private static void DeletePolicy(IPolicyService policyService)
        {
            Console.WriteLine("Deleting Policy...");

            
            Console.Write("Enter Policy ID: ");
            int policyId;
            if (int.TryParse(Console.ReadLine(), out policyId))
            {
                
                try
                {
                    bool result = policyService.DeletePolicy(policyId);

                    if (result)
                    {
                        Console.WriteLine("Policy deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Error deleting policy with ID {policyId}.");
                    }
                }
                catch (SystemException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Policy ID. Please enter a valid integer.");
            }
        }

    }
}
