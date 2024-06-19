namespace BackgroundJobsWithHangfireInDotNet8.Services
{
    public class ServiceManagement : IServiceManagement
    {
        public void GenerateMerchandise()
        {
            Console.WriteLine($"Generate Merchandise: Long Running Task {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void SendEmail()
        {
            Console.WriteLine($"Send Email: Short Running Task {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void SyncData()
        {
            Console.WriteLine($"Sync Data: Short Running Task {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void Updatedatabase()
        {
            Console.WriteLine($"Update Database: Long Running Task {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
