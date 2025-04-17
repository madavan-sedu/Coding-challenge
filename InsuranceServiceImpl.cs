using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InsuranceManagement.Exceptions;
using InsuranceManagement.Utils;
using Insurance_Management_System.entities;

namespace InsuranceManagement.DAO
{
    public class InsuranceServiceImpl : IPolicyService
    {
        private readonly string _configFilePath = "appsettings.json";

        public bool CreatePolicy(Policy policy)
        {
            string query = "INSERT INTO Policies (PolicyId, PolicyName, PremiumAmount, Coverage) VALUES (@id, @name, @premium, @coverage)";
            using (SqlConnection connection = DBConnUtil.GetConnection(_configFilePath))
            {
                connection.Open();
                using SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", policy.PolicyId);
                cmd.Parameters.AddWithValue("@name", policy.PolicyName);
                cmd.Parameters.AddWithValue("@premium", policy.PremiumAmount);
                cmd.Parameters.AddWithValue("@coverage", policy.CoverageAmount);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Policy GetPolicy(int policyId)
        {
            string query = "SELECT * FROM Policies WHERE PolicyId = @id";
            using (SqlConnection connection = DBConnUtil.GetConnection(_configFilePath))
            {
                connection.Open();
                using SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", policyId);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Policy(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDouble(2),
                        reader.GetString(3)
                    );
                }

                throw new PolicyNotFoundException($"Policy with ID {policyId} not found.");
            }
        }

        public List<Policy> GetAllPolicies()
        {
            List<Policy> policies = new List<Policy>();
            string query = "SELECT * FROM Policies";
            using (SqlConnection connection = DBConnUtil.GetConnection(_configFilePath))
            {
                connection.Open();
                using SqlCommand cmd = new SqlCommand(query, connection);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    policies.Add(new Policy(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDouble(2),
                        reader.GetString(3)
                    ));
                }
            }

            return policies;
        }

        public bool UpdatePolicy(Policy policy)
        {
            string query = "UPDATE Policies SET PolicyName = @name, PremiumAmount = @premium, Coverage = @coverage WHERE PolicyId = @id";
            using (SqlConnection connection = DBConnUtil.GetConnection(_configFilePath))
            {
                connection.Open();
                using SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", policy.PolicyName);
                cmd.Parameters.AddWithValue("@premium", policy.PremiumAmount);
                cmd.Parameters.AddWithValue("@coverage", policy.CoverageAmount);
                cmd.Parameters.AddWithValue("@id", policy.PolicyId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeletePolicy(int policyId)
        {
            string query = "DELETE FROM Policies WHERE PolicyId = @id";
            using (SqlConnection connection = DBConnUtil.GetConnection(_configFilePath))
            {
                connection.Open();
                using SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", policyId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
