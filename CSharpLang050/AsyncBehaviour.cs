namespace CSharpLang50
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-50
    /// </summary>
    public class AsyncBehaviour
    {
        // Event Behavior with Async
        /** Events with Async subscribers
           You have one final pattern to learn: How to correctly write event subscribers that call async code. The challenge is described in the article on async and await. Async methods can have a void return type, but that is strongly discouraged. When your event subscriber code calls an async method, you have no choice but to create an async void method. The event handler signature requires it.
           
           You need to reconcile this opposing guidance. Somehow, you must create a safe async void method. The basics of the pattern you need to implement are below:
           
           C#
           
           Copy
           worker.StartWorking += async (sender, eventArgs) =>
           {
           try 
           {
           await DoWorkAsync();
           }
           catch (Exception e)
           {
           //Some form of logging.
           Console.WriteLine($"Async task failure: {e.ToString()}");
           // Consider gracefully, and quickly exiting.
           }
           };
           First, notice that the handler is marked as an async handler. Because it is being assigned to an event handler delegate type, it will have a void return type. That means you must follow the pattern shown in the handler, and not allow any exceptions to be thrown out of the context of the async handler. Because it does not return a task, there is no task that can report the error by entering the faulted state. Because the method is async, the method can't simply throw the exception. (The calling method has continued execution because it is async.) The actual runtime behavior will be defined differently for different environments. It may terminate the thread, it may terminate the program, or it may leave the program in an undetermined state. None of those are good outcomes.
           
           That's why you should wrap the await statement for the async Task in your own try block. If it does cause a faulted task, you can log the error. If it is an error from which your application cannot recover, you can exit the program quickly and gracefully
           
           Those are the major updates to the .NET event pattern. You will see many examples of the earlier versions in the libraries you work with. However, you should understand what the latest patterns are as well.
           
           The next article in this series helps you distinguish between using delegates and events in your designs. They are similar concepts, and that article will help you make the best decision for your programs. **/
    }
}
