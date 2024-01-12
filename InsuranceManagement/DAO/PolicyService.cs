using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InsuranceManagement.Entity;
using InsuranceManagement.Exception;


namespace InsuranceManagement.DAO
{
    public class PolicyServiceImpl : IPolicyService
    {
        private readonly string connectionString;

        public PolicyServiceImpl(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool CreatePolicy(Policy policy)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Policy (policyId,policyNumber, coverageDetails) VALUES (@policyId,@policyNumber, @coverageDetails)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@policyId", policy.PolicyId);

                        command.Parameters.AddWithValue("@PolicyNumber", policy.PolicyNumber);

                        command.Parameters.AddWithValue("@CoverageDetails", policy.CoverageDetails);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"Rows affected: {rowsAffected}");

                        return rowsAffected > 0;
                    }
                }
                    throw new PolicyNotFoundException($"Policy with ID  not found");
                
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error creating policy: {ex.Message}");
                return false;
            }
        }
        public Policy GetPolicy(int policyId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Policy WHERE policyId = @policyId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@policyId", policyId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Policy
                                {
                                    PolicyId = reader.GetInt32(0),
                                    PolicyNumber = reader.GetString(1),
                                    CoverageDetails = reader.GetString(2)
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (SystemException ex)
            {
                Console.WriteLine($"Error getting policy: {ex.Message}");
                return null;
            }
        }
        public IEnumerable<Policy> GetAllPolicies()
        {
            try
            {
                List<Policy> policies = new List<Policy>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Policy";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                policies.Add(new Policy
                                {
                                    policyId = reader.GetInt32(0),
                                    policyNumber = reader.GetString(1),
                                    coverageDetails = reader.GetString(2)
                                });
                            }
                        }
                    }
                }

                return policies;
            }
            catch ( SystemException ex)
            {
                Console.WriteLine($"Error getting all policies: {ex.Message}");
                return null;
            }
        }



        public bool UpdatePolicy(Policy policy)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Policy SET policyNumber = @policyNumber, coverageDetails = @coverageDetails WHERE policyId = @policyId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@policyId", policy.PolicyId);
                        command.Parameters.AddWithValue("@policyNumber", policy.PolicyNumber);
                        command.Parameters.AddWithValue("@coverageDetails", policy.CoverageDetails);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"Rows affected: {rowsAffected}");

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine($"Error updating policy: {ex.Message}");
                return false;
            }
        }
        public bool DeletePolicy(int policyId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Policy WHERE policyId = @policyId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@policyId", policyId);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"Rows affected: {rowsAffected}");

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine($"Error deleting policy: {ex.Message}");
                return false;
            }
        }
    }
}

