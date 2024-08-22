
namespace Minesweeper_Sharp.Engine
{
    public enum Event 
    { 
        Mine_Boom, 
        Empty_Cell,
        Win_Game,
        Loose_Game,
        Update_Flags_Count
    };

    // The Mediator interface declares a method used by components to notify the
    // mediator about various events. The Mediator may react to these events and
    // pass the execution to other components.
    public interface IMediator
    {
        void Notify(object sender, Event ev);
    }
}
