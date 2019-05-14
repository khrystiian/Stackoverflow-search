using Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IStackoverflowReader
    {
        IList<Items> InputRead(SearchInput userInput);
    }
}