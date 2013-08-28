using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace ImportantDate.Model
{
    class DatabaseContext : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/idatedb.sdf";

        // Pass the connection string to the base class.
        public DatabaseContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a single table for the to-do items.
        public Table<IDate> IDates;
        public Table<Anniversary> Anniversaries;
        public Table<IDateAnniversary> IDateAnniversaries;
    }
}
