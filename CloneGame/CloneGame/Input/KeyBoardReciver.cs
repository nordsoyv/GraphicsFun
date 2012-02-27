using System.Collections.Generic;

namespace CloneGame.Input
{
    interface IEventReciver<T>
    {
        void HandleEvent(List<T> events);

    }

    interface IKeyboardEventReciver : IEventReciver<KeyboardEvent>
    {

    }

	interface IMouseEventReciver : IEventReciver<MouseEvent>
	{

	}


}
