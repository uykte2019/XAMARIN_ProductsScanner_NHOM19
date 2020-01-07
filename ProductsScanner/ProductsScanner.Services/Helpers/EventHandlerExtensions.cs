namespace ProductsScanner.Services.Helpers
{
    using System;

    public static class EventHandlerExtensions
    {
        public static bool IsEventHandlerRegistered(this EventHandler handler, Delegate prospectiveHandler)
        {
            var isRegistered = default(bool);

            if (handler != null)
            {
                foreach (Delegate currentHandler in handler.GetInvocationList())
                {
                    if (currentHandler == prospectiveHandler)
                    {
                        isRegistered = true;
                        return isRegistered;
                    }
                }
            }

            return isRegistered;
        }

        public static bool HasAnyHandlers(this EventHandler handler)
        {
            return handler == null 
                ? default(bool) 
                : handler.GetInvocationList().Length > 0;
        }
    }
}