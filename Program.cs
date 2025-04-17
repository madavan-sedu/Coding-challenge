using System;
using Insurance_Management_System.entities;
using InsuranceManagement.DAO;
using InsuranceManagement;
using InsuranceManagement.Exceptions;

namespace InsuranceManagement.MainModule
{
    class Program
    {
        static void Main(string[] args)
        {
            IPolicyService service = new InsuranceServiceImpl();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n==== Insurance Management System ====");
                Console.WriteLine("1. Create Policy");
                Console.WriteLine("2. View Policy");
                Console.WriteLine("3. View All Policies");
                Console.WriteLine("4. Update Policy");
                Console.WriteLine("5. Delete Policy");
                Console.WriteLine("6. Exit");
                Console.Write("Choose option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Enter a number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter Policy ID: ");
                            int id = int.Parse(Console.ReadLine());
                            Console.Write("Enter Policy Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Premium Amount: ");
                            double premium = double.Parse(Console.ReadLine());
                            Console.Write("Enter Coverage: ");
                            string coverage = Console.ReadLine();

                            Policy newPolicy = new Policy(id, name, premium, coverage);
                            bool created = service.CreatePolicy(newPolicy);
                            Console.WriteLine(created ? "Policy created." : "Policy creation failed.");
                            break;

                        case 2:
                            Console.Write("Enter Policy ID: ");
                            int pid = int.Parse(Console.ReadLine());
                            Policy p = service.GetPolicy(pid);
                            Console.WriteLine(p);
                            break;

                        case 3:
                            var allPolicies = service.GetAllPolicies();
                            allPolicies.ForEach(Console.WriteLine);
                            break;

                        case 4:
                            Console.Write("Enter Policy ID to update: ");
                            int upid = int.Parse(Console.ReadLine());
                            Console.Write("Enter new name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Enter new premium: ");
                            double newPremium = double.Parse(Console.ReadLine());
                            Console.Write("Enter new coverage: ");
                            string newCoverage = Console.ReadLine();

                            Policy updated = new Policy(upid, newName, newPremium, newCoverage);
                            bool updatedFlag = service.UpdatePolicy(updated);
                            Console.WriteLine(updatedFlag ? "Policy updated." : "Update failed.");
                            break;

                        case 5:
                            Console.Write("Enter Policy ID to delete: ");
                            int delId = int.Parse(Console.ReadLine());
                            bool deleted = service.DeletePolicy(delId);
                            Console.WriteLine(deleted ? "Policy deleted." : "Delete failed.");
                            break;

                        case 6:
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (PolicyNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
            }
        }
    }
}