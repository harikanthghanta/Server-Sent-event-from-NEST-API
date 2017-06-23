using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventSource = new EventSource("https://developer-api.nest.com/devices/thermostats/UOztydCe0NSPPhzoPI_ztMevEYpE9-oo/?auth=c.7ACWK2LWlQW7BQB51iQihvVfYvussuwluWjoet6Lo4DQvm7PnE5X9w5RkBv6TcPIdF7ywVCvQCx5EvLoYt6qh5cfQsHRBceT9LqtZwqMT395HzYH53yvo9FhwTGoDXXJDv858YxBdr0Lhq9q");
            try
            {                
                eventSource.StateChange += EventSource_StateChange;
                eventSource.Message += EventSource_Message;
                
                

                eventSource.Connect();
                
                while (true)
                {

                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"Error {exception.Message}");
            }
            finally
            {
                if (eventSource.ReadyState == EventSource.EventSourceState.Open )
                {
                    Console.WriteLine("connected");
                    eventSource.Shutdown();
                }
                       eventSource.StateChange -= EventSource_StateChange;
                    eventSource.Message -= EventSource_Message;
             
            }


        }

        private static void EventSource_StateChange(object sender, EventSource.StateChangeEventArgs e)
        {
            Console.WriteLine($"State Change {e.NewState.ToString()}" );
        }

        private static void event_handler(object sender, EventSource.ServerSentEventArgs e)
        {
            Console.WriteLine("Trying to use event handler");

        }

        private static void EventSource_Message(object sender, EventSource.ServerSentEventArgs e)
        {
            var serverSentEventArgs = e;
            var data = e.Data;
            var eventType = e.EventType;
            Console.WriteLine($"Event Type {eventType}, Event Data {data}");
        }
    }
}
