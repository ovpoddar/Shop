using Shop.ViewModels;

namespace Shop.Managers
{
    public interface ICsvManager
    {
        UploadReport Upload(CsvViewModel csv);
        void Update(string csv);
    }
}
