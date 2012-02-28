using System.Collections.Generic;

namespace CloneGame.Input
{
    interface IEventReciver<T>
    {
        void HandleEvent(IEnumerable<T> events);

    }

    interface IKeyboardEventReciver : IEventReciver<KeybuttonEvent>
    {

    }

	interface IMouseEventReciver : IEventReciver<MouseEvent>
	{

	}

	internal interface ICharEventReciver : IEventReciver<CharButtonEvent>
	{
		
	}
}
