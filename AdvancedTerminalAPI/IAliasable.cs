using System.Collections.Generic;

namespace Computerdores; 

public interface IAliasable {
    public IEnumerable<ICommand> GetAll(ITerminal term);
}