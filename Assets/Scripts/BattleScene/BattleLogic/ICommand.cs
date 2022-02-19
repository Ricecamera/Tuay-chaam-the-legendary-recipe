using System.Collections;
using System.Collections.Generic;
using System;

namespace BattleScene.BattleLogic {
    // Interface of all commands that can be put into CommandHandlers
    public interface ICommand {
        void Execute(List<PakRender> diedThisTurn);
    }
}
