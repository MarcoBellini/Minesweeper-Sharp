
namespace Minesweeper_Sharp.Engine
{
    // The Base Component provides the basic functionality of storing a
    // mediator's instance inside component objects.
    public class BaseComponent
    {
        protected IMediator? Mediator;

        public BaseComponent(IMediator? mediator)
        {
            Mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
    }

}
