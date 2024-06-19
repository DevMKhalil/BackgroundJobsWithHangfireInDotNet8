namespace BackgroundJobsWithHangfireInDotNet8.Services
{
    public interface IServiceManagement
    {
        void SendEmail();
        void Updatedatabase();
        void GenerateMerchandise();
        void SyncData();
    }
}
