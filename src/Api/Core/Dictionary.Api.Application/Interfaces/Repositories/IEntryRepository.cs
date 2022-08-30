using Dictionary.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Application.Interfaces.Repositories
{
    public interface IEntryRepository: IGenericRepository<Entry>
    {
    }
}
