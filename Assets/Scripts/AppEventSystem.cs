
// ------------------------------------------------------------------------
// Simple event system

namespace AppEvent
{
	public class EventSystem<E>
	{
		public delegate void EventCallback();
		private static EventCallback[] listeners = new EventCallback[100];

		public static void Subscribe(E eventID, EventCallback callback)
		{
			listeners[(int)(object)eventID] += callback;
		}

		public static void Unsubscribe(E eventID, EventCallback callback)
		{
			listeners[(int)(object)eventID] -= callback;
		}

		public static void TriggerEvent(E eventID)
		{
			listeners[(int)(object)eventID]?.Invoke();
		}
	}

	// ------------------------------------------------------------------------
	// Event listeners with 1 custom parameter

	public class EventSystem<E, T>
	{
		public delegate void EventCallback(T data);

		private static EventCallback[] listeners = new EventCallback[100];

		public static void Subscribe(E eventID, EventCallback callback)
		{
			listeners[(int)(object)eventID] += callback;
		}

		public static void Unsubscribe(E eventID, EventCallback callback)
		{
			listeners[(int)(object)eventID] -= callback;
		}

		public static void TriggerEvent(E eventID, T data)
		{
			listeners[(int)(object)eventID]?.Invoke(data);
		}
	}

	// ------------------------------------------------------------------------
	// Event listeners with multiple parameters

	public class EventSystemMulti<E>
	{
		public delegate void EventCallback(params object[] data);

		private static EventCallback[] listeners = new EventCallback[100];

		public static void Subscribe(E eventID, EventCallback callback)
		{
			listeners[(int)(object)eventID] += callback;
		}

		public static void Unsubscribe(E eventID, EventCallback callback)
		{
			listeners[(int)(object)eventID] -= callback;
		}

		public static void TriggerEvent(E eventID, params object[] Objects)
		{
			listeners[(int)(object)eventID]?.Invoke(Objects);
		}
	}
}