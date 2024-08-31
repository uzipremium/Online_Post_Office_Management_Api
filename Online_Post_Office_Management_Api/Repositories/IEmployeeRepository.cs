namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Get();

        Task<Employee> GetById(string id); 

        Task Create(Employee employee);

        Task<bool> Update(string id, Employee employee);  

        Task<bool> Delete(string id);  

    }
}
