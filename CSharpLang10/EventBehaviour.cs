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


        public delegate void Call(string message);

        // The type of the event needs to be a delegate (below)
        // Tip: Use a past-tense for things that have happened, or present-tense (e.g. Closing) for 
        // events that are about to happen

        // The simplest way to declare an event for your class is as a public field.
        // The compiler emits code that means although this looks like a field, it can 
        // only be accessed in safe ways.
        public event Call FormClosed;

        // The only two operations allowed on the above are add (+=) and remove (-=).

        // The type of the event (EventHandler<FileListArgs> in this example) must be a delegate type. There are a number of conventions that you should follow when declaring an event. Typically, the event delegate type has a void return. Event declarations should be a verb, or a verb phrase. Use past tense (as in this example) when the event reports something that has happened. Use a present tense verb (for example, Closing) to report something that is about to happen. Often, using present tense indicates that your class supports some kind of customization behavior. One of the most common scenarios is to support cancellation. For example, a Closing event may include an argument that would indicate if the close operation should continue, or not. Other scenarios may enable callers to modify behavior by updating properties of the event arguments. You may raise an event to indicate a proposed next action an algorithm will take. The event handler may mandate a different action by modifying properties of the event argument.

        // To raise an event:
        [Test]
        public void ShouldRaise()
        {
            // To raise event, just invoke it like a delegate
            if (FormClosed != null)
            {
                FormClosed("closed!");
            }
        }

        [Test]
        public void ShouldRaiseWithAlternateSyntax()
        {
            // To raise event, just invoke it like a delegate (alternate syntax)
            if (FormClosed != null)
            {
                FormClosed.Invoke("closed");
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
            FormClosed += OnClosed;
            ShouldRaise();
            FluentAssertions.AssertionExtensions.Should(_value).Be(1);
            // To unsubscribe:
            FormClosed -= OnClosed;
        }

        // to subscribe to an event:
        [Test]
        public void SupportsMulticast()
        {
            FormClosed += OnClosed;
            FormClosed += OnClosed;
            ShouldRaise();
            FluentAssertions.AssertionExtensions.Should(_value).Be(2);
        }

        //  The handler method typically is the prefix 'On' followed by the event name, as shown above.
        private void OnClosed(string message)
        {
            _value += 1;
        }

        // .NET events generally follow a few known patterns.
        // Standardizing on these patterns means that developers can leverage knowledge of
        // those standard patterns, which can be applied to any .NET event program.
        // The standard signature for a .NET event delegate is:
        public delegate void OnEventRaised(object sender, EventArgs args);
        // The single return value doesn't scale for multiple subscribers.  Hence void.
        // The first argument is the sender, the second typically derives from EventArgs
        // EventArgs.Empty should be used if there are no EventArgs
        // Otherwise, you typically derive a class from EventArgs for the second argument
        // ** If your event type does not need any additional arguments, you will still provide both arguments. There is a special value, EventArgs.Empty that you should use to denote that your event does not contain any additional information.

        public event OnEventRaised EventRaised;

        [Test]
        public void RaiseConvention()
        {
            if (EventRaised != null)
            {
                EventRaised.Invoke(this, EventArgs.Empty);
            }
        }

        // Using an event model means you can have multiple subscribers, each with their own 
        // response to the event

        // Suggestion: Keep your EventArgs immutable

        public class FileFound : EventArgs
        {
            public string FullPath;
        }

        // Communicating from the handler to the sender of the event.  E.g. Cancellation
        // Since the event handler is a void return, the standard pattern is to 
        // include fields on the EventArgs object that receivers can use to communicate
        // cancellation
        // There are two patterns, either the cancellation flag is false and someone
        // must set it true or it is ture and all receivers must set it false.

        // The recommendation is that EventArgs should be immutable if you can make them
        public class FileFoundEventArgs : EventArgs
        {
            public FileFoundEventArgs(string fullPath)
            {
                FullPath = fullPath;
            }

            public string FullPath;
            public bool Cancel = false; // the nice thing about just adding this flag is that 
            // it is very loose coupling.  Only clients who want to use it need to.
        }

        public delegate void FileFoundEvent(object sender, FileFoundEventArgs eventArgs);

        public event FileFoundEvent OnFileFound;

        [Test]
        public void ShouldCancel()
        {
            OnFileFound += EventBehaviour_OnFileFound;

            FileFoundEventArgs args = new FileFoundEventArgs(@"c:\temp\1.txt");
            OnFileFound.Invoke(this, args);
            if (args.Cancel)
            {
                return;
            }
        }

        private void EventBehaviour_OnFileFound(object sender, FileFoundEventArgs eventArgs)
        {
            eventArgs.Cancel = true;
        }

        // TODO: Talk about the EventHandler delegate

        // In addition to the field syntax, you can explicitly create the event property
        // with add and remove handlers.  The syntax is similar to properties.

        internal event EventHandler DirectoryChanged
        {
            add
            {
                directoryChanged += value;
                Console.WriteLine("Hello");
            }
            remove { directoryChanged -= value; }
        }
        // You must declare a private backing field for the event
        private EventHandler directoryChanged;

        // Events vs. Delegates
        // Use Events is subscription is optional.  Use delegates to inject an implementation.
        // If your code can complete all its work without calling any subscribers, use events.
        // Delegates for events must use void.  If you need a return value, then you'll need a delegate.
        // Event subscriptions also typically tend to be long-lived (e.g. the duration of a program)
        // while delegates can be short lived (e.g. passed to a method)

    }
}
