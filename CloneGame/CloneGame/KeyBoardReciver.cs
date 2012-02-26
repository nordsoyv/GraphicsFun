using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloneGame
{
    interface EventReciver<T>
    {
        void HandleEvent(List<T> events);

    }

    interface KeyboardEventReciver : EventReciver<KeyboardEvent>
    {

    }


}
