using System;
using System.ComponentModel.Design;

namespace ProcessDelivery
{
    public class LibraryManager
    {
        private string EvaluateRiskLevel(DateTime? lastDueDate,
            DateTime? lastReturnedDate, DateTime CurrentDueDate, DateTime dateReturned)
        {
            bool FirstTimeButOnTime = (dateReturned == lastDueDate) && lastReturnedDate == null;
            bool FirstTimeButLate = (dateReturned > lastDueDate) && lastReturnedDate == null;
            bool FirstTimeButEarly = (dateReturned < lastDueDate) && lastReturnedDate == null;

            if (FirstTimeButOnTime)
                return "LowRisk: first time being returned and returned on time";
            else if (FirstTimeButLate)
                return "MediumRisk: first time being returned and returned late";
            else if (FirstTimeButEarly)
                return "LowRisk: first time being returned and returned early";


            if (lastDueDate == lastReturnedDate)
            {
                if (CurrentDueDate == dateReturned)
                    return "LowRisk: returned on due date last 2 times";

                return CurrentDueDate < dateReturned
                    ? "MediumRisk: returned on due date last time but late this time"
                    : "MediumRisk: returned on due date last time but early this time";
            }

            if (lastDueDate < lastReturnedDate)
            {
                if (CurrentDueDate == dateReturned)
                    return "MediumRisk: returned late last time but on due date this time";

                return CurrentDueDate < dateReturned
                    ? "HighRisk: returned late last time and late this time"
                    : "MediumRisk: returned late last time but early this time";
            }


            if (lastDueDate > lastReturnedDate)
            {
                return CurrentDueDate == dateReturned
                        ? "LowRisk: returned early last time and on due date this time"
                        : CurrentDueDate < dateReturned
                            ? "MediumRisk: returned early last time but late this time"
                            : "LowRisk: returned early last time and early this time";
            }

            return string.Empty;
        }






        public string ReturnBook(Book book, DateTime dateReturned)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            return EvaluateRiskLevel(book.LastDueDate, book.LastReturnedDate, book.CurrentDueDate, dateReturned);
        }
    }
}
