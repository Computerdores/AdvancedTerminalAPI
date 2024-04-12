using System.Collections.Generic;

namespace Computerdores.AdvancedTerminalAPI; 

public interface IAliasable {
    public IEnumerable<ICommand> GetAll(ITerminal term);
}