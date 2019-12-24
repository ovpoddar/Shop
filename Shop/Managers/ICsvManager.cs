using Shop.ViewModels;

namespace Shop.Managers
{
    public interface ICsvManager
    {
        bool Upload(CsvViewModel csv);
    }
}
