using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebGallery.Common.ViewModels.Interfaces
{
    public interface ILinesProcessableViewModel
    {
        Task ProcessLines(IEnumerable<string> lines);
        Task ProcessString(string str);
    }
}