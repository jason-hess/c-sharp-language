using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CSharpLang10
{
    public class EventBehaviour
    {
        private int _value = 0;
        // Events are, like delegates, a late binding mechanism.
        // In fact, events are built on the language support for delegates.
        // 
        // Events are a way for an object to broadcast (to all interested components in the system)
        // that something has happened. Any other component can subscribe to the event,
        // and be notified when an event is raised.
        // Events are built on delegates

        // You can define events that should be raised for your classes.
        // One important consideration when working with events is that there
        // may not be any object registered for a particular event.
        // You must write your code so that it does not raise events
        // when no listeners are configured.
        // 
        // Subscribing to an event also creates a coupling between two objects (the event source, and the event sink). You need to ensure that the event sink unsubscribes from the event source when no longer interested in events.

        // To define an event you use the `event` keyword:

        // The type of the event needs to be a delegate

        public delegate void Call(string message);

        // Tip: Use a past-tense for things that have happened, or present-tense (e.g. Closing) for 
        // events that are about to happen
        public event Call FormClosed;

        // The type of the event (EventHandler<FileListArgs> in this example) must be a delegate type. There are a number of conventions that you should follow when declaring an event. Typically, the event delegate type has a void return. Event declarations should be a verb, or a verb phrase. Use past tense (as in this example) when the event reports something that has happened. Use a present tense verb (for example, Closing) to report something that is about to happen. Often, using present tense indicates that your class supports some kind of customization behavior. One of the most common scenarios is to support cancellation. For example, a Closing event may include an argument that would indicate if the close operation should continue, or not. Other scenarios may enable callers to modify behavior by updating properties of the event arguments. You may raise an event to indicate a proposed next action an algorithm will take. The event handler may mandate a different action by modifying properties of the event argument.

        // To raise an event:
        [Test]
        public void ShouldRaise()
        {
            // To raise event, just call it like a delegate
            if (FormClosed != null)
            {
                FormClosed("closed!");
            }
        }

        [Test]
        public void ShouldRaiseWhenNoListener()
        {
            try
            {
                FormClosed("closed!");
            }
            catch (Exception ex)
            {
                return;
            }

            FluentAssertions.AssertionExtensions.Should(true).BeFalse();
        }

        [SetUp]
        public void SetUp()
        {
            // NUnit instantiates the class once
            _value = 0;
        }

        // to subscribe to an event:
        [Test]
        public void ShouldSubscribe()
        {
            FormClosed += EventBehaviour_FormClosed;
            ShouldRaise();
            FluentAssertions.AssertionExtensions.Should(_value).Be(1);
            FormClosed -= EventBehaviour_FormClosed;
        }

        // to subscribe to an event:
        [Test]
        public void SupportsMulticast()
        {
            FormClosed += EventBehaviour_FormClosed;
            FormClosed += EventBehaviour_FormClosed;
            ShouldRaise();
            FluentAssertions.AssertionExtensions.Should(_value).Be(2);
        }

        private void EventBehaviour_FormClosed(string message)
        {
            _value += 1;
        }

        // .NET events generally follow a few known patterns. Standardizing on these patterns means that developers can leverage knowledge of those standard patterns, which can be applied to any .NET event program.
    }
}
