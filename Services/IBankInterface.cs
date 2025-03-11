using ApiBrasil.Models;
using ApiBrasil.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBrasil.Services
{
    public interface IBankInterface
    {
        Task<GenericResponse<IEnumerable<Banks>>> GetAllBanks();
        Task<GenericResponse<Banks>> GetBankById(int id);
    }
}
