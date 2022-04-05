using System.Collections.Generic;

public interface Performable {

    public void Execute(List<PakRender> target, PakRender self);
}