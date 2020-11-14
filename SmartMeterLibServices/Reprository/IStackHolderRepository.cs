using SmartMeterLibServices.Model;
using System.Threading.Tasks;

namespace SmartMeterLibServices.Reprository
{
    public interface IStackHolderRepository
    {
        Task Add(StackholderInfoModel stackholderInfoModel);
    }
}
