using SmartMeterLibServices.Model;
using System.Threading.Tasks;

namespace SmartMeterLibServices.Reprository
{
    public class StackHolderRepository : IStackHolderRepository
    {
        private readonly AppDbContext _context;

        public StackHolderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(StackholderInfoModel stackholderInfoModel)
        {
            _context.stackholderInfoModels.Add(stackholderInfoModel);
            await _context.SaveChangesAsync();
        }
    }
}
