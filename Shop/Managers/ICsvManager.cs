using Shop.Models;
using Shop.ViewModels;

namespace Shop.Managers
{
    public interface ICsvManager
    {
        UploadModel Upload(CsvViewModel csv);
        void Update(string csv);
    }
}
