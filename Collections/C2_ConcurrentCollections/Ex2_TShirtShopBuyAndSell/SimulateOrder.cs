using Collections.C2_ConcurrentCollections.Shop_Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collections.C2_ConcurrentCollections.Ex2_TShirtShopBuyAndSell
{
  public class SimulateShop
  {

    //Entry function 
    public static void Start()
    {
			StockController controller = new StockController();
			TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);
			StaffRecords staffLogs = new StaffRecords();
			LogTradesQueue tradesQueue = new LogTradesQueue(staffLogs);

			SalesPerson[] staff =
			{
				new SalesPerson("Sahil"),
				new SalesPerson("Julie"),
				new SalesPerson("Kim"),
				new SalesPerson("Chuck")
			};
			List<Task> salesTasks = new List<Task>();
			foreach (SalesPerson person in staff)
			{
				salesTasks.Add(
					Task.Run(() => person.Work(workDay, controller, tradesQueue)));
			}

			Task[] loggingTasks =
			{
				Task.Run(() => tradesQueue.MonitorAndLogTrades()),
				Task.Run(() => tradesQueue.MonitorAndLogTrades())
			};

			Task.WaitAll(salesTasks.ToArray());
			tradesQueue.SetNoMoreTrades();
			Task.WaitAll(loggingTasks);

			controller.DisplayStock();
			staffLogs.DisplayCommissions(staff);
		}
  }

}
